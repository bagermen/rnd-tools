using RND.Tools.Core.Interfaces;
using System.Configuration;

namespace RND.Tools.Infrastructure.Configurations.Factories;

internal abstract class BaseConfigurationFactory(IConfigurationTypeProvider typeProvider) : IConfigurationFactory
{
	protected IConfigurationTypeProvider TypeProvider => typeProvider;
	public abstract ConfigurationSection CreateSection();
}