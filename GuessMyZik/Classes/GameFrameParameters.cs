using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GuessMyZik.Classes
{
    class GameFrameParameters
    {
        public Frame rootFrame { get; set; }
        public Frame secondFrame { get; set; }
        public Users connectedUser { get; set; }
        public int classTypeSelected { get; set; }

        public GameFrameParameters(Frame frame, Frame secondFrame, Users user, int classType)
        {
            this.rootFrame = frame;
            this.secondFrame = secondFrame;
            this.connectedUser = user;
            this.classTypeSelected = classType;
        }
    }
}
