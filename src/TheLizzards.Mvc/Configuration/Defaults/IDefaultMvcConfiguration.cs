using System;
using System.Collections.Generic;
using System.Text;

namespace TheLizzards.Mvc.Configuration.Defaults
{
    public interface IDefaultMvcConfiguration
    {
        void Apply(MvcRegistry registry);
    }
}
