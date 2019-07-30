using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessMyZik.Classes.ArtistClasses;


namespace GuessMyZik.Classes.AlbumClasses
{
    public class Albums
    {
        public List<Album> data { get; set; }
        public int total { get; set; }
        public string next { get; set; }

        public Albums()
        {
            data = new List<Album>();
        }
    }
}
