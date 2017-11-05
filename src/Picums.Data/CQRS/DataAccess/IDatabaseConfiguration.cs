namespace Picums.Data.CQRS.DataAccess
{
    public interface IDatabaseConfiguration
    {
        string GetSetting(string key);
    }
}