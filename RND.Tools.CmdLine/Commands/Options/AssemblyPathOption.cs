using System.CommandLine;
using System.IO;

namespace RND.Tools.CmdLine.Commands.Options;

internal class AssemblyPathOption : Option<FileInfo>
{
	public AssemblyPathOption(
		string name = "--assembly-path",
		string? description = "Specifies the path to the DLL assembly or application"
		) : base(
			name: name,
			description: description
		)
	{
		OptionExtensions.ExistingOnly(this);
	}
}
