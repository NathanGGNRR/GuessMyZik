using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GuessMyZik.Classes.TrackClasses;

namespace GuessMyZik.Classes.FrameParameters
{
    class GameFrame
    {
        public Frame rootFrame { get; set; }
        public Frame gameFrame { get; set; }
        public Users connectedUser { get; set; }
        public Track trackGuessed { get; set; }
        public List<Track> listTrack { get; set; }
        public List<Track> beforeTrack { get; set; }
        public ListView listBeforeTrack { get; set; }
        public List<TextBlock> textGuess { get; set; }
        public int? classType { get; set; }
        public int points { get; set; }
        public List<Player> players { get; set; }
        public Party party { get; set; }

        public GameFrame(Frame rootFrame, Frame gameFrame, Users connectedUser, Track trackGuessed, List<Track> listTrack, List<Track> beforeTrack,ListView listView ,List<TextBlock> textGuess, int? classType, int points, List<Player> players, Party party)
        {
            this.rootFrame = rootFrame;
            this.gameFrame = gameFrame;
            this.connectedUser = connectedUser;
            this.trackGuessed = trackGuessed;
            this.listTrack = listTrack;
            this.beforeTrack = beforeTrack;
            this.listBeforeTrack = listView;
            this.textGuess = textGuess;
            this.classType = classType;
            this.points = points;
            this.players = players;
            this.party = party;
        }
    }
}
