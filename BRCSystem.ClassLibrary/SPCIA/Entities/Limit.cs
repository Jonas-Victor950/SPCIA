using BRCSystem.ClassLibrary.Authentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class Limit
    {
        public int? Id { get; set; }

        public int CharacteristicId { get; set; }
        public virtual Characteristic? Characteristic { get; set; }

        public double UCLx { get; set; }
        public double CLx { get; set; }
        public double LCLx { get; set; }
        public double UCLr { get; set; }
        public double CLr { get; set; } 
        public double LCLr {get; set; } = 0;

        public DateTime? SetDate { get; set; }

        public int? SetById { get; set; }
        public User? SetBy {  get; set; }
    }
}
