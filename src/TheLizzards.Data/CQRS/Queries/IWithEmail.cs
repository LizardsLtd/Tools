using TheLizzards.Data.Types.Entites;

namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithEmail<TResult>
	{
		TResult WithEmail(Email email);
	}
}