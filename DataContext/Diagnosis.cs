using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Diagnosis
    {
        public int DiagnosisId { get; set; }

        public string Beskrivning { get; set; }

        public virtual List<VetVisit> VetVisits { get; set; }
    }
}