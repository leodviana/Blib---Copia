using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Blib.Models;
using Blib.Services;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;
using Plugin.Permissions;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;
using Blib.Views;

namespace Blib.ViewModels
{
    public class LoginPage02ViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;

        private ApiService apiService;

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set
            {
                SetProperty(ref _senha, value);

            }
        }

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                SetProperty(ref isRunning, value);

            }
        }

        private string _usuarioid;
        public string Usuarioid
        {
            get { return _usuarioid; }
            set
            {
                SetProperty(ref _usuarioid, value);

            }
        }

        public DelegateCommand NavigateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    http://api.whatsapp.com/send?1=pt_BR&phone=5500000000000
                    _navigationService.NavigateAsync("PerfilPage");
                });

            }
        }
        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    Login();// _navigationService.NavigateAsync("PerfilPage" );
                });

            }
        }
        public LoginPage02ViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            apiService = new ApiService();
        }

        private async void Login()
        {

            if (string.IsNullOrEmpty(Usuarioid))
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
                response = await apiService.Login(Usuarioid, Senha);
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
            App.usuariologado = User;
            await checa_permissao();

            // await _navigationService.NavigateAsync("MainPage", navigationParams);
            App.Current.MainPage = new MainPage();
            //await _navigationService.NavigateAsync("MainPage");
            //navigationServices.SetMainPage(User);
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
