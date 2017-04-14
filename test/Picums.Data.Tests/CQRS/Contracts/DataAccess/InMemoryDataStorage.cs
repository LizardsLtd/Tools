using System.Collections.Generic;

namespace Picums.Data.Tests.CQRS.Contracts.DataAccess
{
	internal sealed class InMemoryDataStorage : Dictionary<string, List<object>>
	{
	}
}