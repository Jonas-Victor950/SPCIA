using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using BRCSystem.ClassLibrary.POSStation.Entities;

namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class ProcessSampling
    {
        public int? Id { get; set; }

        public int ProcessId { get; set; }
        public virtual Process? Process { get; set; }

        public int MachineId { get; set; }
        public virtual Machine? Machine { get; set;}

        public int OperatorId { get; set; }
        public virtual User? Operator { get; set; }

        public int? ProductLotId { get; set; }
        public virtual ProductLot? ProductLot { get; set; }

        public Classifier Classifier { get; set; }

        public DateTime? SampleDate { get; set; }

        public List<Samples>? Samples { get; set; }

        public String? RootCause { get; set; }

        public String? Who {  get; set; }

        public String? CorrectiveAction { get; set; }

        public bool Active { get; set; } = true;
    }
}
