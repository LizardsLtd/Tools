﻿namespace Picums.Mvc.Configuration.Defaults
{
    public interface IDefault
    {
        void Apply(StartupConfigurations host, params object[] arguments);
    }
}