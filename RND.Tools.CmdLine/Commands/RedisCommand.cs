using System.CommandLine;

namespace RND.Tools.CmdLine.Commands;

internal class RedisCommand : Command
{
	public RedisCommand() : base(
		name: CommandNames.Redis,
		description: "Redis management"
		)
	{
	}
}
