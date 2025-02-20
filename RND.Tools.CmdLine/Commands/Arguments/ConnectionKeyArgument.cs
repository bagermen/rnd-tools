using System.CommandLine;

namespace RND.Tools.CmdLine.Commands.Arguments;

internal class ConnectionKeyArgument : Argument<string>
{
	public ConnectionKeyArgument(
		string name = "connectionKey",
		string? description = "Specifies connection key"
		) : base(
			name: name,
			description: description
		)
	{}
}