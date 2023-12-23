using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace Lab2
{
    public class Catalogue: SearchEngine
    {
        private List<Artist> data;
        private List<Genre> genres;
        private List<TrackCompilation> trackCompilations;

        public Catalogue()
        {
            data = new List<Artist>();
            genres = new List<Genre>();
            trackCompilations = new List<TrackCompilation>();
        }

        private TrackCompilation GetTrackCompilation(string name)
        {
            foreach (var compil in trackCompilations)
                if (Comparator.IgnoreCaseCompare(compil.Name, name))
                    return compil;

            return null;
        }

        private Artist GetArtist(string name)
        {
            foreach (var artist in data)
                if (Comparator.IgnoreCaseCompare(artist.ToString(), name))
                    return artist;

            return null;
        }

        private Genre GetGenre(string name)
        {
            foreach (var genre in genres)
                if (Comparator.IgnoreCaseCompare(genre.ToString(), name))
                    return genre;

            return null;
        }

        private void TrackAddition(string[] trackInfo, Album album)
        {
            string[] time = trackInfo[4].Trim().Split(':');
            int min, sec;
            Int32.TryParse(time[0].Trim(), out min);
            Int32.TryParse(time[1].Trim(), out sec);
            Track track = new Track(trackInfo[0].Trim(), min, sec, album);
            album.AddTrack(track);
            if (trackInfo.Length == 7)
            {
                TrackCompilation compil = GetTrackCompilation(trackInfo[6].Trim());
                if (compil != null)
                    compil.AddTrack(track);
                else
                {
                    TrackCompilation compilation = new TrackCompilation(trackInfo[6].Trim());
                    trackCompilations.Add(compilation);
                    compilation.AddTrack(track);
                }
            }
        }

        private void AlbumAddition(string[] trackInfo, Artist artist, int linenum)
        {
            short year;
            short.TryParse(trackInfo[3].Trim(), out year);

            Genre genre = GetGenre(trackInfo[5].Trim());
            if (genre != null)
            {
                Album album = new Album(trackInfo[2].Trim(), year, genre, artist);
                artist.AddAlbum(album);
                TrackAddition(trackInfo, album);
            }
            else Console.WriteLine($"Invalid genre in the track: line {linenum}");
        }

        private void LoadTracks(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                int linenum = 0;

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    linenum++;

                    string[] trackInfo = line.Split('/');
                    if (!(trackInfo.Length == 6 || trackInfo.Length == 7))
                    {
                        Console.WriteLine($"Invalid input tracks data: line {linenum}");
                        continue;
                    }

                    Artist artist;
                    if ((artist = GetArtist(trackInfo[1].Trim())) != null)
                    {
                        Album album;
                        if ((album = artist.GetAlbum(trackInfo[2].Trim())) != null)
                            TrackAddition(trackInfo, album);
                        else AlbumAddition(trackInfo, artist, linenum);
                    }
                    else
                    {
                        artist = new Artist(trackInfo[1].Trim());
                        data.Add(artist);
                        AlbumAddition(trackInfo, artist, linenum);
                    }
                }
            }
        }

        private void LoadGenres(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                int linenum = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    linenum++;
                    string[] lines = line.Split('/');

                    if (lines.Length == 2 && lines[0].Trim().ToLower().Equals("g") ||
                        lines.Length == 3 && lines[0].Trim().ToLower().Equals("s"))
                    {
                        if (lines.Length == 2)
                            genres.Add(new Genre(lines[1].Trim()));
                        else
                        {
                            Genre genre = new Genre(lines[2].Trim());
                            genres.Add(genre);
                            Genre baseG = GetGenre(lines[1].Trim());
                            if (baseG != null)
                                baseG.AddSubgenre(genre);
                            else Console.WriteLine($"Invalid genres info (base genre doesn't exist): line {linenum}");
                        }
                    }
                    else Console.WriteLine($"Invalid genres info: line {linenum}");
                }
            }
        }

        public static Catalogue Load(string songsPath, string genresPath)
        {
            Catalogue catalogue = new Catalogue();
            catalogue.LoadGenres(genresPath);
            catalogue.LoadTracks(songsPath);
            return catalogue;
        }

        public List<Artist> SearchArtists(string name)
        {
            return SearchEngine.SearchArtistsByName(name, data);
        }

        public List<Artist> SearchArtists(string name, string genre)
        {
            try
            {
                return SearchEngine.SearchArtistsByGenre(genre, genres, SearchArtists(name));
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<Artist> SearchArtistsByGenre(string genre)
        {
            try
            {
                return SearchEngine.SearchArtistsByGenre(genre, genres, data);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<Album> SearchAlbums(string name)
        {
            return SearchEngine.SearchAlbumsByName(name, data);
        }

        public List<Album> SearchAlbums(int year)
        {
            return SearchEngine.SearchAlbumsByYear(year, data);
        }

        public List<Album> SearchAlbumsByGenre(string genre)
        {
            try
            {
                return SearchEngine.SearchAlbumsByGenre(genre, genres, data);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<Album> SearchAlbums(string genre, string albumName)
        {
            try
            {
                return SearchEngine.SearchAlbumsByGenre(genre, genres,
                    SearchEngine.SearchAlbumsByName(albumName, data));
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<Album> SearchAlbums(string genre, int year)
        {
            try
            {
                return SearchEngine.SearchAlbumsByGenre(genre, genres, SearchEngine.SearchAlbumsByYear(year, data));
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<Album> SearchAlbums(int year, string albumName)
        {
            return SearchEngine.SearchAlbumsByName(albumName, SearchEngine.SearchAlbumsByYear(year, data));
        }

        public List<Album> SearchAlbums(string genre, string albumName, int year)
        {
            try
            {
                return SearchEngine.SearchAlbumsByGenre(genre, genres, SearchAlbums(year, albumName));
            }
            catch (Exception)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<Track> SearchTracks(string name)
        {
            return SearchEngine.SearchTracksByName(name, data);
        }

        public List<Track> SearchTracks(int year)
        {
            return SearchEngine.SearchTracksByYear(year, data);
        }

        public List<Track> SearchTracksByGenre(string genre)
        {
            try
            {
                return SearchEngine.SearchTracksByGenre(genre, genres, data);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<Track> SearchTracks(string name, int year)
        {
            return SearchEngine.SearchTracksByName(name, SearchEngine.SearchTracksByYear(year, data));
        }

        public List<Track> SearchTracks(int year, string genre)
        {
            try
            {
                return SearchEngine.SearchTracksByGenre(genre, genres, SearchEngine.SearchTracksByYear(year, data));
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<Track> SearchTracks(string name, string genre)
        {
            try
            {
                return SearchEngine.SearchTracksByName(name, SearchEngine.SearchTracksByGenre(genre, genres, data));
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<Track> SearchTracks(string name, string genre, int year)
        {
            try
            {
                return SearchEngine.SearchTracksByGenre(genre, genres, SearchTracks(name, year));
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public List<TrackCompilation> SearchTrackCompilations(string artist)
        {
            return SearchEngine.SearchTrackCompilationsByArtist(artist, trackCompilations);
        }

        public List<TrackCompilation> SearchTrackCompilationsByGenre(string genre)
        {
            return SearchEngine.SearchTrackCompilationsByGenre(genre, trackCompilations);
        }

        public List<TrackCompilation> SearchTrackCompilations(string artist, string genre)
        {
            return SearchEngine.SearchTrackCompilationsByArtist(artist,
                SearchEngine.SearchTrackCompilationsByGenre(genre, trackCompilations));
        }
    }
}