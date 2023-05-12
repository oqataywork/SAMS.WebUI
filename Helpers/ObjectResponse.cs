using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.WebUI.Helpers
{
    public struct ObjectResponse
    {
        public string error { get; set; }
        public object Response { get; set; }
    }
}