using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Blib.Models;
using Blib.Services;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;

namespace Blib.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private ApiService apiService;
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            apiService = new ApiService();
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

       

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set
            {
                SetProperty(ref _senha, value);

            }
        }
        public DelegateCommand NavigateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    Login(); //_navigationService.NavigateAsync("/MasterPage/NavigationPage/" +);
                });

            }
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
                await _dialogService.DisplayAlertAsync("Erro","Dispositivo sem Conexâo", "OK");
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
            navigationParams.Add("usuario" , User);
            await _navigationService.NavigateAsync("MainPage", navigationParams);
            //await _navigationService.NavigateAsync("MainPage");
            //navigationServices.SetMainPage(User);
        }

     
    }
}
