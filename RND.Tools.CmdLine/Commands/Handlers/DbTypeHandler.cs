using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using RND.Tools.CmdLine.Configurations;
using RND.Tools.Core.Enums;
using RND.Tools.UseCases.DbType.GetDbType;
using RND.Tools.UseCases.DbType.SetDbType;

namespace RND.Tools.CmdLine.Commands.Handlers
{
	internal class DbTypeHandler(
	IServiceScopeFactory scopeFactory,
	CmdLineOptions options) : BaseHandler(scopeFactory, options)
	{
		public string? Value { get; set; }
		protected override async Task<int> InvokeInternalAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
		{
			var mediator = serviceProvider.GetRequiredService<IMediator>();

			if (Value is null)
			{
				var result = await mediator.Send(new GetDbTypeRequest(), cancellationToken);
				Console.WriteLine(result);
				return 0;
			}
			var dbType = Enum.Parse<DbType>(Value, ignoreCase: true);
			await mediator.Send(new SetDbTypeRequest(dbType), cancellationToken);

			return 0;
		}
	}
}
