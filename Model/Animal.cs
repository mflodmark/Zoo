using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Zoo.Model
{
    public class Animal
    {
        public int AnimalId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
        
        public string Enviroment { get; set; }

        public string Species { get; set; }

        public string CountryOfOrigin { get; set; }

        public string Gender { get; set; }

        public double Weight { get; set; }

    }


}
