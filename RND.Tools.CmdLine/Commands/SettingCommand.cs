using RND.Tools.CmdLine.Commands.Arguments;
using RND.Tools.CmdLine.Commands.Options;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands;

internal class SettingCommand : Command
{
	public SettingCommand() : base(
		name: CommandNames.Setting,
		description: "AppSetting configuration management"
		)
	{
		var settingKey = new SettingKeyArgument();
		var settingValue = new SettingValueArgument
		{
			Arity = ArgumentArity.ZeroOrOne,
		};
		AddArgument(settingKey);
		AddArgument(settingValue);
	}
}