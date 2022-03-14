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
    public class MoviesListViewModel : BaseViewModel
    {

        public ObservableCollection<Movie> movies { get; }

        private Movie selectedMovie;

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set { SetProperty(ref selectedMovie, value); }
        }

        private string search;

        public string Search
        {
            get { return search; }
            set { SetProperty(ref search, value); }
        }

        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }

        public async Task LoadData(string search)
        {
            IsBusy = true;

            if (search.Length == 0)
            {

                List<Movie> list = await ApiServices.getPopulae();
                movies.Clear();

                foreach (Movie movie in list)
                {
                    movies.Add(movie);
                }
            }
            else
            {
                List<Movie> list = await ApiServices.getSearchResults(search);
                movies.Clear();

                foreach (Movie movie in list)
                {
                    movies.Add(movie);
                }
            }

            IsBusy = false;
        }

        public async Task GoToDetails()
        {

            if (selectedMovie != null)
            {
                MovieDetailsViewModel vm = new MovieDetailsViewModel();
                MovieDetailsView page = new MovieDetailsView();

                page.BindingContext = vm;

                vm.SelectedMovie = selectedMovie;


                await App.Current.MainPage.Navigation.PushAsync(page);
            }
        }


        public MoviesListViewModel()
        {
            movies = new ObservableCollection<Movie>();
            search = "";

            LoadDataCommand = new Command(async () => await LoadData(search));
            GoToDetailsCommand = new Command(async () => await GoToDetails());
        }

    }
}
