using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;

namespace Blib.ViewModels
{
    public class ListaPerfilPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        public DelegateCommand NavigateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    _navigationService.NavigateAsync("PerfilPage");
                });

            }
        }
        public ListaPerfilPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            // apiService = new ApiService();
        }
    }
}
