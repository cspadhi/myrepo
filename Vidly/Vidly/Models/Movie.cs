using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }

        public DateTime ReleaseDate { get; set; }
        public DateTime RecordDate { get; set; }

        public int QuantityInStock { get; set; }
    }
}