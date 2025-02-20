using Microsoft.Extensions.Options;
using RND.Tools.Core.Enums;
using RND.Tools.Core.Interfaces;
using System.Runtime.CompilerServices;

namespace RND.Tools.CmdLine.Configurations;

internal class CmdLineOptions(IOptionsMonitor<CmdLineSettings> options, ConfigProvider cmdConfigProvider) : ICmdLineOptions
{
	public string AssemblyPath
	{
		get => options.CurrentValue.AssemblyPath;
		set => SetValue(value.ToString());
	}

	public AssemblyType AssemblyType
	{
		get => options.CurrentValue.AssemblyType ?? AssemblyType.Main;
		set => SetValue(value.ToString());
	}

	private void SetValue(string value, [CallerMemberName] string? propertyName = null)
	{
		cmdConfigProvider.Set($"{CmdLineSettings.CmdLine}:{propertyName}", value);
	}
}
