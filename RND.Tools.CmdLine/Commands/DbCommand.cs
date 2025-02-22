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
		Add(new DbTypeCommand());

		AddGlobalOption(new AssemblyPathOption
		{
			IsRequired = true,
		});
	}
}
