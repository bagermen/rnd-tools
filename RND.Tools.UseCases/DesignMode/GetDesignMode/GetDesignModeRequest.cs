using MediatR;

namespace RND.Tools.UseCases.DesignMode.GetDesignMode;

public record GetDesignModeRequest : IRequest<bool>;