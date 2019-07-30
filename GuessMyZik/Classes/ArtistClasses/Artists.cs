using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMyZik.Classes.ArtistClasses
{
    public class Artists
    {
        public List<Artist> data { get; set; }
        public int total { get; set; }
        public string next { get; set; }

        public Artists()
        {
            data = new List<Artist>();
        }
    }
}
