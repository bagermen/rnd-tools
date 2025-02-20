using MediatR;

namespace RND.Tools.UseCases.Connection.GetConnectionString;

public record GetConnectionRequest(string ConnectionKey) : IRequest<string?>;