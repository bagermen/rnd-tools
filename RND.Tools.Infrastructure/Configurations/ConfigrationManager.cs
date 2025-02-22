using RND.Tools.Core.Enums;
using RND.Tools.Core.Interfaces;
using RND.Tools.Infrastructure.Configurations.Factories;
using System.Configuration;
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

	public void SetDbConnection(DbType dbType)
	{
		var dbSection = dbType switch
		{
			DbType.PostgreSQL => configurationFactories.OfType<PostgreSQLConnectionFactory>().First().CreateSection(),
			DbType.SQLServer => configurationFactories.OfType<MSSQLConnectionFactory>().First().CreateSection(),
			_ => throw new NotImplementedException()
		};

		RemoveSection("bpmsoft/db", "general");
		AddSection("bpmsoft/db", "general", dbSection);
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
