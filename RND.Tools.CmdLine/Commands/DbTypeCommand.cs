using RND.Tools.CmdLine.Commands.Arguments;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands
{
	internal class DbTypeCommand : Command
	{
		public DbTypeCommand() : base(
			name: CommandNames.DbType,
			description: "Database type configuration management")
		{
			var dbType = new DbTypeArgument
			{
				Arity = ArgumentArity.ZeroOrOne,
			};
			AddArgument(dbType);
		}
	}
}
