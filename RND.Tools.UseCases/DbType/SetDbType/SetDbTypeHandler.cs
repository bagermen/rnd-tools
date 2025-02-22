using MediatR;

namespace RND.Tools.UseCases.DbType.SetDbType;

public class SetDbTypeHandler : IRequestHandler<SetDbTypeRequest>
{
	Task IRequestHandler<SetDbTypeRequest>.Handle(SetDbTypeRequest request, CancellationToken cancellationToken)
	{
		Console.WriteLine($"Setting DbType to {request.DbType}");
		return Task.CompletedTask;
	}
}