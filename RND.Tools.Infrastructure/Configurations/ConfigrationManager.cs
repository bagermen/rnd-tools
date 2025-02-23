using RND.Tools.Core.Enums;
using RND.Tools.Core.Interfaces;
using RND.Tools.Infrastructure.Configurations.Factories;
using System.Configuration;
using System.Reflection;
using System.Runtime.Loader;
using SysConfiguration = System.Configuration.Configuration;

namespace RND.Tools.Infrastructure.Configurations;

internal class ConfigrationManager : IConfigrationManager, IDisposable
{
	private readonly SysConfiguration config;
	private readonly AssemblyLoadContext.ContextualReflectionScope contextualReflectionScope;
	private readonly IEnumerable<IConfigurationFactory> configurationFactories;

	internal ConfigrationManager(
		ICmdLineOptions cmdOptions,
		ConfigurationLoadContext alc,
		IEnumerable<IConfigurationFactory> configurationFactories
	)
	{
		this.configurationFactories = configurationFactories;
		contextualReflectionScope = alc.EnterContextualReflection();
		config = ConfigurationManager.OpenExeConfiguration(cmdOptions.AssemblyPath);
	}
	public string? GetConnectionString(string key)
	{
		var connectionStringSettings = config.ConnectionStrings.ConnectionStrings[key];
		return connectionStringSettings?.ConnectionString;
	}

	public string? GetAppSetting(string key)
	{
		return config.AppSettings.Settings[key]?.Value;
	}

	public void SetConnectionString(string key, string value)
	{
		var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

		if (connectionStringsSection.ConnectionStrings[key] != null)
		{
			connectionStringsSection.ConnectionStrings[key].ConnectionString = value;
		}
		else
		{
			connectionStringsSection.ConnectionStrings.Add(new ConnectionStringSettings(key, value));
		}

		config.Save(ConfigurationSaveMode.Modified);
		ConfigurationManager.RefreshSection("connectionStrings");
	}

	public void SetAppSetting(string key, string value)
	{
		var appSettingsSection = (AppSettingsSection)config.GetSection("appSettings");

		if (appSettingsSection.Settings[key] != null)
		{
			appSettingsSection.Settings[key].Value = value;
		}
		else
		{
			appSettingsSection.Settings.Add(key, value);
		}

		config.Save(ConfigurationSaveMode.Modified);
		ConfigurationManager.RefreshSection("appSettings");
	}

	public void RemoveConnectionString(string key)
	{
		var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

		if (connectionStringsSection.ConnectionStrings[key] != null)
		{
			connectionStringsSection.ConnectionStrings.Remove(key);
		}

		config.Save(ConfigurationSaveMode.Modified);
		ConfigurationManager.RefreshSection("connectionStrings");
	}

	public void RemoveAppSetting(string key)
	{
		var appSettingsSection = (AppSettingsSection)config.GetSection("appSettings");

		if (appSettingsSection.Settings[key] != null)
		{
			appSettingsSection.Settings.Remove(key);
		}

		config.Save(ConfigurationSaveMode.Modified);
		ConfigurationManager.RefreshSection("appSettings");
	}

	public string? GetFileDesignMode()
	{
		var sectionGroup = config.GetSectionGroup("bpmsoft");

		var designModeSection = sectionGroup.Sections["fileDesignMode"];

		// Use reflection to get the Enabled property
		var enabledProperty = designModeSection.GetType().GetProperty("Enabled");

		bool? enabled = enabledProperty!.GetValue(designModeSection) as bool?;
		return enabled?.ToString();
	}

	public void SetFileDesignMode(string value)
	{
		var sectionGroup = config.GetSectionGroup("bpmsoft");

		var designModeSection = sectionGroup.Sections["fileDesignMode"];

		// Get the BPMSoft.Core.FileDesignModeSection type
		var designModeSectionType = designModeSection.GetType();

		// Use reflection to set the Enabled property
		var enabledProperty = designModeSectionType.GetProperty("Enabled");
		enabledProperty!.SetValue(designModeSection, bool.Parse(value));

		config.Save(ConfigurationSaveMode.Modified);
		ConfigurationManager.RefreshSection("bpmsoft");
	}

