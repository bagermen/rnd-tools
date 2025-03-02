using RND.Tools.CmdLine.Commands.Arguments;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands;

public class DbLoadCommand : Command
{
	public DbLoadCommand() : base(CommandNames.DbLoad, "Load database from backup")
	{
		var backupFileArgument = new BackupFileArgument
		{
			Arity = ArgumentArity.ExactlyOne
		};

		backupFileArgument.ExistingOnly();
		AddArgument(backupFileArgument);
	}
}
