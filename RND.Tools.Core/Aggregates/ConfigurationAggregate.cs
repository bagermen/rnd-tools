using RND.Tools.Core.Enums;
using RND.Tools.Core.Interfaces;

namespace RND.Tools.Core.Aggregates;

public class ConfigurationAggregate(
	IConfigrationManager configrationManager,
	IConfigurationTypeProvider configurationTypeProvider
	) : IAggregateRoot
{
	public string? GetConnectionString(string key)
	{
		return configrationManager.GetConnectionString(key);
	}
	public string? GetAppSetting(string key)
	{
		return configrationManager.GetAppSetting(key);
	}
	public void SetConnectionString(string key, string value)
	{
		configrationManager.SetConnectionString(key, value);
	}
	public void SetAppSetting(string key, string value)
	{
		configrationManager.SetAppSetting(key, value);
	}
	public bool GetFileDesignMode()
	{
		bool result;
		var designMode = bool.TryParse(configrationManager.GetFileDesignMode(), out result) && result;
		var staticFileContent = bool.TryParse(configrationManager.GetAppSetting("UseStaticFileContent"), out result) && result;

		return designMode && !staticFileContent;
	}
	public void SetFileDesignMode(bool value)
	{
		configrationManager.SetFileDesignMode(value.ToString());
		configrationManager.SetAppSetting("UseStaticFileContent", (!value).ToString());
	}

	public DbType? GetDbType()
	{
		return configrationManager.GetDBExecutorType() switch
		{
			var v when ReferenceEquals(v, configurationTypeProvider.PostgreSqlExecutorType) => DbType.PostgreSQL,
			var v when ReferenceEquals(v, configurationTypeProvider.MSSqlExecutorType) => DbType.SQLServer,
			_ => null
		};
	}

	public void SetDbType(DbType dbType)
	{
		var connectionStringName = configrationManager.GetDBConnectionKey() ?? "db";
		configrationManager.SetDbConnection(connectionStringName, dbType);
		configrationManager.UpdateQuartzToDb(connectionStringName, dbType);
	}
}