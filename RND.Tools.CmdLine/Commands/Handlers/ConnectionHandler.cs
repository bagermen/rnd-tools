using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RND.Tools.CmdLine.Configurations;
using RND.Tools.UseCases.Connection.GetConnectionString;
using RND.Tools.UseCases.Connection.SetConnectionString;

namespace RND.Tools.CmdLine.Commands.Handlers;

internal class ConnectionsHandler(
	IServiceScopeFactory scopeFactory,
	CmdLineOptions options) : BaseHandler(scopeFactory, options)
{
	public string ConnectionKey { get; set; } = default!;
	public string? ConnectionString { get; set; }
	protected override async Task<int> InvokeInternalAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
	{
		var mediator = serviceProvider.GetRequiredService<IMediator>();

		if (string.IsNullOrEmpty(ConnectionString))
		{
			var result = await mediator.Send(new GetConnectionRequest(ConnectionKey), cancellationToken);
			Console.WriteLine(result);
			return 0;
		}

		await mediator.Send(new SetConnectionRequest(ConnectionKey, ConnectionString), cancellationToken);

		return 0;
	}
}