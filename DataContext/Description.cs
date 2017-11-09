using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.DataContext
{
    public class Description
    {
        public int DescriptionId { get; set; }

        public string Name { get; set; }

        public int DiagnosisId { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }

        //public virtual List<VetVisit> VetVisits { get; set; }

        //[ForeignKey("VetVisit")]
        //public int VetVisitId { get; set; }

        public virtual VetVisit VetVisit { get; set; }


    }
}