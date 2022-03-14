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
    public partial class MoviesListView : ContentPage
    {
        MoviesListViewModel vm;
        public MoviesListView()
        {
            InitializeComponent();

            vm = new MoviesListViewModel();

            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            if (vm.movies.Count == 0)
            {
                base.OnAppearing();

                await Task.Run(() => vm.LoadDataCommand.Execute(null));
            }
            vm.SelectedMovie = null;
        }
    }
}