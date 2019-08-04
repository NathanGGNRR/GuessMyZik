using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GuessMyZik.Classes.TrackClasses;

namespace GuessMyZik.Classes.FrameParameters
{
    class GameFrameSoloParameters
    {
        public Frame rootFrame { get; set; }
        public Frame secondFrame { get; set; }
        public Users connectedUser { get; set; }
        public int? classTypeSelected { get; set; }
        public Dictionary<int, object> listSelected { get; set; }
        public List<Track> listTrack { get; set; }
        public int? game_duel { get; set; }
        public int? number_tracks { get; set; }
        public int? party_id { get; set; }

        public GameFrameSoloParameters(Frame frame, Frame secondFrame, Users user, int? classType, Dictionary<int, object> list , int? gameDuel, int? numberTracks, int? party_id)
        {
            this.rootFrame = frame;
            this.secondFrame = secondFrame;
            this.connectedUser = user;
            this.classTypeSelected = classType;
            this.listSelected = list;
            this.game_duel = gameDuel;
            this.number_tracks = numberTracks;
            this.party_id = party_id;
        }
    }
}
