using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GuessMyZik.Classes.FrameParameters
{
    class FrameParameters
    {
        public Frame rootFrame { get; set; }
        public Frame secondFrame { get; set; }
        public Users connectedUser { get; set; }
        public int? nbVisiteur { get; set; }

        public FrameParameters(Frame frame, Frame secondFrame, Users user)
        {
            this.rootFrame = frame;
            this.secondFrame = secondFrame;
            this.connectedUser = user;
        }

        public FrameParameters(Frame frame, Frame secondFrame, int? nbVisit)
        {
            this.rootFrame = frame;
            this.secondFrame = secondFrame;
            this.nbVisiteur = nbVisit;
        }
    }
}
