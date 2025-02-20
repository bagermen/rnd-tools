using Microsoft.Extensions.DependencyInjection;
using RND.Tools.Core.Interfaces;
using RND.Tools.Infrastructure.Configurations;

namespace RND.Tools.Infrastructure.Services.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddScoped<IConfigrationManager, ConfigrationManager>();
			return services;
		}
	}
}