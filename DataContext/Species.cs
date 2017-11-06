using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Species
    {
        public int SpeciesId { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public virtual Type Type { get; set; }

        public List<Animal> Animals { get; set; }

        public int EnviromentId { get; set; }

        public virtual Enviroment Enviroment { get; set; }

    }
}