using System.CommandLine;

namespace RND.Tools.CmdLine.Commands.Arguments;

internal class SettingValueArgument : Argument<string>
{
	public SettingValueArgument(
		string name = "settingValue"
		) : base(
			name: name,
			description: "value of AppSettings configuration"
		)
	{
	}
}