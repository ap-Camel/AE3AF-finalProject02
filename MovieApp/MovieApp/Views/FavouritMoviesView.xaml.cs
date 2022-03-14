using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MovieApp.ViewModels;


namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritMoviesView : ContentPage
    {
        FavouritMoviesViewModel vm;
        public FavouritMoviesView()
        {
            InitializeComponent();

            vm = new FavouritMoviesViewModel();

            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            
                base.OnAppearing();

                await Task.Run(() => vm.LoadDataCommand.Execute(null));
            
            vm.SelectedMovie = null;
        }
    }
}