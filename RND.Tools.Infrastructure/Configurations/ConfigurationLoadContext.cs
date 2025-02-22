using System.Reflection;
using System.Runtime.Loader;

namespace RND.Tools.Infrastructure.Configurations;

public class ConfigurationLoadContext : AssemblyLoadContext, IDisposable
{
	private AssemblyDependencyResolver resolver;

	public ConfigurationLoadContext(string dllPath, bool isCollectible) : base(Path.GetFileName(dllPath), isCollectible)
	{
		resolver = new AssemblyDependencyResolver(dllPath);
	}

	public void Dispose() => Unload();

	protected override Assembly? Load(AssemblyName assemblyName)
	{
		if (assemblyName.Name is null || !assemblyName.Name.StartsWith("BPMSoft"))
		{
			return null;
		}
		var assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
		if (assemblyPath is not null)
		{
			return LoadFromAssemblyPath(assemblyPath);
		}

		return null;
	}

	protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
	{
		var libraryPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
		if (libraryPath is not null)
		{
			return LoadUnmanagedDllFromPath(libraryPath);
		}

		return IntPtr.Zero;
	}
}
