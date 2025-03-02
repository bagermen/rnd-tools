using System.CommandLine;

namespace RND.Tools.CmdLine.Commands.Arguments;

internal class BackupFileArgument : Argument<FileInfo>
{
	public BackupFileArgument(
		string name = "backupFile",
		string? description = "Path to the backup file"
		) : base(
			name: name,
			description: description
		)
	{}
}