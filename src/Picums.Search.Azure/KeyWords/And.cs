using System.Linq;

namespace Picums.Search.Azure.KeyWords
{
    public sealed class And : ISearchForParameter
    {
        private const string AndText = " && ";
        private readonly ISearchForParameter[] searchForParameters;

        public And(params ISearchForParameter[] searchForParameters)
        {
            this.searchForParameters = searchForParameters;
        }

        public string GetSearchCommmand()
            => this.searchForParameters.Aggregate(string.Empty, (prev, current) => prev + AndText + current);
    }
}