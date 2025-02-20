using MediatR;

namespace RND.Tools.UseCases.Connection.SetConnectionString;

public record SetConnectionRequest(string ConnectionKey, string ConnectionString) : IRequest;