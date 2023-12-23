using System;
using System.Collections.Generic;
/*
 * сделать UI
 * Разбить по папочкам
*/
namespace Lab2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Catalogue catalogue = Catalogue.Load("Songs.txt", "Genres.txt");

            string gNotFound = "";

            try
            {
                gNotFound = "Indie";

                Console.WriteLine($" === {gNotFound} tracks === ");
                foreach (var track in catalogue.SearchTracksByGenre(gNotFound))
                    Console.WriteLine(track.ToString());

                gNotFound = "Rock";

                Console.WriteLine($" === {gNotFound} artists === ");
                foreach (var artist in catalogue.SearchArtistsByGenre(gNotFound))
                    Console.WriteLine(artist.ToString());

                int year = 2004;
                Console.WriteLine($" === {gNotFound} tracks of {year} === ");
                foreach (var track in catalogue.SearchTracks(year, gNotFound))
                    Console.WriteLine(track.ToString());
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"\nGenre {gNotFound} not found\n");
            }

            Console.WriteLine(" === Oasis === ");
            List<Artist> oasis = catalogue.SearchArtists("Oasis");
            foreach (var artist in oasis)
            {
                Console.WriteLine(artist.ToString());
            }

            Console.WriteLine(" === Track compilation contanied pop === ");
            foreach (var tc in catalogue.SearchTrackCompilationsByGenre("Pop"))
                Console.WriteLine(tc.ToString());


            while (true)
            {
                string name, genre;
                List<Artist> artists = new List<Artist>();
                List<Track> tracks = new List<Track>();
                List<Album> albums = new List<Album>();
                int year;
                Console.WriteLine("1 - Search artist by name\n" + 
                    "2 - Search track by name\n" +
                    "3 - Search album by name\n" +
                    "4 - Search track by year\n" +
                    "5 - Search album by genre\n"
                    );
                int input = int.Parse(Console.ReadLine());

                switch(input)
                {
                    case 0:
                        return;

                    case 1:
                        Console.WriteLine("Please input artist's name");
                        name = Console.ReadLine();
                        artists = catalogue.SearchArtists(name);
                        foreach (var artist in artists)
                        {
                            Console.WriteLine(artist.ToString());
                        }
                        break;

                    case 2:
                        Console.WriteLine("Please input song's name");
                        name = Console.ReadLine();
                        tracks = catalogue.SearchTracks(name);
                        foreach (var track in tracks)
                        {
                            Console.WriteLine(track.ToString());
                        }
                        break;

                    case 3:
                        Console.WriteLine("Please input albums's name");
                        name = Console.ReadLine();
                        albums = catalogue.SearchAlbums(name);
                        foreach (var album in albums)
                        {
                            Console.WriteLine(album.ToString());
                        }
                        break;

                    case 4:
                        Console.WriteLine("Please input song's year");
                        year = int.Parse(Console.ReadLine());
                        tracks = catalogue.SearchTracks(year);
                        foreach (var track in tracks)
                        {
                            Console.WriteLine(track.ToString());
                        }
                        break;

                    case 5:
                        Console.WriteLine("Please input song's genre");
                        genre = Console.ReadLine();
                        tracks = catalogue.SearchTracksByGenre(genre);
                        foreach (var track in tracks)
                        {
                            Console.WriteLine(track.ToString());
                        }
                        break;

                }

            }
        }
    }
}
