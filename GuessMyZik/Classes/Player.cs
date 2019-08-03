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
        public KeyRoutedEventArgs key { get; set; }

        public Player(string playerName, KeyRoutedEventArgs keyAffected)
        {
            this.name = playerName;
            this.key = keyAffected;
        }
    }
}
