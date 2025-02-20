using MediatR;
using RND.Tools.Core.Aggregates;

namespace RND.Tools.UseCases.DesignMode.SetDesignMode;

public class SetDesignModeHandler(ConfigurationAggregate configuration) : IRequestHandler<SetDesignModeRequest>
{
	public Task Handle(SetDesignModeRequest request, CancellationToken cancellationToken)
	{
		configuration.SetFileDesignMode(request.DesignModeValue);
		return Task.CompletedTask;
	}
}