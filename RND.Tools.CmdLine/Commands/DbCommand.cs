using RND.Tools.CmdLine.Commands.Options;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands;

internal class DbCommand : Command
{
	public DbCommand() : base(
		name: CommandNames.Db,
		description: "Database management"
		)
	{
		AddGlobalOption(new AssemblyPathOption
		{
			IsRequired = true,
		});
	}
}
