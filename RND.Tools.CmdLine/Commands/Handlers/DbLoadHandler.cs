using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RND.Tools.CmdLine.Configurations;
using RND.Tools.UseCases.Db.Load;

namespace RND.Tools.CmdLine.Commands.Handlers;

internal class DbLoadHandler(
IServiceScopeFactory scopeFactory,
CmdLineOptions options) : BaseHandler(scopeFactory, options)
{
	public FileInfo BackupFile { get; set; } = default!;
	protected override async Task<int> InvokeInternalAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
	{
		var mediator = serviceProvider.GetRequiredService<IMediator>();
		await mediator.Send(new LoadDbRequest(BackupFile), cancellationToken);

		return 0;
	}
}
