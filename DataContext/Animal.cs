namespace Zoo.DataContext
{
    public class Animal
    {
        public int AnimalId { get; set; }

        public int EnviromentId { get; set; }

        public virtual Enviroment Enviroment { get; set; }

        public int SpeciesId { get; set; }

        public virtual Species Species { get; set; }

        public int Quantity { get; set; }
    }
}