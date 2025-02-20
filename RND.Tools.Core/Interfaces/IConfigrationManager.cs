using System;

namespace RND.Tools.Core.Interfaces;

public interface IConfigrationManager
{
	string? GetConnectionString(string key);
	string? GetAppSetting(string key);
	void SetConnectionString(string key, string value);
	void SetAppSetting(string key, string value);
	void RemoveConnectionString(string key);
	void RemoveAppSetting(string key);
	string? GetFileDesignMode();
	void SetFileDesignMode(string value);
}
