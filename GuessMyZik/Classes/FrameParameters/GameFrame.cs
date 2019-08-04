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
        public Track trackGuessed { get; set; }
        public List<Track> listTrack { get; set; }
        public List<Track> beforeTrack { get; set; }
        public ListView listBeforeTrack { get; set; }
        public List<TextBlock> textGuess { get; set; }
        public int? classType { get; set; }
        public int points { get; set; }

        public GameFrame(Frame frame, Track trackGuessed, List<Track> listTrack, List<Track> beforeTrack,ListView listView ,List<TextBlock> textGuess, int? classType, int points)
        {
            this.rootFrame = frame;
            this.trackGuessed = trackGuessed;
            this.listTrack = listTrack;
            this.beforeTrack = beforeTrack;
            this.listBeforeTrack = listView;
            this.textGuess = textGuess;
            this.classType = classType;
            this.points = points;
        }
    }
}
