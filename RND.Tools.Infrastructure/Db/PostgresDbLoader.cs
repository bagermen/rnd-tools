using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using RND.Tools.Core.Interfaces;
using RND.Tools.Core.Utilities;
using RND.Tools.Infrastructure.Db.ConnectionStrings;
using System.Diagnostics;
using DbTypeEnum = RND.Tools.Core.Enums.DbType;

namespace RND.Tools.Infrastructure.Db;

public class PostgresDbLoader(
	[FromKeyedServices(DbTypeEnum.PostgreSQL)] IConnectionStringGetter connectionStringGetter,
	ILogger<PostgresDbLoader> logger
) : IDbLoader
{
	public async Task Load(FileInfo backupFile, CancellationToken cancellationToken)
	{
		logger.LogInformation("Starting Load method");
		var connectionStringBuilder = GetConnectionBuilder();

		await DropAndCreateDatabase(connectionStringBuilder, cancellationToken);
		await RestoreDatabase(backupFile, connectionStringBuilder, cancellationToken);

		logger.LogInformation("Finished Load method");
	}

	private NpgsqlConnectionStringBuilder GetConnectionBuilder()
	{
		var builder = (NpgsqlConnectionStringBuilder) connectionStringGetter.GetConnectionStringBuilder();
		builder.Username = Environment.GetEnvironmentVariable("PGUSER") ?? "postgres";
		builder.Password = Environment.GetEnvironmentVariable("PGPASSWORD") ?? builder.Password;
		builder.Host = Environment.GetEnvironmentVariable("PGHOST") ?? "localhost";
		builder.Port = int.Parse(Environment.GetEnvironmentVariable("PGPORT") ?? "5432");

		return builder;
	}
	private async Task DropAndCreateDatabase(NpgsqlConnectionStringBuilder connectionStringBuilder, CancellationToken cancellationToken)
	{
		Guard.AgainstNullOrWhiteSpace(connectionStringBuilder.Database, nameof(connectionStringBuilder.Database));
		var database = connectionStringBuilder.Database;


		var masterConnectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionStringBuilder.ConnectionString)
		{
			Database = "postgres"
		};

		await using var connection = new NpgsqlConnection(masterConnectionStringBuilder.ConnectionString);
		await connection.OpenAsync(cancellationToken);

		await using var command = connection.CreateCommand();

		logger.LogInformation("Setting database \"{Database}\" offline", database);
		command.CommandText = $"SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity WHERE pg_stat_activity.datname = '{database}' AND pid <> pg_backend_pid();";
		await command.ExecuteNonQueryAsync(cancellationToken);

		logger.LogInformation("Dropping and creating database \"{Database}\"", database);
		command.CommandText = $"DROP DATABASE IF EXISTS {database};";
		await command.ExecuteNonQueryAsync(cancellationToken);

		command.CommandText = $"CREATE DATABASE {database} WITH OWNER = {connectionStringBuilder.Username} ENCODING = 'UTF8' CONNECTION LIMIT = -1;";
		await command.ExecuteNonQueryAsync(cancellationToken);

		await connection.CloseAsync();
		logger.LogInformation("Database \"{Database}\" dropped and created successfully", database);
	}

	private async Task RestoreDatabase(FileInfo backupFile, NpgsqlConnectionStringBuilder connectionStringBuilder, CancellationToken cancellationToken)
	{
		logger.LogInformation("Restoring database \"{Database}\" from backup file \"{BackupFile}\"", connectionStringBuilder.Database, backupFile.FullName);

		var processStartInfo = new ProcessStartInfo
		{
			FileName = GetPgRestorePath(),
			ArgumentList = {
				"--no-acl",
				"--no-owner",
				"--verbose",
				"-h", connectionStringBuilder.Host!.ToString(),
				"-p", connectionStringBuilder.Port.ToString(),
				"-U", connectionStringBuilder.Username!.ToString(),
				"-d", connectionStringBuilder.Database!.ToString(),
				backupFile.FullName
			},
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false,
			CreateNoWindow = true,
			Environment =
			{
				["PGPASSWORD"] = connectionStringBuilder.Password
			}
		};

		using var process = new Process { StartInfo = processStartInfo };
		process.OutputDataReceived += (sender, args) => logger.LogInformation(args.Data);
		process.ErrorDataReceived += (sender, args) => logger.LogError(args.Data);

		process.Start();
		process.BeginOutputReadLine();
		process.BeginErrorReadLine();

		await process.WaitForExitAsync(cancellationToken);

		if (process.ExitCode != 0)
		{
			throw new Exception($"pg_restore failed with exit code {process.ExitCode}");
		}

		logger.LogInformation("Database \"{Database}\" restored successfully", connectionStringBuilder.Database);
	}

	private string GetPgRestorePath()
	{
		var paths = Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator) ?? Array.Empty<string>();
		var appPaths = paths.SelectMany(path => new[] { "pg_restore", "pg_restore.exe" }.Select(program => Path.Combine(path, program)));

		var fullPath = appPaths.FirstOrDefault(File.Exists);

		if (string.IsNullOrEmpty(fullPath))
		{
			throw new Exception("pg_restore not found in PATH");
		}

		return fullPath;
	}
}