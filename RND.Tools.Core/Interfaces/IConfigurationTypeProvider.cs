namespace RND.Tools.Core.Interfaces;

public interface IConfigurationTypeProvider
{
	Type DBConfigurationSectionType { get; }
	Type PostgreSqlSecurityEngineType { get; }
	Type PostgreSqlExecutorType { get; }
	Type PostgreSqlEngineType { get; }
	Type PostgreSqlMetaEngineType { get; }
	Type PostgreSqlMetaScriptType { get; }
	Type PostgreSqlTypeConverterType { get; }
	Type MSSqlSecurityEngineType { get; }
	Type MSSqlExecutorType { get; }
	Type MSSqlEngineType { get; }
	Type MSSqlMetaEngineType { get; }
	Type MSSqlMetaScriptType { get; }
	Type MSSqlTypeConverterType { get; }
	Type MSSqlRetryOperationFactoryType { get; }
}