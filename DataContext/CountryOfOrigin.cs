using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class CountryOfOrigin
    {
        public int CountryOfOriginId { get; set; }

        public string Name { get; set; }

        public virtual List<Animal> Animals { get; set; }
    }
}