using MediatR;
using RND.Tools.Core.Aggregates;
using DbTypeEnum = RND.Tools.Core.Enums.DbType;

namespace RND.Tools.UseCases.Db.Load;

public class LoadDbHandler(DbAggregate db) : IRequestHandler<LoadDbRequest>
{
	public async Task Handle(LoadDbRequest request, CancellationToken cancellationToken)
	{
		await db.Load(request.BackupFile, cancellationToken);
	}
}