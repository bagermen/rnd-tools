using Microsoft.Extensions.DependencyInjection;
using RND.Tools.CmdLine.Configurations;
using RND.Tools.Core.Enums;
using System.CommandLine.Invocation;

namespace RND.Tools.CmdLine.Commands.Handlers;

internal abstract class BaseHandler(
	IServiceScopeFactory scopeFactory,
	CmdLineOptions options) : ICommandHandler
{
	private FileInfo assemblyPath = default!;
	public FileInfo AssemblyPath
	{
		get => assemblyPath;
		set
		{
			Options.AssemblyPath = value.FullName;
			Options.AssemblyType = value.Name.Contains("WorkspaceConsole")
				? AssemblyType.WorkspaceConsole
				: AssemblyType.Main;
			assemblyPath = value;
		}
	}

	protected CmdLineOptions Options => options;
	public int Invoke(InvocationContext context)
	{
		throw new NotImplementedException();
	}

	public Task<int> InvokeAsync(InvocationContext context)
	{
		var cancellationToken = context.GetCancellationToken();
		using var scope = scopeFactory.CreateScope();

		return InvokeInternalAsync(scope.ServiceProvider, cancellationToken);
	}
	protected abstract Task<int> InvokeInternalAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);
}
