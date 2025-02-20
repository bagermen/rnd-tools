using RND.Tools.CmdLine.Commands.Arguments;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands;

internal class DesignModeCommand : Command
{
	public DesignModeCommand() : base(
		name: CommandNames.DesignMode,
		description: "FileDesignMode configuration management"
		)
	{
		var designMode = new DesignModeArgument
		{
			Arity = ArgumentArity.ZeroOrOne,
		};

		AddArgument(designMode);
	}
}
