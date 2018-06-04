using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Blib.Custom_render;
using Blib.iOS.Renderers;

[assembly: ExportRenderer(typeof(TextboxCustomizadoSemBorda), typeof(RenderizadorCustomizadoTextboxSemBorda))]
namespace Blib.iOS.Renderers
{
    public class RenderizadorCustomizadoTextboxSemBorda : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }
    }
    
}