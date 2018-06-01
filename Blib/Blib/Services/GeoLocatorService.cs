using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Internals;

namespace Blib.Services
{
    public class GeoLocatorService
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public async Task GeoLocation()
        {

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var location = await locator.GetPositionAsync();
            Latitude = location.Latitude;
            Longitude = location.Longitude;
          

        }

             /*   public async Task<Position> PosicaoAtualAsync()
        {
            var locator = CrossGeolocator.Current;


            if (locator.IsGeolocationEnabled && locator.IsGeolocationAvailable)
            {
                locator.DesiredAccuracy = 50;
                if (!locator.IsListening)
                    await locator.StartListeningAsync(TimeSpan.FromMinutes(5),10);
                
                var position = await locator.GetPositionAsync();
                
                if (position != null)
                {
                    Latitude = position.Latitude;
                    Longitude = position.Longitude;
                    return new Position(position.Latitude, position.Longitude);
                }
            }

            //await DisplayAlert("Ops", "Não conseguimos obter sua localização, por favor, verifique sua conexão e se o seu GPS está ativo.", "OK");
            return new Position(0, 0);

        }*/


    }
}
