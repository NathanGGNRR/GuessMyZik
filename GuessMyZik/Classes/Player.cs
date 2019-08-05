using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;

namespace GuessMyZik.Classes
{
    class Player
    {
        public string name { get; set; }
        public string keyName { get; set; }
        public KeyRoutedEventArgs key { get; set; }
        public int points {get; set; }

        public Player(string playerName, string keyName, KeyRoutedEventArgs keyAffected)
        {
            this.name = playerName;
            this.key = keyAffected;
            this.keyName = keyName;
            this.points = 0;
        }
    }
}
