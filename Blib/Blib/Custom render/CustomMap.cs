﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Blib.Custom_render
{
    public class CustomMap : Map
    {
       public List<CustomPin> CustomPins { get; set; }
            
    }
}
