using System.Collections.Generic;
using System.Xml;

namespace Lab2
{
    public class TrackCompilation
    {
        private List<Track> tracks;
        public readonly string Name;
        public readonly List<Genre> Genres;
        public readonly List<Artist> Artists;

        public TrackCompilation(string name)
        {
            this.Name = name;
            tracks = new List<Track>();
            Genres = new List<Genre>();
            Artists = new List<Artist>();
        }

        public void AddTrack(Track track)
        {
            tracks.Add(track);
            AddArtist(track);
            AddGenre(track);
        }

        private bool HasGenre(Genre genre)
        {
            foreach (var gen in Genres)
                if (Comparator.IgnoreCaseCompare(gen.ToString(), genre.ToString()))
                    return true;

            return false;
        }
        
        private bool HasArtist(Artist artist)
        {
            foreach (var art in Artists)
                if (art.ToString().ToLower().Equals(artist.ToString().ToLower()))
                    return true;

            return false;
        }
        
        private void AddGenre(Track track)
        {
            if (!HasGenre(track.Album.Genre))
                Genres.Add(track.Album.Genre);
        }

        private void AddArtist(Track track)
        {
            if (!HasArtist(track.Album.Artist))
                Artists.Add(track.Album.Artist);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}