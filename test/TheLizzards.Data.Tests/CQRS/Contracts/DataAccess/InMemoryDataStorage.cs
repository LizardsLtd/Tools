using System.Collections.Generic;

namespace TheLizzards.Data.Tests.CQRS.Contracts.DataAccess
{
	internal sealed class InMemoryDataStorage : Dictionary<string, Dictionary<string, object>>
	{
	}
}