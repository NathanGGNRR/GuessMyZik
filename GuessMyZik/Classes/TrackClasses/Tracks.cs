using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessMyZik.Classes.ArtistClasses;
using GuessMyZik.Classes.AlbumClasses;


namespace GuessMyZik.Classes.TrackClasses{
    public class Tracks
    {
        public List<Track> data { get; set; }
        public int total { get; set; }
        public string next { get; set; }

        public Tracks()
        {
            data = new List<Track>();
        }
    }
}
