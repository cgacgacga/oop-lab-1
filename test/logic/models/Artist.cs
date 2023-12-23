using System.Collections.Generic;

namespace Lab2
{
    public class Artist
    {
        private string Name;
        public readonly List<Album> AlbumList;

        public Artist(string name)
        {
            this.Name = name;
            AlbumList = new List<Album>();
        }

        public void AddAlbum(Album album)
        {
            AlbumList.Add(album);
        }

        public Album GetAlbum(string name)
        {
            foreach (var album in AlbumList)
                if (album.Name.ToLower().Trim().Equals(name.ToLower().Trim()))
                    return album;

            return null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}