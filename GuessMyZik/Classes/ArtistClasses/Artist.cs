﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMyZik.Classes.ArtistClasses
{
    public class Artist 
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string picture { get; set; }
        public string picture_small { get; set; }
        public string picture_medium { get; set; }
        public string picture_big { get; set; }
        public string picture_xl { get; set; }
        public int nb_album { get; set; }
        public int nb_fan { get; set; }
        public bool radio { get; set; }
        public string tracklist { get; set; }
        public string type { get; set; }
        public int position { get; set; }
    }
}
