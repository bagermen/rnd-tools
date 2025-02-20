using RND.Tools.Core.Enums;

namespace RND.Tools.Core.Interfaces;

public interface ICmdLineOptions
{
	string AssemblyPath { get; }
	AssemblyType AssemblyType { get; }
}
