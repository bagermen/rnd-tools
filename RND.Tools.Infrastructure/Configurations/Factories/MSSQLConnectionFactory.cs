using RND.Tools.Core.Interfaces;
using System.Configuration;

namespace RND.Tools.Infrastructure.Configurations.Factories;

internal class MSSQLConnectionFactory(IConfigurationTypeProvider typeProvider) : BaseConfigurationFactory(typeProvider)
{
	private Type SecurityEngineType => TypeProvider.MSSqlSecurityEngineType;
	private Type ExecutorType => TypeProvider.MSSqlExecutorType;
	private Type EngineType => TypeProvider.MSSqlEngineType;
	private Type MetaEngineType => TypeProvider.MSSqlMetaEngineType;
	private Type MetaScriptType => TypeProvider.MSSqlMetaScriptType;
	private Type TypeConverterType => TypeProvider.MSSqlTypeConverterType;
	private Type RetryDBOperationFactoryType => TypeProvider.MSSqlRetryOperationFactoryType;
	private bool EnableRetryDBOperations => false;
	private int BinaryPackageSize => 1048576;
	private string ConnectionStringName => "db";
	private string CurrentSchemaName => "dbo";
	private bool IsCaseInsensitive => true;
	private bool EnableSqlLog => false;
	private int SqlLogQueryTimeElapsedThreshold => 5000;
	private int SqlLogRowsThreshold => 100;
	private bool? UseOrderNullsPosition => false;
	private int MaxEntitySchemaNameLength => 128;

	public override ConfigurationSection CreateSection()
	{
		var configType = TypeProvider.DBConfigurationSectionType;
		var config = (ConfigurationSection)Activator.CreateInstance(configType)!;
		configType.GetProperty("SecurityEngineType")!.SetValue(config, SecurityEngineType);
		configType.GetProperty("ExecutorType")!.SetValue(config, ExecutorType);
		configType.GetProperty("EngineType")!.SetValue(config, EngineType);
		configType.GetProperty("MetaEngineType")!.SetValue(config, MetaEngineType);
		configType.GetProperty("MetaScriptType")!.SetValue(config, MetaScriptType);
		configType.GetProperty("TypeConverterType")!.SetValue(config, TypeConverterType);
		configType.GetProperty("RetryDBOperationFactoryType")!.SetValue(config, RetryDBOperationFactoryType);
		configType.GetProperty("EnableRetryDBOperations")!.SetValue(config, EnableRetryDBOperations);
		configType.GetProperty("BinaryPackageSize")!.SetValue(config, BinaryPackageSize);
		configType.GetProperty("ConnectionStringName")!.SetValue(config, ConnectionStringName);
		configType.GetProperty("CurrentSchemaName")!.SetValue(config, CurrentSchemaName);
		configType.GetProperty("IsCaseInsensitive")!.SetValue(config, IsCaseInsensitive);
		configType.GetProperty("EnableSqlLog")!.SetValue(config, EnableSqlLog);
		configType.GetProperty("SqlLogQueryTimeElapsedThreshold")!.SetValue(config, SqlLogQueryTimeElapsedThreshold);
		configType.GetProperty("SqlLogRowsThreshold")!.SetValue(config, SqlLogRowsThreshold);
		configType.GetProperty("UseOrderNullsPosition")!.SetValue(config, UseOrderNullsPosition);
		configType.GetProperty("MaxEntitySchemaNameLength")!.SetValue(config, MaxEntitySchemaNameLength);

		return config;
	}
}