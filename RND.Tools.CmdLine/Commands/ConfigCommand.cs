using RND.Tools.CmdLine.Commands.Options;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands;

internal class ConfigCommand : Command
{
	public ConfigCommand() : base(
		name: CommandNames.Config,
		description: "Application configuration management"
		)
	{
		Add(new SettingCommand());
		Add(new СonnectionCommand());
		Add(new DesignModeCommand());
		Add(new DbTypeCommand());

		AddGlobalOption(new AssemblyPathOption
		{
			IsRequired = true,
		});
	}
}