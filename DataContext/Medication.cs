using System.Collections.Generic;

namespace Zoo.DataContext
{
    public class Medication
    {
        public int MedicationId { get; set; }

        public string Name { get; set; }

        public virtual List<Diagnosis> Diagnosis { get; set; }

    }
}