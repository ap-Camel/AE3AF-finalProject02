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
    public class MovieDetailsViewModel : BaseViewModel
    {

        private Movie selectedMovie;

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set { SetProperty(ref selectedMovie, value); }
        }

        private FavouriteMovie selectedFavoutiteMovie;

        public FavouriteMovie SelectedFavoutiteMovie
        {
            get { return selectedFavoutiteMovie; }
            set { SetProperty(ref selectedFavoutiteMovie, value); }
        }


        public ICommand FavouritingCommand { private set; get; }
        public ICommand GoToTailerCommand { private set; get; }
        public ICommand GoToPageCommand { private set; get; }
        public ICommand ShareTextCommand { private set; get; }

        public async Task Favouriting()
        {

            List<FavouriteMovie> list = new List<FavouriteMovie>();
            list = await App.DbContext.GetItems<FavouriteMovie>();

            bool check = true;

            FavouriteMovie movie = new FavouriteMovie();

            movie.slug = selectedMovie.slug;
            movie.title = selectedMovie.title;
            movie.imgLink = selectedMovie.imgLink;



            foreach (FavouriteMovie Movies in list)
            {
                if (movie.slug == Movies.slug)
                {
                    check = false;
                    movie.Id = Movies.Id;
                }
            }

            if (check)
            {
                bool op = false;

                op = await App.DbContext.AddItem<FavouriteMovie>(movie);

                if (op)
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "the movie was added to favourits", "Ok");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("failiur!", "the movie was not added", "Ok");
                }
            }

            if (!check)
            {
                if (SelectedMovie != null)
                {
                    bool op = await App.Current.MainPage.DisplayAlert("Delete", "the movie already in favourite, do you want to revome it?", "Yes", "no");

                    if (op)
                    {

                        op = await App.DbContext.DeleteItem<FavouriteMovie>(movie);


                        if (op)
                        {
                            await App.Current.MainPage.DisplayAlert("Success!", "The movie was removed from favourite", "Ok");
                        }
                        else
                            await App.Current.MainPage.DisplayAlert("Error!", "The movie was not removed", "Ok");
                    }
                }

            }        }

        public async Task gotoTrailer()
        {
            await Browser.OpenAsync(selectedMovie.trailerLink, BrowserLaunchMode.SystemPreferred);
        }

        public async Task gotoPage()
        {
            await Browser.OpenAsync(selectedMovie.pageLink, BrowserLaunchMode.SystemPreferred);
        }

        public async Task ShareText()
        {

            string text = $" Im currently watching the movie:  {selectedMovie.title}";
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }
        

        public MovieDetailsViewModel()
        {
            FavouritingCommand = new Command(async () => await Favouriting());
            GoToTailerCommand = new Command(async () => await gotoTrailer());
            GoToPageCommand = new Command(async () => await gotoPage());
            ShareTextCommand = new Command(async () => await ShareText());




            //if(selectedMovie.imgLink == null)
            //{
            //    selectedMovie.imgLink = selectedFavoutiteMovie.imgLink;
            //}

        }



    }
}
