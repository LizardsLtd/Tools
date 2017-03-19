namespace TheLizzards.Mvc.Configuration.Defaults
{
    public interface IDefault
    {
        void Apply(IDefaultsHost host);
    }
}