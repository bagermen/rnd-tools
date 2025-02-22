using RND.Tools.Core.Enums;
using System.CommandLine;

namespace RND.Tools.CmdLine.Commands.Arguments;

internal class DbTypeArgument : Argument<string>
{
	public DbTypeArgument(
		string name = "value"
		) : base(
			name: name,
			description: "database type"
		)
	{
		var dbTypes = Enum.GetNames(typeof(DbType));
		foreach (var dbType in dbTypes)
		{
			this.FromAmong(dbType.ToLower());
		}
	}
}
