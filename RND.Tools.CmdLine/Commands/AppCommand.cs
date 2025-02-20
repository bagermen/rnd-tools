using RND.Tools.CmdLine.Commands.Options;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands;

internal class AppCommand : Command
{
	public AppCommand() : base(
		name: CommandNames.AppConfig,
		description: "Application configuration management"
		)
	{
		Add(new SettingCommand());
		Add(new СonnectionCommand());
		Add(new DesignModeCommand());

		AddGlobalOption(new AssemblyPathOption
		{
			IsRequired = true,
		});
	}
}