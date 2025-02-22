using RND.Tools.Core.Enums;

namespace RND.Tools.Core.Interfaces;

public interface IConfigrationManager
{
	string? GetAppSetting(string key);
	string? GetConnectionString(string key);
	string? GetDBConnectionStringName();
	Type? GetDBExecutorType();
	string? GetFileDesignMode();
	void RemoveAppSetting(string key);
	void RemoveConnectionString(string key);
	void SetAppSetting(string key, string value);
	void SetConnectionString(string key, string value);
	void SetDbConnection(DbType dbType);
	void SetFileDesignMode(string value);
}
