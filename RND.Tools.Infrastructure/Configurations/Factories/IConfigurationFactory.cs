using System.Configuration;

namespace RND.Tools.Infrastructure.Configurations.Factories;

public interface IConfigurationFactory
{
	ConfigurationSection CreateSection();
}