using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMyZik.Classes
{
    class Users
    {
        public string username { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string level_id { get; set; }
        public string xp { get; set; }

        public Users(string username, string password, string mail, string level_id, string xp)
        {
            this.username = username;
            this.password = password;
            this.mail = mail;
            this.level_id = level_id;
            this.xp = xp;
        }

    }
}
