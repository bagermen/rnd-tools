using RND.Tools.CmdLine.Commands.Arguments;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands;

internal class СonnectionCommand : Command
{
	public СonnectionCommand() : base(name: CommandNames.Connection, description: "СonnectionString configuration management")
	{
		var connectionKey = new ConnectionKeyArgument();
		var connectionString = new ConnectionStringArgument
		{
			Arity = ArgumentArity.ZeroOrOne,
		};
		AddArgument(connectionKey);
		AddArgument(connectionString);
	}
}