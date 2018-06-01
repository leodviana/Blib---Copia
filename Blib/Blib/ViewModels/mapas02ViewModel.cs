using Blib.Custom_render;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blib.ViewModels
{
    public class mapas02ViewModel : BindableBase
    {
        public CustomMap MyMap { get; private set; }

        public mapas02ViewModel()
        {
            MyMap = new CustomMap();
        }
    }
}
