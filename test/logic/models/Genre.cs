using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    public class Genre
    {
        private string Name;
        private List<Genre> subgenres;

        public Genre(string name)
        {
            this.Name = name;
            subgenres = new List<Genre>();
        }

        public void AddSubgenre(Genre subgenre)
        {
            subgenres.Add(subgenre);
        }

        //subgenre == genre, but genre != subgenre
        public bool IsSubgenreOf(Genre genre)
        {
            return this.Equals(genre) || genre.subgenres.Any(x => x.IsSubgenreOf(this));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
