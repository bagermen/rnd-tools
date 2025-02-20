using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RND.Tools.CmdLine.Configurations;
using RND.Tools.UseCases.Config.SetAppSetting;
using RND.Tools.UseCases.Config.GetAppSetting;

namespace RND.Tools.CmdLine.Commands.Handlers;

internal class SettingHandler(
	IServiceScopeFactory scopeFactory,
	CmdLineOptions options) : BaseHandler(scopeFactory, options)
{
	/// <summary>
	/// LoadAssemblyFromByteArray
	/// </summary>
	public string SettingKey { get; set; } = default!;
	public string? SettingValue { get; set; }
	protected override async Task<int> InvokeInternalAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
	{
		var mediator = serviceProvider.GetRequiredService<IMediator>();

		if (string.IsNullOrWhiteSpace(SettingValue))
		{
			var result = await mediator.Send(new GetAppSettingRequest(SettingKey), cancellationToken);
			Console.WriteLine(result);
			return 0;
		}

		await mediator.Send(new SetAppSettingRequest(SettingKey, SettingValue), cancellationToken);

		return 0;
	}
}