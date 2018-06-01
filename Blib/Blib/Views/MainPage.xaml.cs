
using Blib.Custom_render;
using Blib.Menu;
using Blib.ViewModels;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SlideOverKit;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace Blib.Views
{
    public partial class MainPage : MenuContainerPage
    {
        public MainPage()
        {
            InitializeComponent();
            SlideMenu = new QuickInnerMenuView(MenuOrientation.LeftToRight);

            NavigationPage.SetHasNavigationBar(this, false);
            
            //muda_posicao();
             
           
            
            MainPageViewModel.customMap = customMap;
            MainPageViewModel.customMap.CustomPins = new List<CustomPin>();

        }

        /* private async void getLocations()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            //var location = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
            var location = await locator.GetPositionAsync();
            var position = new Position(location.Latitude, location.Longitude);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(.3)));
        }*/


    }
}
