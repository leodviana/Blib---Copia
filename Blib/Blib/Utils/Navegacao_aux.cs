using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Blib.Utils
{
     static class Navegacao_aux
    {
        public static INavigation Navigation { get; private set; }

        public static Page GetMainPage()
        {
            var rootPage = new NavigationPage(new Views.LoginPage02());

            Navigation = rootPage.Navigation;
            rootPage.BarBackgroundColor = Color.FromRgb(64, 95, 126);
            // rootPage.BarTextColor = Color.FromHex(AppColors.GRAY);

            return rootPage;
        }
    }
}
