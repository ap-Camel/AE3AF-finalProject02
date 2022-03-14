using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Models
{
    
    public class Movie
    {

        

        public string slug { get; set; }
        public string title { get; set; }
        public string trailerLink { get; set; }
        public string pageLink { get; set; }
        public IEnumerable<string> geners { get; set; }
        public string certificate { get; set; }
        public string rating { get; set; }
        public string dateReleased { get; set; }
        public string status { get; set; }
        public string imgLink { get; set; }
        public string overview { get; set; }


    }
}
