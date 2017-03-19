using System;
using System.Collections.Generic;
using System.Text;

namespace TheLizzards.Mvc.Configuration.Defaults
{
    public sealed class FeaturesDefaults : IDefaultMvcConfiguration
    {
        public void Apply(MvcRegistry registry)
        {
            registry.Conventions.AddControllerConvention<Fet>
        }
    }
}
