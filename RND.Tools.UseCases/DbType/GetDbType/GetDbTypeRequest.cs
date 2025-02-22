using MediatR;
using DbTypeEnum = RND.Tools.Core.Enums.DbType;

namespace RND.Tools.UseCases.DbType.GetDbType;

public record GetDbTypeRequest() : IRequest<DbTypeEnum?>;
