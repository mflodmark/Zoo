using System;
using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class VetVisit
    {
        public int VetVisitId { get; set; }

        public DateTime DateAndTime { get; set; }

        public int VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        public int DiagnosisId { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }

        public virtual List<Medication> Medications { get; set; }
        
    }
}