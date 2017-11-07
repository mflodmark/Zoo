using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Enviroment
    {
        public int EnviromentId { get; set; }

        public string Name { get; set; }

        public virtual List<Species> Species { get; set; }

    }

}