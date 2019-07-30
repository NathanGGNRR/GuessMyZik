using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GuessMyZik.Classes.CategoryClasses { 
    public class Categories
    {
        public List<Category> data { get; set; }

        public Categories()
        {
            data = new List<Category>();
        }
    }
}
