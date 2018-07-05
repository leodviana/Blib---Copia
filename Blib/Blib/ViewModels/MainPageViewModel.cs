using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blib.Models;
using Blib.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = Xamarin.Forms.Maps.Position;
using Blib.Custom_render;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Services;
using Blib.Utils;
using Blib.Menu;
using Blib.Interface;
using Blib.Views;

namespace Blib.ViewModels
{
    public class MainPageViewModel : BindableBase , INavigationAware
    {
       
        private GeoLocatorService geoLocatorService;
        private ApiService apiservice;
        private INavigationService _navigationService;
        public static CustomMap customMap;
        private IPageDialogService _dialogService;


        private double _Latitude = 0;

        public double Latitude
        {
            get { return _Latitude; }
            set
            {
                SetProperty(ref _Latitude, value);
              
            }
        }

        private double _Longitude = 0;

        public double Longitude
        {
            get { return _Longitude; }
            set
            {
                SetProperty(ref _Longitude, value);

            }
        }

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }

        private bool _primeiravez = true;

        public bool Primeiravez
        {
            get { return _primeiravez; }
            set { SetProperty(ref _primeiravez, value); }
        }

        public usuario _usuarioLogado ; 



        public DelegateCommand NavigateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                   
                });

            }
        }
        public MainPageViewModel(INavigationService navigationService ,  IPageDialogService dialogService)
        {
            Primeiravez  = (bool)helper.Settings.Default.umavez;
            _dialogService = dialogService;
            _navigationService = navigationService;
          
            _usuarioLogado  = (usuario)helper.Settings.Default.usuarioLogado;
            geoLocatorService = new GeoLocatorService();
            apiservice = new ApiService();
           
            IsRunning = true;
              updateLocation();
              getusuarios();
            IsRunning = false;
                /*Task.Run(async() =>
                {



                });*/
           


            if (Primeiravez)
            {
                MessagingCenter.Subscribe<retorno>(this, "UserName", (model) =>
                {
                    IsRunning = true;
                    updateLocation();
                    getusuarios();
                    /*Task.Run(async () =>
                    {
                        
                        
                    });*/
                    IsRunning = false;


                });

                MessagingCenter.Subscribe<retorno>(this, "UserName2", (model) =>
                {
                    MessagingCenter.Unsubscribe<retorno>(this, "UserName2");
                    MessagingCenter.Unsubscribe<retorno>(this, "UserName");
                    App.usuariologado = null;
                   //_navigationService.GoBackAsync();
                   fecha_listen();
                  // _navigationService.NavigateAsync("LoginPage02");
                  Page nova = Navegacao_aux.GetMainPage();
                    App.Current.MainPage = nova;
                    //App.Current.MainPage = new NavigationPage(new MasterPage());
                    helper.Settings.Default.umavez = true;

                });
                // Primeiravez = true;
                helper.Settings.Default.umavez = false;
            }
            
        }

      
      
        private async Task fecha_listen()
        {        
            await StopListening();
        }

        private async Task getusuarios()
        {
           
            await Getusuariocomposicao();
        }
        public DelegateCommand SairCommand
        {
            get
            {
                return new DelegateCommand( () =>
                {

                    Sair();
                });

            }
        }

        private async void Sair()
        {
           
            
        }
        

        private async Task Getusuariocomposicao()
        {
            IsRunning = true;

            var usuarioscomposicao = await apiservice.Usuarioscomposicao();

            //customMap.CustomPins = new List<CustomPin> () ;
            customMap.CustomPins.Clear();
            customMap.Pins.Clear();
            int i = 1 ;
            foreach (var user in usuarioscomposicao)
            {
                if (_usuarioLogado.flg_desenvolvedor=="S")
                //if (_usuarioLogado.flg_desenvolvedor == "S")
                {

                    var pin = new CustomPin();
                    pin.Position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(user.cod_latitude), Convert.ToDouble(user.cod_longitude));
                    pin.Type = Xamarin.Forms.Maps.PinType.Generic;
                    pin.Label = user.dsc_nome_usuario;
                    pin.Address = "Av. xxxxxxx";

                    string mensagem = "&text = Ola!%20Estou%20entrando%20em%20contato%20através%20do%20BLIB";
                    pin.Url = "http://api.whatsapp.com/send?1=pt_BR&phone=558599890253" + mensagem ;
                               
                    customMap.CustomPins.Add(pin);
                }
                else
                {
                    if (user.isn_usuario == _usuarioLogado.isn_usuario)
                    {
                        
                        var pin = new CustomPin();
                        pin.Position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(user.cod_latitude), Convert.ToDouble(user.cod_longitude));
                        pin.Type = Xamarin.Forms.Maps.PinType.Generic;
                        pin.Label = user.dsc_nome_usuario;
                        pin.Address = "Av. xxxxxxx";
                        //pin.Url = "www.BliB.com";
                        string mensagem = "&text = Ola!%20Estou%20entrando%20em%20contato%20através%20do%20BLIB";
                        pin.Url = "http://api.whatsapp.com/send?1=pt_BR&phone=558599890253" + mensagem;
                        customMap.CustomPins.Add(pin);

                    }
                    else
                    {
                        var pin = new CustomPin();
                        pin.Position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(user.cod_latitude), Convert.ToDouble(user.cod_longitude));
                        pin.Type = Xamarin.Forms.Maps.PinType.Generic;
                        pin.Label = "Blib No. " +i ;
                        pin.Address = "Av. xxxxxxx";
                        //pin.Url = "www.BliB.com";
                        string mensagem = "&text = Ola!%20Estou%20entrando%20em%20contato%20através%20do%20BLIB";
                        pin.Url = "http://api.whatsapp.com/send?1=pt_BR&phone=558599890253" + mensagem;

                        customMap.CustomPins.Add(pin);
                        i = i + 1;
                    }
                        
                }
            }
            foreach (var pino in customMap.CustomPins)
            {
                customMap.Pins.Add(pino);
            }
            
            IsRunning = false;
          //  await _dialogService.DisplayAlertAsync("depois do preenchimento do mapa ", "Gunna need that location", "OK");
        }
               

        private async Task updateLocation()
        {
           // await _dialogService.DisplayAlertAsync("entrei no update ", "Gunna need that location", "OK");
            IsRunning = true;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                  
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    await StartListening();
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    if (locator.IsGeolocationEnabled)
                    {
                        var position = await locator.GetPositionAsync();
                        Latitude = position.Latitude;
                        Longitude = position.Longitude;
                        await UpdateCustomerLocation();
                        customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(.3)));
                      
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Location Denied", "Can not continue, try again.", "OK");
                    }
                }
                else if (status != PermissionStatus.Unknown)
                {
                    string teste  = "Location Denied,Can not continue, try again.";
                }
                
                
                             

            }
            catch (Exception ex)
            {

                IsRunning = false;
            }
            IsRunning = false;
        }
        private void Current_PositionChanged(object sender, PositionEventArgs e)
        {
           // _dialogService.DisplayAlertAsync("entrei no posisao mudada", "currtent change", "OK");
            if (e.Position != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var position = e.Position;
                    Latitude = position.Latitude;
                    Longitude = position.Longitude;
                    //MyMap.CustomPins.Clear();
                    /* MyMap.Pins.Add(new Xamarin.Forms.Maps.Pin
                     {
                         Position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude),
                         Type = Xamarin.Forms.Maps.PinType.Place,
                         Label = "My Location"
                     });
                     */
                    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(.3)));
                        
                    UpdateCustomerLocation();
                    getusuarios();
                   
                    //Getusuariocomposicao();
                    
                });
            }
        }


        private async Task UpdateCustomerLocation()
        {
            IsRunning = true;

            //ATUALIZAÇÃO NA API
          //  _usuarioLogado.cod_latitude = Latitude.ToString();
          //  _usuarioLogado.cod_longitude = Longitude.ToString();
             var response = await apiservice.Gravaposicao(_usuarioLogado);
            
            IsRunning = false;

            
        }
        

        private async void Locator()
        {
            IsRunning = true;
            await geoLocatorService.GeoLocation();
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(geoLocatorService.Latitude,geoLocatorService.Longitude), Distance.FromKilometers(.3)));
            IsRunning = false;
            
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _usuarioLogado = (usuario)parameters["usuario"];
            string nome = _usuarioLogado.dsc_nome_usuario;
            
            updateLocation();
           
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            string teste = "";
           
        }

        
        public async Task StartListening()
        {
            if (CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(30), 50, true);

            CrossGeolocator.Current.PositionChanged += PositionChanged;
            CrossGeolocator.Current.PositionError += PositionError;
        }

        // como tranformar esse metodo assicrono 
        private void PositionChanged(object sender, PositionEventArgs e)
        {

            
                        
            //If updating the UI, ensure you invoke on main thread
            var position = e.Position;
            var output = "Full: Lat: " + position.Latitude + " Long: " + position.Longitude;
            output += "\n" + $"Time: {position.Timestamp}";
            output += "\n" + $"Heading: {position.Heading}";
            output += "\n" + $"Speed: {position.Speed}";
            output += "\n" + $"Accuracy: {position.Accuracy}";
            output += "\n" + $"Altitude: {position.Altitude}";
            output += "\n" + $"Altitude Accuracy: {position.AltitudeAccuracy}";
            Latitude = position.Latitude;
            Longitude = position.Longitude;
            _usuarioLogado.cod_latitude = position.Latitude.ToString();
            _usuarioLogado.cod_longitude = position.Longitude.ToString();
            UpdateCustomerLocation();
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(.3)));
           // updateLocation();
            //UpdateCustomerLocation();
            //Getusuariocomposicao();
            getusuarios();
        }

        private void PositionError(object sender, PositionErrorEventArgs e)
        {
            Debug.WriteLine(e.Error);
            //Handle event here for errors
        }

        async Task StopListening()
        {
            if (!CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StopListeningAsync();

            CrossGeolocator.Current.PositionChanged -= PositionChanged;
            CrossGeolocator.Current.PositionError -= PositionError;
        }

    }
}
