using MediatR;
using RND.Tools.Core.Aggregates;
using RND.Tools.Core.Interfaces;

namespace RND.Tools.UseCases.Config.GetAppSetting;

public class GetAppSettingHandler(ConfigurationAggregate configration) : IRequestHandler<GetAppSettingRequest, string?>
{
	public Task<string?> Handle(GetAppSettingRequest request, CancellationToken cancellationToken)
	{
		var response = configration.GetAppSetting(request.SettingKey);
		return Task.FromResult(response);
	}
}