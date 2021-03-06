﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;
using Xamarin.Forms.Platform.Android;
using Plugin.Permissions;
using Android.Locations;
using Xamarin.Forms;
using Android.Content;

namespace Blib.Droid
{
    [Activity(Label = "Blib", Icon = "@drawable/BliB", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation) ]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;
            
            base.OnCreate(bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("BliB");
                alert.SetMessage("Seu GPS não está ativado, por favor ative o GPS para continuar.");
                alert.SetButton("OK", (c, ev) =>
                {
                    Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                    Forms.Context.StartActivity(gpsSettingIntent);
                });
                alert.Show();
            }
            LoadApplication(new App(new AndroidInitializer()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }



    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

