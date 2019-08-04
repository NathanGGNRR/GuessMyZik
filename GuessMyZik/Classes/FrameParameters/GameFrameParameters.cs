using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMyZik.Classes.FrameParameters
{
    class GameFrameParameters
    {
        public GameFrameSoloParameters solo { get; set; }
        public GameFrameMultiParameters multi { get; set; }

        public GameFrameParameters(GameFrameSoloParameters solo, GameFrameMultiParameters multi) {
            this.solo = solo;
            this.multi = multi;
        }
    }
}
