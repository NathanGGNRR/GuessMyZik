using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GuessMyZik.Classes.TrackClasses;


namespace GuessMyZik.Classes
{
    class Party
    {
        public int? party_id { get; set; }
        public string username { get; set; }
        public string initials { get; set; }
        public string date { get; set; }
        public int? classType { get; set; }
        public int? nb_tracks { get; set; }
        public int? game_duel { get; set; }
        public int? points { get; set; }
        public List<Track> listTrack { get; set; }

        public Party(string username, string date, int? classType, int? nb_tracks, int? game_duel, List<Track> listTrack)
        {
            this.party_id = 0;
            this.username = username;
            this.initials = username[0].ToString().ToUpper() + username[1].ToString().ToUpper();
            this.date = date;
            this.classType = classType;
            this.nb_tracks = nb_tracks;
            this.game_duel = game_duel;
            this.points = 0;
            this.listTrack = listTrack;

        }
    }
}
