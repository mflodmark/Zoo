using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Parent
    {
        public int ParentId { get; set; }

        //public string Name { get; set; }

        public int AnimalId { get; set; }

        public Animal Animal { get; set; }

        public List<Animal> Children { get; set; }
    }
}