using Microsoft.Extensions.DependencyInjection;
using RND.Tools.Core.Aggregates;

namespace RND.Tools.Core.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCoreServices(this IServiceCollection services)
		{
			services.AddScoped<ConfigurationAggregate>();
			services.AddScoped<DbAggregate>();
			return services;
		}
	}
}