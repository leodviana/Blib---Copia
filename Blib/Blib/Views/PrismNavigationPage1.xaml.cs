using Blib.Custom_render;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Blib.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrismNavigationPage1 : NavigationPage
    {
        public PrismNavigationPage1()
        {
            InitializeComponent();
            Title = "merda";
            CustomNavigationPage.SetTitleColor(this, Color.Red);
            CustomNavigationPage.SetBarBackground(this, Device.RuntimePlatform == Device.iOS ? "monkeybackground.jpg" : "icon");
        }
    }
}
