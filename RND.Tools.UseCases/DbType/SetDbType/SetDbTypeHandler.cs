using MediatR;
using RND.Tools.Core.Aggregates;

namespace RND.Tools.UseCases.DbType.SetDbType;

public class SetDbTypeHandler(ConfigurationAggregate configuration) : IRequestHandler<SetDbTypeRequest>
{
	Task IRequestHandler<SetDbTypeRequest>.Handle(SetDbTypeRequest request, CancellationToken cancellationToken)
	{
		var currentDbType = configuration.GetDbType();

		if (currentDbType != request.DbType)
		{
			configuration.SetDbType(request.DbType);
		}

		return Task.CompletedTask;
	}
}