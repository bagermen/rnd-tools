using System.Data.Common;

namespace RND.Tools.Infrastructure.Db.ConnectionStrings;

public interface IConnectionStringGetter
{
	DbConnectionStringBuilder GetConnectionStringBuilder();
}