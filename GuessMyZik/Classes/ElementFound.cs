using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMyZik.Classes
{
    class ElementFound
    {
        public string username { get; set; }
        public string track_id { get; set; }
        public string element { get; set; }
        public string date { get; set; }

        public ElementFound(string username, string track_id, string element, string date)
        {
            this.username = username;
            this.track_id = track_id;
            this.element = element;
            this.date = date;
        }

    }
}
