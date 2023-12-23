using System.Collections.Generic;

namespace Lab2
{
    public class Album
    {
        public readonly List<Track> TrackList;
        public readonly Genre Genre;
        public readonly short Year;
        public readonly string Name;
        public readonly Artist Artist;

        public Album(string albumName, short year, Genre genre, Artist artist)
        {
            Name = albumName;
            this.Year = year;
            this.Genre = genre;
            this.Artist = artist;
            
            TrackList = new List<Track>();
        }

        public void AddTrack(Track track)
        {
            TrackList.Add(track);
        }

        public override string ToString()
        {
            return $"{Name} : {Artist.ToString()}";
        }
    }
}