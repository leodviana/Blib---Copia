using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blib.Models;
using Blib.ViewModels;
using SlideOverKit;
using Xamarin.Forms;

namespace Blib.Menu
{
    public class QuickInnerMenuView : SlideMenuView
    {
        public QuickInnerMenuView(MenuOrientation orientation)
        {
            //botao refresh

            Image icon = new Image();
            icon.Source = ImageSource.FromFile("refresh");
            icon.WidthRequest =30;
            icon.HeightRequest = 30;
            var iconTap = new TapGestureRecognizer();
            iconTap.Tapped += (object sender, EventArgs e) =>
            {
                MessagingCenter.Send(new retorno()
                {
                    Message = "botao01",
                   
                }, "UserName");
            };
            icon.GestureRecognizers.Add(iconTap);
            //botao home 
            Image icon2 = new Image();
            icon2.Source = ImageSource.FromFile("sair");
            icon2.WidthRequest = 30;
            icon2.HeightRequest = 30;
            var iconTap2 = new TapGestureRecognizer();
            iconTap2.Tapped += (object sender, EventArgs e) =>
            {
                MessagingCenter.Send(new retorno()
                {
                    Message = "botao02",

                }, "UserName2");
            };

            icon2.GestureRecognizers.Add(iconTap2);
            var mainLayout = new StackLayout
            {
                Spacing = 15,
                Children = {

                    icon,
                    icon2,
                    /*new Image {
                        Source = "MessageFilled.png",
                        WidthRequest = 25,
                        HeightRequest = 25,
                    },
                    new Image {
                        Source = "Settings.png",
                        WidthRequest = 25,
                        HeightRequest = 25,
                    },*/
                }
            };
            // In this case the IsFullScreen must set false
            this.IsFullScreen = false;
            this.BackgroundViewColor = Color.Transparent;

            // You must set BackgroundColor, 
            // and you cannot put another layout with background color cover the whole View
            // otherwise, it cannot be dragged on Android
            //this.BackgroundColor = Color.FromHex("#0AE3D6");
            this.BackgroundColor = Color.FromHex("#74889F");
            this.MenuOrientations = orientation;
            if (orientation == MenuOrientation.BottomToTop)
            {
                mainLayout.Orientation = StackOrientation.Vertical;
                mainLayout.Children.Insert(0, new Image
                {
                    Source = "DoubleUp.png",
                    WidthRequest = 25,
                    HeightRequest = 25,
                });
                mainLayout.Padding = new Thickness(0, 5);
                // In this case, you must set both WidthRequest and HeightRequest.
                this.WidthRequest = 50;
                this.HeightRequest = 200;

                // A little bigger then DoubleUp.png image size, used for user drag it.
                this.DraggerButtonHeight = 30;

                // In this menu direction you must set LeftMargin.
                this.LeftMargin = 100;

            }

            if (orientation == MenuOrientation.TopToBottom)
            {
                mainLayout.Orientation = StackOrientation.Vertical;
                mainLayout.Children.Insert(4, new Image
                {
                    Source = "DoubleDown_White.png",
                    WidthRequest = 25,
                    HeightRequest = 25,
                });
                mainLayout.Padding = new Thickness(0, 5);
                this.WidthRequest = 50;
                this.HeightRequest = 200;
                this.DraggerButtonHeight = 40;
                this.LeftMargin = 100;

            }

            if (orientation == MenuOrientation.LeftToRight)
            {
                mainLayout.Orientation = StackOrientation.Horizontal;
                mainLayout.Children.Insert(4, new Image
                {
                    Source = "DoubleRight.png",
                    WidthRequest = 25,
                    HeightRequest = 25,
                });
                mainLayout.Padding = new Thickness(5, 0);
                this.WidthRequest = 200;
                this.HeightRequest = 50;
                // In this case, it should be DraggerButtonWidth not DraggerButtonHeight
                this.DraggerButtonWidth = 40;

                // In this menu direction you must set TopMargin.
                this.TopMargin = 30;

            }

            if (orientation == MenuOrientation.RightToLeft)
            {
                mainLayout.Orientation = StackOrientation.Horizontal;
                mainLayout.Children.Insert(0, new Image
                {
                    Source = "DoubleLeft.png",
                    WidthRequest = 25,
                    HeightRequest = 25,
                });
                mainLayout.Padding = new Thickness(5, 0);
                this.WidthRequest = 200;
                this.HeightRequest = 50;
                this.DraggerButtonWidth = 30;
                this.TopMargin = 30;
            }
            Content = mainLayout;
        }
    }
}

