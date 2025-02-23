using MediatR;
using RND.Tools.Core.Aggregates;
using DbTypeEnum = RND.Tools.Core.Enums.DbType;

namespace RND.Tools.UseCases.DbType.GetDbType;

public class GetDbTypeHandler(ConfigurationAggregate configuration) : IRequestHandler<GetDbTypeRequest, DbTypeEnum?>
{
	public Task<DbTypeEnum?> Handle(GetDbTypeRequest request, CancellationToken cancellationToken)
	{
		return Task.FromResult(configuration.GetDbType());
	}
}
