using RND.Tools.CmdLine.Commands.Options;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands;

internal class WCCommand : Command
{
	public WCCommand() : base(
		name: CommandNames.WCConfig,
		description: "WC configuration management"
		)
	{
		Add(new SettingCommand());
		Add(new СonnectionCommand());

		AddGlobalOption(new AssemblyPathOption
		{
			IsRequired = true,
		});
	}
}