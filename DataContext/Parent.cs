using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Parent
    {
        public int ParentId { get; set; }

        //public string Name { get; set; }

        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        public virtual List<Animal> Children { get; set; }
    }
}