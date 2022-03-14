using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Models
{
    [Table("Movies")]
    public class FavouriteMovie
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string slug { get; set; }
        public string title { get; set; }
        public string imgLink { get; set; }

    }
}
