using MediatR;

namespace RND.Tools.UseCases.Config.SetAppSetting;

public record SetAppSettingRequest(string SettingKey, string SettingValue) : IRequest;