using MediatR;
using RND.Tools.Core.Aggregates;

namespace RND.Tools.UseCases.Connection.SetConnectionString;

public class SetAppSettingHandler(ConfigurationAggregate configration) : IRequestHandler<SetConnectionRequest>
{
	public Task Handle(SetConnectionRequest request, CancellationToken cancellationToken)
	{
		configration.SetConnectionString(request.ConnectionKey, request.ConnectionString);
		return Task.CompletedTask;
	}
}