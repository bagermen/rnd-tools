using System.CommandLine;

namespace RND.Tools.CmdLine.Commands.Arguments;

internal class DesignModeArgument : Argument<bool>
{
	public DesignModeArgument(
		string name = "DesignModeValue"
		) : base(
			name: name,
			description: "DesignMode value"
		)
	{
	}
}