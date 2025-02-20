using RND.Tools.Core.Enums;
using System.Text.Json.Serialization;

namespace RND.Tools.CmdLine.Configurations;

internal class CmdLineSettings
{
	public static string CmdLine = nameof(CmdLine);
	public string AssemblyPath { get; set; } = default!;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public AssemblyType? AssemblyType { get; set; } = default!;
}
