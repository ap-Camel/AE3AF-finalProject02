using System;
using System.Collections.Generic;
using System.Text;

using TraktNet;
using TraktNet.Objects.Authentication;
using TraktNet.Objects.Get.Movies;
using TraktNet.Objects.Get.Shows;
using TraktNet.Objects.Get.Collections;
using TraktNet.Requests.Parameters;
using TraktNet.Responses;
using TraktNet.Services;
using TraktNet.Modules;
using TraktNet.Objects.Basic;
using MovieApp.Models;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace MovieApp.Services
{
    public class ApiServices
    {

        public static TraktClient client = new TraktClient("7781df713fce7739296b675bf47f65a363efefb977a89b9e392290baacce8e6c", "9a5abc7ac5b00b8794581eb84fc85357c1ac0c8de12f089a1d19b28c7252f993");
        

        public static async Task<List<Movie>> getSearchResults(string search = "martian")
        {
            string key = "de3dff0de182786b656774d5d4423636";

            List<Movie> movies = new List<Movie>();
            Movie movieInfo = new Movie();

            TraktExtendedInfo info = new TraktExtendedInfo();
            info.Full = true;

            var movie = TraktNet.Enums.TraktSearchResultType.Movie;

            TraktPagedResponse<ITraktSearchResult> searchResult = await client.Search.GetTextQueryResultsAsync(movie, search, null, null, info, null);

            if (searchResult)
            {

                foreach (ITraktSearchResult s in searchResult)
                {
                    string movieId = s.Movie.Ids.Tmdb.ToString();

                    Result result = new Result();

                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        string fullUrl = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={key}&language=en-US";
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                       

                        HttpResponseMessage response = await client.GetAsync(fullUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<Result>(json);
                        }
                    }

                    movies.Add(new Movie { title = s.Movie.Title,
                                           slug = s.Movie.Ids.Slug,
                                           rating = s.Movie.Rating.ToString(),
                                           geners = s.Movie.Genres,
                                           dateReleased = $"{s.Movie.Year}",
                                           status = s.Movie.Status.ToString(),
                                           trailerLink = s.Movie.Trailer,
                                           pageLink = s.Movie.Homepage,
                                           certificate = s.Movie.Certification,
                                           overview = s.Movie.Overview,
                                           imgLink = $"https://image.tmdb.org/t/p/w500/{result.poster_path}" });
                }
            }

            return movies;
        }

        public static async Task<List<Movie>> getPopulae()
        {
            string key = "de3dff0de182786b656774d5d4423636";

            List<Movie> movies = new List<Movie>();
            Movie movieInfo = new Movie();

            TraktExtendedInfo info = new TraktExtendedInfo();
            info.Full = true;


            TraktPagedResponse<ITraktTrendingMovie> topMoviesSearch = await client.Movies.GetTrendingMoviesAsync(info, null);

            if (topMoviesSearch)
            {

                foreach (ITraktTrendingMovie s in topMoviesSearch)
                {
                    string movieId = s.Movie.Ids.Tmdb.ToString();

                    Result result = new Result();

                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        string fullUrl = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={key}&language=en-US";
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.GetAsync(fullUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<Result>(json);
                        }
                    }

                    movies.Add(new Movie
                    {
                        title = s.Movie.Title,
                        slug = s.Movie.Ids.Slug,
                        rating = s.Movie.Rating.ToString(),
                        geners = s.Movie.Genres,
                        dateReleased = $"{s.Movie.Year}",
                        status = s.Movie.Status.ToString(),
                        trailerLink = s.Movie.Trailer,
                        pageLink = s.Movie.Homepage,
                        certificate = s.Movie.Certification,
                        overview = s.Movie.Overview,
                        imgLink = $"https://image.tmdb.org/t/p/w500/{result.poster_path}"
                    });
                }
            }

            return movies;
        }

        public static async Task<Movie> getMovie(string Id)
        {
            string key = "de3dff0de182786b656774d5d4423636";

            Movie movieInfo = new Movie();

            TraktExtendedInfo info = new TraktExtendedInfo();
            info.Full = true;


            TraktResponse<ITraktMovie> search = await client.Movies.GetMovieAsync(Id, new TraktExtendedInfo().SetFull());

            if (search)
            {
                

                ITraktMovie movie = search.Value;

                string movieId = movie.Ids.Tmdb.ToString();

                Result result = new Result();

                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string fullUrl = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={key}&language=en-US";
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.
                    Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(fullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<Result>(json);
                    }
                }

                movieInfo.title = movie.Title;
                movieInfo.slug = movie.Ids.Slug;
                movieInfo.rating = movie.Rating.ToString();
                movieInfo.geners = movie.Genres;
                movieInfo.dateReleased = $"{movie.Year}";
                movieInfo.status = movie.Status.ToString();
                movieInfo.trailerLink = movie.Trailer;
                movieInfo.pageLink = movie.Homepage;
                movieInfo.certificate = movie.Certification;
                movieInfo.overview = movie.Overview;
                movieInfo.imgLink = $"https://image.tmdb.org/t/p/w500/{result.poster_path}";
            }

            return movieInfo;
        }


    }
}
