﻿using Prism.Unity;
using Blib.Views;
using Xamarin.Forms;
using Blib.Models;
using Prism.Navigation;

namespace Blib
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        public static usuario usuariologado { get; set; }
        public App() : this(null) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
                           

                    
            if (usuariologado == null)
            {
                helper.Settings.Default.umavez = true;
               
                await NavigationService.NavigateAsync("/PrismNavigationPage2/LoginPage02");
               
            }
            else
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("usuario", usuariologado);
                helper.Settings.Default.usuarioLogado = usuariologado;
                await NavigationService.NavigateAsync("MainPage", navigationParams);
              
            }

        }

        protected override void OnStart()
        {

            string mensagem = "";
        }

        protected override void OnResume()
        {

            string mensagem = "";
        }
        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<PrismNavigationPage1>();

            Container.RegisterTypeForNavigation<CustomPage>();
            Container.RegisterTypeForNavigation<Pesquisa01>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<LoginPage02>();
            Container.RegisterTypeForNavigation<PerfilPage>();
            Container.RegisterTypeForNavigation<ListaPerfilPage>();
            Container.RegisterTypeForNavigation<PrismNavigationPage2>();
            Container.RegisterTypeForNavigation<mapas02>();
        }
    }
}
