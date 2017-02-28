using TheLizzards.Data.Types;

namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithEmail<TResult>
	{
		TResult WithEmail(Email email);
	}
}