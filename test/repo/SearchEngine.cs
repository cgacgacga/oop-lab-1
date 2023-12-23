﻿using System;
using System.Collections.Generic;
//using System.Management.Instrumentation;

namespace Lab2
{
    public class SearchEngine
    {
        protected static List<Artist> SearchArtistsByName(string name, List<Artist> artists)
        {
            List<Artist> result = new List<Artist>();

            foreach (var artist in artists)
                if (Comparator.IgnoreCaseCompare(artist.ToString(), name))
                    result.Add(artist);

            return result;
        }

        protected static List<Artist> SearchArtistsByGenre(string genre, List<Genre> genres, List<Artist> artists)
        {
            List<Artist> result = new List<Artist>();

            Genre foundGenre = null;
            foreach (var gen in genres)
                if (Comparator.IgnoreCaseCompare(gen.ToString(), genre))
                    foundGenre = gen;

            if (foundGenre == null) throw new KeyNotFoundException();

            foreach (var artist in artists)
            {
                foreach (var album in artist.AlbumList)
                    if (album.Genre.IsSubgenreOf(foundGenre))
                    {
                        result.Add(artist);
                        break;
                    }
            }

            return result;
        }

        protected static List<Album> SearchAlbumsByGenre(string genre, List<Genre> genres, List<Artist> artists)
        {
            List<Album> result = new List<Album>();

            Genre foundGenre = null;
            foreach (var gen in genres)
                if (gen.ToString().ToLower().Equals(genre.ToString().ToLower()))
                    foundGenre = gen;

            if (foundGenre == null) throw new KeyNotFoundException();

            foreach (var artist in artists)
            {
                foreach (var album in artist.AlbumList)
                    if (album.Genre.IsSubgenreOf(foundGenre))
                        result.Add(album);
            }

            return result;
        }

        protected static List<Album> SearchAlbumsByGenre(string genre, List<Genre> genres, List<Album> albums)
        {
            List<Album> result = new List<Album>();

            Genre foundGenre = null;
            foreach (var gen in genres)
                if (gen.ToString().ToLower().Equals(genre.ToString().ToLower()))
                    foundGenre = gen;

            if (foundGenre == null) throw new KeyNotFoundException();

            foreach (var album in albums)
                if (album.Genre.IsSubgenreOf(foundGenre))
                    result.Add(album);

            return result;
        }

        protected static List<Album> SearchAlbumsByYear(int year, List<Artist> artists)
        {
            List<Album> result = new List<Album>();

            foreach (var artist in artists)
            {
                foreach (var album in artist.AlbumList)
                    if (album.Year == year)
                        result.Add(album);
            }
            
            return result;
        }

        protected static List<Album> SearchAlbumsByYear(int year, List<Album> albums)
        {
            List<Album> result = new List<Album>();

            foreach (var album in albums)
                if (album.Year == year)
                    result.Add(album);
            
            return result;
        }

        protected static List<Album> SearchAlbumsByName(string name, List<Artist> artists)
        {
            List<Album> result = new List<Album>();
            
            foreach (var artist in artists)
            {
                foreach (var album in artist.AlbumList)
                    if (Comparator.IgnoreCaseCompare(album.Name, name))
                        result.Add(album);
            }

            return result;
        }

        protected static List<Album> SearchAlbumsByName(string name, List<Album> albums)
        {
            List<Album> result = new List<Album>();
            
            foreach (var album in albums)
                if (Comparator.IgnoreCaseCompare(album.Name, name))
                    result.Add(album);

            return result;
        }

        protected static List<Track> SearchTracksByName(string name, List<Artist> artists)
        {
            List<Track> result = new List<Track>();
            
            foreach (var artist in artists)
            {
                foreach (var album in artist.AlbumList)
                {
                    foreach (var track in album.TrackList)
                        if (Comparator.IgnoreCaseCompare(track.name, name))
                            result.Add(track);
                }
            }

            return result;
        }

        protected static List<Track> SearchTracksByName(string name, List<Track> tracks)
        {
            List<Track> result = new List<Track>();
            
            foreach (var track in tracks)
                if (Comparator.IgnoreCaseCompare(track.name, name))
                    result.Add(track);

            return result;
        }

        protected static List<Track> SearchTracksByYear(int year, List<Artist> artists)
        {
            List<Track> result = new List<Track>();

            List<Album> albums = SearchEngine.SearchAlbumsByYear(year, artists);
            foreach (var album in albums)
            {
                foreach (var track in album.TrackList)
                    result.Add(track);
            }

            return result;
        }

        protected static List<Track> SearchTracksByYear(int year, List<Track> tracks)
        {
            List<Track> result = new List<Track>();

            foreach (var track in tracks)
                if (track.Album.Year == year)
                    result.Add(track);

            return result;
        }

        protected static List<Track> SearchTracksByGenre(string genre, List<Genre> genres, List<Artist> artists)
        {
            List<Track> result = new List<Track>();
            bool cathedEx = false;

            try
            {
                List<Album> albums = SearchAlbumsByGenre(genre, genres, artists);
                foreach (var album in albums)
                {
                    foreach (var track in album.TrackList)
                        result.Add(track);
                }
            }
            catch (Exception)
            {
                cathedEx = true;
            }

            if (cathedEx)
                throw new KeyNotFoundException();
            else 
                return result;
        }

        protected static List<Track> SearchTracksByGenre(string genre, List<Genre> genres, List<Track> tracks)
        {
            List<Track> result = new List<Track>();
            bool cathedEx = false;

            try
            {
                Genre foundGenre = null;
                foreach (var gen in genres)
                    if (Comparator.IgnoreCaseCompare(gen.ToString(), genre))
                        foundGenre = gen;

                if (foundGenre == null) throw new KeyNotFoundException();
                    
                foreach (var track in tracks)
                    if (track.Album.Genre.IsSubgenreOf(foundGenre))
                        result.Add(track);
            }
            catch (Exception)
            {
                cathedEx = true;
            }

            if (cathedEx)
                throw new KeyNotFoundException();
            else 
                return result;
        }

        protected static List<TrackCompilation> SearchTrackCompilationsByGenre(string genre, List<TrackCompilation> trackCompilations)
        {
            List<TrackCompilation> tc = new List<TrackCompilation>();

            foreach (var compil in trackCompilations)
            {
                foreach (var gen in compil.Genres)
                    if (Comparator.IgnoreCaseCompare(gen.ToString(), genre))
                        tc.Add(compil);
            }

            return tc;
        }

        protected static List<TrackCompilation> SearchTrackCompilationsByArtist(string artist, List<TrackCompilation> trackCompilations)
        {
            List<TrackCompilation> tc = new List<TrackCompilation>();
            
            foreach (var compil in trackCompilations)
            {
                foreach (var art in compil.Artists)
                    if (Comparator.IgnoreCaseCompare(art.ToString(), artist))
                        tc.Add(compil);
            }

            return tc;
        }
    }
}