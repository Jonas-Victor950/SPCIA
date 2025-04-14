using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class Samples
    {
        public int? Id { get; set; }

        public int CharacteristicId { get; set; }
        public virtual Characteristic? Characteristic { get; set; }

        public List<Input> Inputs { get; set; }

        [NotMapped]
        public double Xbar { get {
                double sum = 0;
                foreach(var input in Inputs)
                {
                    sum = sum + ((double)input.Value);
                }
                return sum / Inputs.Count;
            } }

        [NotMapped]
        public double R { get
            {
                List<double> values = [];
                foreach (var input in Inputs)
                {
                    values.Add(input.Value);
                }
                double r = values.Max() - values.Min();
                return Math.Round(r, 4);         
            }
        }

        [NotMapped]
        public Boolean XbarAlarm { get; set; } = false;
        [NotMapped]
        public Boolean RAlarm { get; set; } = false;
    }
}
