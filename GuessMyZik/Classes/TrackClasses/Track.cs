using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessMyZik.Classes.ArtistClasses;
using GuessMyZik.Classes.AlbumClasses;

namespace GuessMyZik.Classes.TrackClasses
{
    public class Track
    {
        public string id { get; set; }
        public bool readable { get; set; }
        public string title { get; set; }
        public string title_short { get; set; }
        public string title_version { get; set; }
        public string link { get; set; }
        public string duration { get; set; }
        public string rank { get; set; }
        public bool explicit_lyrics { get; set; }
        public int explicit_content_lyrics { get; set; }
        public int explicit_content_cover { get; set; }
        public string preview { get; set; }
        public Artist artist { get; set; }
        public Album album { get; set; }
        public string type { get; set; }

        public Track(string id, bool readable, string title, string title_short, string title_version, string link, string duration, string rank, bool explicit_lyrics, int explicit_content_lyrics, int explicit_content_cover, string preview, Artist artist, Album album, string type)
        {
            this.id = id;
            this.readable = readable;
            this.title = title;
            this.title_short = title_short;
            this.title_version = title_version;
            this.link = link;
            this.duration = duration;
            this.rank = rank;
            this.explicit_lyrics = explicit_lyrics;
            this.explicit_content_lyrics = explicit_content_lyrics;
            this.explicit_content_cover = explicit_content_cover;
            this.preview = preview;
            this.artist = artist;
            this.album = album;
            this.type = type;
        }
    }
}
