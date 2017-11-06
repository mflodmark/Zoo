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

        public CountryOfOrigin CountryOfOrigin { get; set; }
    }
}