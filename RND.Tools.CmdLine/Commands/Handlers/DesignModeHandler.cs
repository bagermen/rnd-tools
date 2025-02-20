using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RND.Tools.CmdLine.Configurations;
using RND.Tools.UseCases.DesignMode.GetDesignMode;
using RND.Tools.UseCases.DesignMode.SetDesignMode;

namespace RND.Tools.CmdLine.Commands.Handlers;

internal class DesignModeHandler(
	IServiceScopeFactory scopeFactory,
	CmdLineOptions options) : BaseHandler(scopeFactory, options)
{
	public bool? DesignModeValue { get; set; }
	protected override async Task<int> InvokeInternalAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
	{
		var mediator = serviceProvider.GetRequiredService<IMediator>();

		if (DesignModeValue is null)
		{
			var result = await mediator.Send(new GetDesignModeRequest(), cancellationToken);
			Console.WriteLine(result);
			return 0;
		}

		await mediator.Send(new SetDesignModeRequest((bool) DesignModeValue), cancellationToken);
		return 0;
	}
}