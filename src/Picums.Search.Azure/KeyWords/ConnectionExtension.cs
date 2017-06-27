namespace Picums.Search.Azure.KeyWords
{
    public static class ConnectionExtension
    {
        public static ISearchForParameter And(this ISearchForParameter first, ISearchForParameter second)
            => new And(first, second);
    }
}