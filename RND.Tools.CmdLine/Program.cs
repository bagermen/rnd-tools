// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RND.Tools.CmdLine.Commands;
using RND.Tools.CmdLine.Configurations;
using RND.Tools.Core.Interfaces;
using RND.Tools.UseCases.Config.GetAppSetting;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using System.Reflection;
using RND.Tools.Infrastructure.Services.Extensions;
using RND.Tools.CmdLine.Commands.Handlers;
using RND.Tools.Core.Extensions;

/**
app --assembly-path ./appsettings.dll setting key value
app --assembly-path ./appsettings.dll connection key value
app --assembly-path ./appsettings.dll design-mode value

wc --assembly-path ./appsettings.dll setting key value
wc --assembly-path ./appsettings.dll connection key value

// second version
db --assembly-path ./appsettings.dll --key key load /backup
db --assembly-path ./appsettings.dll --key key backup ./backup

redis --assembly-path ./appsettings.dll --key key clear
*/

var commandBuilder = new CommandLineBuilder();

CreateCommand(commandBuilder.Command);

await commandBuilder
	.UseHost(
		_ => Host.CreateDefaultBuilder(),
		builder =>
		{
			var configProvider = new ConfigProvider();
			builder.ConfigureAppConfiguration(config => {
				config.Add(new ConfigSource(configProvider));
			});
			builder.ConfigureServices(services =>
			{
				var mediatRAssemblies = new [] {
					Assembly.GetAssembly(typeof(GetAppSettingRequest))
				};
				services.AddMediatR(cfg => {
					cfg.RegisterServicesFromAssemblies(mediatRAssemblies!);
				});
				services.AddSingleton(configProvider);
				services.AddSingleton<CmdLineOptions>();
				services.AddTransient<ICmdLineOptions>(sp => sp.GetRequiredService<CmdLineOptions>());
				services.AddInfrastructureServices();
				services.AddCoreServices();

			})
			.ConfigureContainer<IServiceCollection>((builder, services) =>
			{
				var configurationRoot = builder.Configuration;
				services
					.AddOptions<CmdLineSettings>()
					.Bind(configurationRoot.GetSection(CmdLineSettings.CmdLine));

			})
			.UseCommandHandler<SettingCommand, SettingHandler>()
			.UseCommandHandler<СonnectionCommand, ConnectionsHandler>()
			.UseCommandHandler<DesignModeCommand, DesignModeHandler>();
		})
	.UseDefaults()
	.Build()
	.InvokeAsync(args);

void CreateCommand(Command command)
{
	command.Description = "Build management";
	command.Add(new AppCommand());
	command.Add(new WCCommand());
}
