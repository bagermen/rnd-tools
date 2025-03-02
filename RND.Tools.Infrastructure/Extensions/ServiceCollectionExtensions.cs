using Microsoft.Extensions.DependencyInjection;
using RND.Tools.Core.Interfaces;
using RND.Tools.Infrastructure.Configurations;
using RND.Tools.Infrastructure.Configurations.Factories;
using RND.Tools.Infrastructure.Db;
using RND.Tools.Infrastructure.Db.ConnectionStrings;
using DbTypeEnum = RND.Tools.Core.Enums.DbType;

namespace RND.Tools.Infrastructure.Services.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddScoped(serviceProvider =>
			{
				var options = serviceProvider.GetRequiredService<ICmdLineOptions>();
				return new ConfigurationLoadContext(options.AssemblyPath, true);
			});

			services.AddTransient<IConfigurationTypeProvider, ConfigurationTypeProvider>();
			services.AddTransient<IConfigurationFactory, MSSQLConnectionFactory>();
			services.AddTransient<IConfigurationFactory, PostgreSQLConnectionFactory>();
			services.AddScoped<IConfigrationManager, ConfigrationManager>(serviceProvider =>
			{
				var cmdOptions = serviceProvider.GetRequiredService<ICmdLineOptions>();
				var alc = serviceProvider.GetRequiredService<ConfigurationLoadContext>();
				var configurationFactories = serviceProvider.GetServices<IConfigurationFactory>();

				return new ConfigrationManager(cmdOptions, alc,	configurationFactories);
			});
			services.AddKeyedTransient<IConnectionStringGetter, PostgresConnectionStringGetter>(DbTypeEnum.PostgreSQL);
			services.AddKeyedTransient<IDbLoader, PostgresDbLoader>(DbTypeEnum.PostgreSQL);

			return services;
		}
	}
}