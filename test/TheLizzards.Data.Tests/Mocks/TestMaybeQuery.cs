using TheLizzards.Data.CQRS.Queries;

namespace TheLizzards.Data.Tests.Mocks
{
	internal sealed class TestMaybeQuery : QueryByIdWithDefault<SimpleAggregateRoot>
	{
	}
}