namespace Lab2
{
    public class Track
    {
        public readonly string name;
        private int durationMin;
        private int durationSec;
        public readonly Album Album;

        public Track(string name, int min, int sec, Album album)
        {
            this.name = name;
            this.durationMin = min;
            this.durationSec = sec;
            this.Album = album;
        }

        public override string ToString()
        {
            return $"{name} - {Album.Artist.ToString()} {durationMin}:{durationSec:00}";
        }
    }
}