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

        public virtual List<Animal> FamilyMembersLink { get; set; }

        //public virtual List<Animal> Parents { get; set; }

        //public virtual List<Animal> Children { get; set; }

        public virtual List<VetVisit> VetVisits { get; set; }
    }

    public class FamilyMembersLink
    {
        public int Id { get; set; }
        //public int ParentId { get; set; }
        //public int ChildId { get; set; }

        public Animal Parent { get; set; }
        public Animal Child { get; set; }
    }
}