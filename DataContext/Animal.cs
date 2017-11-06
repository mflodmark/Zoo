using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Animal
    {
        public int AnimalId { get; set; }

        public int SpeciesId { get; set; }

        public virtual Species Species { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public int CountryOfOriginId { get; set; }

        public virtual CountryOfOrigin CountryOfOrigin { get; set; }

        public int GenderId { get; set; }

        public virtual Gender Gender { get; set; }

        public List<Animal> Parents { get; set; }

        public List<Animal> Children { get; set; }
    }
}