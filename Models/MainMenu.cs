using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.WebUI.Models
{
    public class MainMenu
    {
        public string MenuName { get; set; }
        public string Class { get; set; }
        public string Text { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool Encoded { get; set; }

    }
}