using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMovieClassDemo.Models
{
    internal class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{MovieId}: {Name} ({Year}) - Genre: {Genre}";
        }
    }
}
