using MediatR;
using RND.Tools.Core.Aggregates;

namespace RND.Tools.UseCases.Connection.GetConnectionString;

public class GetAppSettingHandler(ConfigurationAggregate configration) : IRequestHandler<GetConnectionRequest, string?>
{
	public Task<string?> Handle(GetConnectionRequest request, CancellationToken cancellationToken)
	{
		var response = configration.GetConnectionString(request.ConnectionKey);
		return Task.FromResult(response);
	}
}