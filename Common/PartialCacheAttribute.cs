using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Configuration;

namespace MVC5.Common
{
    public class PartialCacheAttribute : OutputCacheAttribute
    {
        public PartialCacheAttribute(string cacheProfileName){
        OutputCacheSettingsSection cacheSettingsSection = 
            (OutputCacheSettingsSection)WebConfigurationManager.GetSection("system.web/caching/outputCacheSettings");
        OutputCacheProfile profiles = cacheSettingsSection.OutputCacheProfiles[cacheProfileName];
        Duration = profiles.Duration;
        VaryByParam = profiles.VaryByParam;
        }
    }
}