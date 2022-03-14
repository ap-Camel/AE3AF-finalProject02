using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Essentials;

using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Views;
namespace MovieApp.ViewModels
{
    public class FavouritMoviesViewModel : BaseViewModel
    {

        public ObservableCollection<FavouriteMovie> movies { get; }

        private FavouriteMovie selectedMovie;

        public FavouriteMovie SelectedMovie
        {
            get { return selectedMovie; }
            set { SetProperty(ref selectedMovie, value); }
        }

        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }

        public async Task LoadData()
        {
            List<FavouriteMovie> moviesList = await App.DbContext.GetItems<FavouriteMovie>();

            movies.Clear();

            foreach (FavouriteMovie movie in moviesList)
            {
                movies.Add(movie);
            }
        }

        public async Task GoToDetails()
        {

            if (selectedMovie != null)
            {
                MovieDetailsViewModel vm = new MovieDetailsViewModel();
                MovieDetailsView page = new MovieDetailsView();

                page.BindingContext = vm;

                vm.SelectedMovie = await ApiServices.getMovie(selectedMovie.slug);

                await App.Current.MainPage.Navigation.PushAsync(page);
            }
        }

        public FavouritMoviesViewModel()
        {
            movies = new ObservableCollection<FavouriteMovie>();

            LoadDataCommand = new Command(async () => await LoadData());
            GoToDetailsCommand = new Command(async () => await GoToDetails());
        }


    }
}
