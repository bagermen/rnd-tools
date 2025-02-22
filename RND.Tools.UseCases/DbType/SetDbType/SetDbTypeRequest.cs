using MediatR;
using DbTypeEnum = RND.Tools.Core.Enums.DbType;
namespace RND.Tools.UseCases.DbType.SetDbType;

public record SetDbTypeRequest(DbTypeEnum DbType) : IRequest;