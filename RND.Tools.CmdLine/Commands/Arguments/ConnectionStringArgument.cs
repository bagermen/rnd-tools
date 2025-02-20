using System.CommandLine;

namespace RND.Tools.CmdLine.Commands.Arguments;

internal class ConnectionStringArgument : Argument<string>
{
	public ConnectionStringArgument(
		string name = "connectionString"
		) : base(
			name: name,
			description: "connection string"
		)
	{
	}
}