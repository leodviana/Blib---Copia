using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Blib.Models;
using Blib.Services;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using System.Threading.Tasks;

namespace Blib.ViewModels
{
    public class PerfilPageViewModel : BindableBase
    {
        private const string emailRegex =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private ApiService apiService;
        private IPageDialogService _dialogService;
        private INavigationService _navigationService;
        private string _icone;
        public string Icone
        {
            get { return _icone; }
            set
            {
                SetProperty(ref _icone, value);

            }
        }

        private bool _isRunning;
       
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                SetProperty(ref _isRunning, value);

            }
        }

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

        private bool _valido = true;

        public bool Valido
        {
            get { return _valido; }
            set
            {
                SetProperty(ref _valido, value);
                if (Valido)

                    Icone = "salvar_ativo.png";
                else
                {
                    
                    Icone = "Pencil.png";
                }
            }
        }

        private string _dsc_nome_usuario;
        public string dsc_nome_usuario
        {
            get { return _dsc_nome_usuario; }
            set
            {
                SetProperty(ref _dsc_nome_usuario, value);

            }
        }
        

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set
            {
                SetProperty(ref _senha, value);

            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                Valido = (Regex.IsMatch(_email, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            }
        }
       

        public DelegateCommand GoToCommand
        {
            get
            {

                return new DelegateCommand(() =>
                {

                    Salva(); //_navigationService.NavigateAsync("/MasterPage/NavigationPage/" +);
                });

            }
        }

        public async void Salva()
        {

            if (string.IsNullOrEmpty(_dsc_nome_usuario))
            {
                await _dialogService.DisplayAlertAsync("Erro", "Prencha o campo Usuário!", "OK");
                // await dialogServices.ShowMessage("Erro", "Prencha o campo Usuário!");
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                await _dialogService.DisplayAlertAsync("Erro", "Prencha o campo Email!", "OK");
                // await dialogServices.ShowMessage("Erro", "Prencha o campo Usuário!");
                return;
            }
            if (string.IsNullOrEmpty(_dsc_nome_usuario))
            {
                await _dialogService.DisplayAlertAsync("Erro", "Prencha o campo Senha!", "OK");
                // await dialogServices.ShowMessage("Erro", "Prencha o campo Usuário!");
                return;
            }
            var response = new Response();
            IsRunning = true;

            if (CrossConnectivity.Current.IsConnected)
            {
                await updateLocation();
                usuario novousuario = new usuario();
                novousuario.dsc_nome_usuario = dsc_nome_usuario;
                novousuario.cod_senha = Senha;
                novousuario.dsc_email = Email;
                novousuario.cod_latitude = Latitude.ToString();
                novousuario.cod_longitude = Longitude.ToString();
                response = await apiService.Gravausuario(novousuario);
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Erro", "Dispositivo sem Conexâo", "OK");
               // await dialogServices.ShowMessage("Erro", response.Message);
                IsRunning = false;
                return;
            }
            

            if (!response.IsSuccess)
            {
                await _dialogService.DisplayAlertAsync("Erro", response.Message, "OK");
                //await dialogServices.ShowMessage("Erro", response.Message);
                IsRunning = false;
                return;
            }
            IsRunning = false;
            await _dialogService.DisplayAlertAsync("BliB", response.Message, "OK");
            Login();
            limpa_tela();
            
        }

        private async void Login()
        {

            if (string.IsNullOrEmpty(_dsc_nome_usuario))
            {
                await _dialogService.DisplayAlertAsync("Erro", "Prencha o campo Usuário!", "OK");
                // await dialogServices.ShowMessage("Erro", "Prencha o campo Usuário!");
                return;
            }


            if (string.IsNullOrEmpty(Senha))
            {
                await _dialogService.DisplayAlertAsync("Erro", "Prencha o campo Senha!", "OK");
                return;
            }

            var response = new Response();
            IsRunning = true;

            if (CrossConnectivity.Current.IsConnected)
            {
                response = await apiService.Login(_dsc_nome_usuario, Senha);
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Erro", "Dispositivo sem Conexâo", "OK");
                //await dialogServices.ShowMessage("Erro", response.Message);
                IsRunning = false;
                return;
            }
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await _dialogService.DisplayAlertAsync("Erro", response.Message, "OK");
                //await dialogServices.ShowMessage("Erro", response.Message);
                return;
            }

            var User = (usuario)response.Result;
            
            var navigationParams = new NavigationParameters();
            navigationParams.Add("usuario", User);
            helper.Settings.Default.usuarioLogado = User;
            await checa_permissao();
            await _navigationService.NavigateAsync("MainPage", navigationParams);
            //await _navigationService.NavigateAsync("MainPage");
            //navigationServices.SetMainPage(User);
        }

       // async Task FooAsync()
        async Task updateLocation()
        {
            // await _dialogService.DisplayAlertAsync("entrei no update ", "Gunna need that location", "OK");
            IsRunning = true;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    /* if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                     {
                         await _dialogService.DisplayAlertAsync("Need location", "Gunna need that location", "OK");
                        // status = await Util.CheckPermissions(Permission.Location);
                     }*/

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    if (locator.IsGeolocationEnabled)
                    {
                        var position = await locator.GetPositionAsync();
                        Latitude = position.Latitude;
                        Longitude = position.Longitude;
                       
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Location Denied", "Can not continue, try again.", "OK");
                    }
                }
                else if (status != PermissionStatus.Unknown)
                {
                    string teste = "Location Denied,Can not continue, try again.";
                }




            }
            catch (Exception ex)
            {

                IsRunning = false;
            }
            IsRunning = false;
        }
        private void limpa_tela()
        {
            dsc_nome_usuario = "";
            Senha = "";
            Email = "";
        }

        private bool Valida_campos()
        {
            return false;
        }

        public PerfilPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            apiService = new ApiService();
        }
        private async Task checa_permissao()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    /* if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                     {
                         await _dialogService.DisplayAlertAsync("Need location", "Gunna need that location", "OK");
                        // status = await Util.CheckPermissions(Permission.Location);
                     }*/

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {


                }
                else if (status != PermissionStatus.Unknown)
                {
                    //string teste = "";
                    await _dialogService.DisplayAlertAsync("Erro", "Location Denied,Can not continue, try again.", "OK");

                }




            }
            catch (Exception ex)
            {

                IsRunning = false;
            }
            IsRunning = false;
        }
    }
}
