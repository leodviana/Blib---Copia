using Blib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blib.helper
{
    public class Settings
    {
        // this is the default static instance you'd use like string text = Settings.Default.SomeSetting;
        public readonly static Settings Default = new Settings();

        public usuario usuarioLogado { get; set; } // add setting properties as you wish

        public bool umavez { get; set; } // add setting properties as you wish
    }
}
