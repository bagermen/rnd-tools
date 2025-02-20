using MediatR;
using RND.Tools.Core.Aggregates;

namespace RND.Tools.UseCases.Config.SetAppSetting;

public class SetAppSettingHandler(ConfigurationAggregate configration) : IRequestHandler<SetAppSettingRequest>
{
	public Task Handle(SetAppSettingRequest request, CancellationToken cancellationToken)
	{
		configration.SetAppSetting(request.SettingKey, request.SettingValue);
		return Task.CompletedTask;
	}
}