using Microsoft.Extensions.Configuration;

namespace RND.Tools.CmdLine.Configurations;

internal class ConfigSource(ConfigProvider provider) : IConfigurationSource
{
	public IConfigurationProvider Build(IConfigurationBuilder builder) => provider;
}
