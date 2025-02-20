using System.CommandLine;

namespace RND.Tools.CmdLine.Commands.Arguments;

internal class SettingKeyArgument : Argument<string>
{
	public SettingKeyArgument(
		string name = "settingKey",
		string? description = "Specifies AppSetting key"
		) : base(
			name: name,
			description: description
		)
	{}
}
