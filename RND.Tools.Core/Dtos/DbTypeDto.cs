namespace RND.Tools.Core.Dtos;

public record DbTypeDto
{
	public required string ConnectionStringName { get; init; }
	public required string SecurityEngineType { get; init; }
	public required string ExecutorType { get; init; }
	public required string EngineType { get; init; }
	public required string MetaEngineType { get; init; }
	public required string MetaScriptType { get; init; }
	public required string TypeConverterType { get; init; }
	public required string BinaryPackageSize { get; init; }
	public required string CurrentSchemaName { get; init; }
}