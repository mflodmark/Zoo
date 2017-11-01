using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Type
    {
        public int TypeId { get; set; }

        public string Name { get; set; }

        public List<Species> Species { get; set; }

    }
}