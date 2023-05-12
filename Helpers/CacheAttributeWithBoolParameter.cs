using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SAMS.WebUI.Helpers
{

    namespace CachinginMVC.Common
    {
        public class CacheAttributeWithBoolParameter : OutputCacheAttribute
        {
            public CacheAttributeWithBoolParameter(string cacheProfileName)
            {
                OutputCacheSettingsSection cacheSettings = (OutputCacheSettingsSection)WebConfigurationManager.GetSection("system.web/caching/outputCacheSettings");
                OutputCacheProfile cacheProfile = cacheSettings.OutputCacheProfiles[cacheProfileName];
                Duration = cacheProfile.Duration;
                VaryByParam = cacheProfile.VaryByParam;
                VaryByCustom = cacheProfile.VaryByCustom;
            }
        }
    }
}