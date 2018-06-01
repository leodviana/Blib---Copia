using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Blib.Custom_render;
using Blib.Interfaces.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TextboxCustomizadoSemBorda), typeof(RenderizadorCustomizadoTextboxSemBorda))]
namespace Blib.Interfaces.Renderers
{
    public class RenderizadorCustomizadoTextboxSemBorda : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}