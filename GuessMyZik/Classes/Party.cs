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
        public string username { get; set; }
        public string date { get; set; }
        public int? nb_tracks { get; set; }
        public int? game_duel { get; set; }
        public List<Track> listTrack { get; set; }

        public Party(string username, string date, int? nb_tracks, int? game_duel, List<Track> listTrack)
        {
            this.username = username;
            this.date = date;
            this.nb_tracks = nb_tracks;
            this.game_duel = game_duel;
            this.listTrack = listTrack;

        }
    }
}
