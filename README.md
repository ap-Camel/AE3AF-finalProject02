# AE3AF-finalProject02

## task description

-	There is a menu with several options: Search movies, View Favorites
-	In Search movies, there is a Search Bar and a CollectionView. 
-	The user introduces a search term (example: Batman) in the Search Bar and the results appear in the CollectionView
-	When the user taps on a result, you will navigate to a Details page.
-	In the Details page, you will display more information about the movie (this depends on the API that you use).
-	Also, in Details page: 
o	Include an option to mark the movie as Favorite 
o	Include an option to share information about it (choose at least two: email, SMS, social network, …)
o	If the movie includes a YouTube link, add an option to visit it
-	In View My Favorites, all movies that have been marked as favorite will appear here under a CollectionView.
-	When the user clicks on a favorite movie, it will navigate to the details page where more information about it will be displayed (it will also be possible to share info about it).

General requirements:
-	Use MVVM. Very few code will be allowed in the View
-	You can use Xamarin.Essentials for the Share part
-	You can use Xamarin.Forms Shell for the menu part
-	Marking a movie as favorite means that you will save its information in a local Sqlite database (You can save it to a remote database instead if you want, but you’ll need to set up an API for that-).
-	Use styles defined in App.xaml or inside every page 

Here are a few APIs that you can use to get Movies information:
-	The Open Movie Database http://www.omdbapi.com/
-	The MovieDB https://www.themoviedb.org/documentation/api 
-	TasteDive https://tastedive.com/read/api
-	Trakt https://trakt.docs.apiary.io/#introduction/website-media-links 
-	YTS https://yts.mx/api 

## app description

* the app show the current most popular movies when it first opens up
* user can search for a movie they want
* user can select a movie and add that movie to their favourits 
* user can also share info about the movie, watch the trailer, and go to the movie's main page

## solution description

* Xamarin forms is used for making this app
* for the architecturem, MVVM is used
* the app connects with two APIs ([tmdb](https://www.themoviedb.org/) and [trakt.tv](https://trakt.tv/))
* the app uses an external library for interacting with trakt.tv api ==> [link to github](https://github.com/henrikfroehling/Trakt.NET)
* the app uses local SQL storage for saving the favourites

## app pictures 

#### main page

![main page](https://github.com/ap-Camel/AE3AF-finalProject02/blob/master/Pictures/Screenshot%202022-03-31%20111854.png)


#### selecting a movie

![selecting a movie](https://github.com/ap-Camel/AE3AF-finalProject02/blob/master/Pictures/Screenshot%202022-03-31%20111948.png)


#### searching for a movie

![searching for a movie](https://github.com/ap-Camel/AE3AF-finalProject02/blob/master/Pictures/Screenshot%202022-03-31%20112413.png)


#### favourites tab

![favourites tab](https://github.com/ap-Camel/AE3AF-finalProject02/blob/master/Pictures/Screenshot%202022-03-31%20112541.png)
