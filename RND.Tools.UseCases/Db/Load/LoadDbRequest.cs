using MediatR;

namespace RND.Tools.UseCases.Db.Load;

public record LoadDbRequest(FileInfo BackupFile) : IRequest;