	public Type? GetDBExecutorType()
	{
		var dbGeneral = config.GetSection("bpmsoft/db/general");

		return dbGeneral?.GetType().GetProperty("ExecutorType")!.GetValue(dbGeneral) as Type;
	}
	public string? GetDBConnectionStringName()
	{
		var dbGeneral = config.GetSection("bpmsoft/db/general");

		return dbGeneral?.GetType().GetProperty("ConnectionStringName")!.GetValue(dbGeneral) as string;
	}

	public void SetDbConnection(string connectionStringName, DbType dbType)
	{
		var dbSection = dbType switch
		{
			DbType.PostgreSQL => configurationFactories.OfType<PostgreSQLConnectionFactory>().First().CreateSection(),
			DbType.SQLServer => configurationFactories.OfType<MSSQLConnectionFactory>().First().CreateSection(),
			_ => throw new NotImplementedException()
		};

		dbSection.GetType().GetProperty("ConnectionStringName")!.SetValue(dbSection, connectionStringName);
		RemoveSection("bpmsoft/db", "general");
		AddSection("bpmsoft/db", "general", dbSection);
	}

	public void UpdateQuartzToDb(string connectionStringName, DbType dbType)
	{
		var quartzSectionName = "quartzConfig";
		var sectionGroup = config.GetSection(quartzSectionName);
		var quartzElements = (ConfigurationElementCollection) sectionGroup.GetType().GetProperty("Schedulers")!.GetValue(sectionGroup)!;

		foreach (ConfigurationElement quartzElement in quartzElements)
		{
			var props = (ConfigurationElementCollection) quartzElement.GetType().GetProperty("Props")!.GetValue(quartzElement)!;

			foreach (ConfigurationElement prop in props)
			{
				var propType = prop.GetType();
				var propName = (string) propType.GetProperty("Key")!.GetValue(prop)!;

				if (propName == "quartz.dataSource.SchedulerDb.connectionStringName")
				{
					SetQuartzPropElementProp(prop, "value", connectionStringName);
				}
				else if (propName == "quartz.dataSource.SchedulerDb.provider")
				{
					SetQuartzPropElementProp(prop, "value", dbType == DbType.PostgreSQL ? "Npgsql" : "SqlServer");
				}
				else if (propName == "quartz.jobStore.driverDelegateType")
				{
					SetQuartzPropElementProp(prop, "value", dbType == DbType.PostgreSQL
						? "Quartz.Impl.AdoJobStore.PostgreSQLDelegate, Quartz"
						: "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz"
					);
				}
			}
		}
		config.Save(ConfigurationSaveMode.Modified);
		ConfigurationManager.RefreshSection(quartzSectionName);
	}
	private void SetQuartzPropElementProp(ConfigurationElement element, string propertyName, object value)
	{
		var setPropertyValueMethod = element.GetType().GetMethod("SetPropertyValue", BindingFlags.Instance | BindingFlags.NonPublic);
		var propertiesProperty = element.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic)!;
		var properties = propertiesProperty.GetValue(element) as ConfigurationPropertyCollection;

		if (properties != null && properties.Contains(propertyName))
		{
			setPropertyValueMethod!.Invoke(element, [properties[propertyName], value, false]);
		}
	}

	private void AddSection(string sectionGroupName, string sectionName, ConfigurationSection section)
	{
		var sectionGroup = config.GetSectionGroup(sectionGroupName);
		if (sectionGroup != null && sectionGroup.Sections[sectionName] == null)
		{
			sectionGroup.Sections.Add(sectionName, section);
			config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(sectionGroupName);
		}
	}

	private void RemoveSection(string sectionGroupName, string sectionName)
	{
		var sectionGroup = config.GetSectionGroup(sectionGroupName);
		if (sectionGroup != null && sectionGroup.Sections[sectionName] != null)
		{
			sectionGroup.Sections.Remove(sectionName);
			config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(sectionGroupName);
		}
	}

	public void Dispose() => contextualReflectionScope.Dispose();
}
