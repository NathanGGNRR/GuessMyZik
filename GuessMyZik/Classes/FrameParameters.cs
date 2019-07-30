using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GuessMyZik.Classes
{
    class FrameParameters
    {
        public Frame rootFrame { get; set; }
        public Frame secondFrame { get; set; }
        public Users connectedUser { get; set; }

        public FrameParameters(Frame frame, Frame secondFrame, Users user)
        {
            this.rootFrame = frame;
            this.secondFrame = secondFrame;
            this.connectedUser = user;
        }
    }
}
