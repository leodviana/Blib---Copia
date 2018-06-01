using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blib.Custom_render
{
    
        public class CustomPin : Xamarin.Forms.Maps.Pin
        {
        public CustomPin()
        {
           // MapPin = new Xamarin.Forms.Maps.Pin();
        }
        public string Url { get; set; }
       // public Xamarin.Forms.Maps.Pin MapPin { get; set; }
    }
    
}
