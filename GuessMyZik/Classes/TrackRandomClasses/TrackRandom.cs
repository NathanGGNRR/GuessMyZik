using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessMyZik.Classes.ArtistClasses;
using GuessMyZik.Classes.AlbumClasses;
using GuessMyZik.Classes.TrackClasses;

namespace GuessMyZik.Classes.TrackRandomClasses
{
    class TrackRandom
    {
        public string id { get; set; }
        public bool readable { get; set; }
        public string title { get; set; }
        public string title_short { get; set; }
        public string title_version { get; set; }
        public string isrc { get; set; }
        public string link { get; set; }
        public string share { get; set; }
        public string duration { get; set; }
        public int track_position { get; set; }
        public int disk_number { get; set; }
        public int rank { get; set; }
        public string release_date { get; set; }
        public bool explicit_lyrics { get; set; }
        public int explicit_content_lyrics { get; set; }
        public int explicit_content_cover { get; set; }
        public string preview { get; set; }
        public int bpm { get; set; }
        public double gain { get; set; }
        public List<string> available_countries { get; set; }
        public List<Contributor> contributors { get; set; }
        public Artist artist { get; set; }
        public Album album { get; set; }
        public string type { get; set; }
    }
}
