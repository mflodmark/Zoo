using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Diagnosis
    {
        public int DiagnosisId { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public virtual List<VetVisit> VetVisits { get; set; }

    }
}