using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Gender
    {
        public int GenderId { get; set; }

        public string Name { get; set; }

        public List<Animal> Animals { get; set; }
    }
}