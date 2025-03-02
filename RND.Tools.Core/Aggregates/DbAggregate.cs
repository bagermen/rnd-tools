using Microsoft.Extensions.DependencyInjection;
using RND.Tools.Core.Interfaces;
using DbTypeEnum = RND.Tools.Core.Enums.DbType;

namespace RND.Tools.Core.Aggregates;

public class DbAggregate(
	ConfigurationAggregate configuration,
	[FromKeyedServices(DbTypeEnum.PostgreSQL)] IDbLoader psqlLoader
	) : IAggregateRoot
{
	public async Task Load(FileInfo backupFile, CancellationToken cancellationToken)
	{
		var dbType = configuration.GetDbType();

		cancellationToken.ThrowIfCancellationRequested();

		switch (dbType)
		{
			case DbTypeEnum.PostgreSQL:
				await psqlLoader.Load(backupFile, cancellationToken);
				break;
			case null:
				throw new InvalidOperationException("Database type not set");
			default:
				throw new NotSupportedException("Database type not supported");
		}
	}
}