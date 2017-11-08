using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Description
    {
        public int DescriptionId { get; set; }

        public string Name { get; set; }

        public int DiagnosisId { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }

        //public virtual List<VetVisit> VetVisits { get; set; }

        public int VetVisitId { get; set; }

        public VetVisit VetVisit { get; set; }


    }
}