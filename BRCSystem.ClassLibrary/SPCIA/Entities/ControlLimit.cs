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
    public class ControlLimit
    {
        public int? Id { get; set; }

        public int ProcessId { get; set; }
        public virtual Process? Process { get; set; }

        public int MachineId { get; set; }
        public virtual Machine? Machine { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public DateTime? CreationDate { get; set; }

        public bool Active { get; set; } = true;

        public bool ApplyForSetup { get; set; } = false;
        public bool ApplyForMonitoring { get; set; } = false;
        public bool ApplyForRequal { get; set; } = false;
        public bool ApplyForDataGathering { get; set; } = false;

        public List<Limit>? Limits { get; set; }
    }
}
