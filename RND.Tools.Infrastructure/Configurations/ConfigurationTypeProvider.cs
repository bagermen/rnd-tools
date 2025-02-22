using RND.Tools.Core.Interfaces;
using System.Reflection;

namespace RND.Tools.Infrastructure.Configurations;

internal class ConfigurationTypeProvider(ConfigurationLoadContext alc) : IConfigurationTypeProvider
{
	private Assembly PSQLAssembly => alc.LoadFromAssemblyName(new AssemblyName("BPMSoft.DB.PostgreSql"));
	private Assembly MSSQLAssembly => alc.LoadFromAssemblyName(new AssemblyName("BPMSoft.DB.MSSql"));
	private Assembly CoreAssembly => alc.LoadFromAssemblyName(new AssemblyName("BPMSoft.Core"));

	public Type DBConfigurationSectionType => CoreAssembly.GetType("BPMSoft.Core.DB.DBConfigurationSection")!;
	public Type PostgreSqlSecurityEngineType => PSQLAssembly.GetType("BPMSoft.DB.PostgreSql.PostgreSqlSecurityEngine")!;
	public Type PostgreSqlExecutorType => PSQLAssembly.GetType("BPMSoft.DB.PostgreSql.PostgreSqlExecutor")!;
	public Type PostgreSqlEngineType => PSQLAssembly.GetType("BPMSoft.DB.PostgreSql.PostgreSqlEngine")!;
	public Type PostgreSqlMetaEngineType => PSQLAssembly.GetType("BPMSoft.DB.PostgreSql.PostgreSqlMetaEngine")!;
	public Type PostgreSqlMetaScriptType => PSQLAssembly.GetType("BPMSoft.DB.PostgreSql.PostgreSqlMetaScript")!;
	public Type PostgreSqlTypeConverterType => PSQLAssembly.GetType("BPMSoft.DB.PostgreSql.PostgreSqlTypeConverter")!;
	public Type MSSqlSecurityEngineType => MSSQLAssembly.GetType("BPMSoft.DB.MSSql.MSSqlSecurityEngine")!;
	public Type MSSqlExecutorType => MSSQLAssembly.GetType("BPMSoft.DB.MSSql.MSSqlExecutor")!;
	public Type MSSqlEngineType => MSSQLAssembly.GetType("BPMSoft.DB.MSSql.MSSqlEngine")!;
	public Type MSSqlMetaEngineType => MSSQLAssembly.GetType("BPMSoft.DB.MSSql.MSSqlMetaEngine")!;
	public Type MSSqlMetaScriptType => MSSQLAssembly.GetType("BPMSoft.DB.MSSql.MSSqlMetaScript")!;
	public Type MSSqlTypeConverterType => MSSQLAssembly.GetType("BPMSoft.DB.MSSql.MSSqlTypeConverter")!;
	public Type MSSqlRetryOperationFactoryType => MSSQLAssembly.GetType("BPMSoft.DB.MSSql.MSSqlRetryOperationFactory")!;
}