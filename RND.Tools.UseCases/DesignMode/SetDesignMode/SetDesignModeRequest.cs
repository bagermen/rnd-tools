using MediatR;

namespace RND.Tools.UseCases.DesignMode.SetDesignMode;

public record SetDesignModeRequest(bool DesignModeValue) : IRequest;