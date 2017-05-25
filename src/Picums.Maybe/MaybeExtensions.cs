namespace Picums.Maybe
{
    public sealed class Maybe
    {
        public static Maybe<T> None<T>() => Maybe<T>.Nothing;

        public static Maybe<T> From<T>(T item) => (Maybe<T>)item;
    }
}