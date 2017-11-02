using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Enviroment
    {
        public int EnviromentId { get; set; }

        public string Name { get; set; }

        public List<Animal> Animals { get; set; }

    }

    public enum EnumEnviroment
    {
        Mark, Träd, Vatten
    }
}