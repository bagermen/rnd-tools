namespace RND.Tools.Core.Interfaces;

public interface IDbLoader
{
	Task Load(FileInfo backupFile, CancellationToken cancellationToken);
}