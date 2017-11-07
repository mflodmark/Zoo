using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Vet
    {
        public int VetId { get; set; }

        public string Name { get; set; }

        public virtual List<VetVisit> VetVisits { get; set; }

    }
}