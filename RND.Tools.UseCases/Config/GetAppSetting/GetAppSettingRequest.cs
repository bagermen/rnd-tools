using MediatR;

namespace RND.Tools.UseCases.Config.GetAppSetting;

public record GetAppSettingRequest(string SettingKey) : IRequest<string?>;