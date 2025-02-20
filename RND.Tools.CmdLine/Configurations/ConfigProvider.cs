using Microsoft.Extensions.Configuration;

namespace RND.Tools.CmdLine.Configurations;

internal class ConfigProvider: ConfigurationProvider
{
	public override void Set(string key, string value)
    {
        base.Set(key, value);
        OnReload();
    }
}