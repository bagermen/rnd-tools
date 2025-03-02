using RND.Tools.Core.Interfaces;
using RND.Tools.Core.Utilities;

namespace RND.Tools.Infrastructure.Db.ConnectionStrings;

public abstract class BaseConnectionStringGetter(IConfigrationManager configrationManager)
{
	protected IConfigrationManager ConfigrationManager => configrationManager;
	protected string GetConnectionStringSafe()
	{
		var dbConnectionKey = configrationManager.GetDBConnectionKey();
		Guard.AgainstNullOrWhiteSpace(dbConnectionKey, nameof(dbConnectionKey));

		var connectionString = configrationManager.GetConnectionString(dbConnectionKey);
		Guard.AgainstNullOrWhiteSpace(connectionString, nameof(connectionString));

		return connectionString;
	}
}