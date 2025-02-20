using MediatR;
using RND.Tools.Core.Aggregates;

namespace RND.Tools.UseCases.DesignMode.GetDesignMode;

public class GetDesignModeHandler(ConfigurationAggregate configuration) : IRequestHandler<GetDesignModeRequest, bool>
{
	public Task<bool> Handle(GetDesignModeRequest request, CancellationToken cancellationToken)
	{
		return Task.FromResult(configuration.GetFileDesignMode());
	}
}