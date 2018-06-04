using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Blib.iOS.Renderes
{
    public class RenderizadorCustomizadoTextboxsemBorda : EntryRenderer
    {


        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }

    }
}
