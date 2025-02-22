using MediatR;
using DbTypeEnum = RND.Tools.Core.Enums.DbType;

namespace RND.Tools.UseCases.DbType.GetDbType;

public class GetDbTypeHandler : IRequestHandler<GetDbTypeRequest, DbTypeEnum?>
{
	public Task<DbTypeEnum?> Handle(GetDbTypeRequest request, CancellationToken cancellationToken)
	{
		return Task.FromResult<DbTypeEnum?>(DbTypeEnum.SQLServer);
	}
}
