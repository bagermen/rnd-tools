using Npgsql;
using RND.Tools.Core.Interfaces;
using System.Data.Common;

namespace RND.Tools.Infrastructure.Db.ConnectionStrings;

public class PostgresConnectionStringGetter(IConfigrationManager configrationManager) : BaseConnectionStringGetter(configrationManager), IConnectionStringGetter
{
	public NpgsqlConnectionStringBuilder GetConnectionStringBuilder()
	{
		var connectionString = GetConnectionStringSafe();
		var builder = new NpgsqlConnectionStringBuilder();

		builder.ConnectionString = connectionString;
		return builder;
	}
	DbConnectionStringBuilder IConnectionStringGetter.GetConnectionStringBuilder()
	{
		return GetConnectionStringBuilder();
	}
}