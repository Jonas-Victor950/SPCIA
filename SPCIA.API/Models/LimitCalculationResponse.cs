using BRCSystem.ClassLibrary.GetEntities.SPCIA;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Cuemon;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPCIA.API.Models
{
    public class LimitCalculationResponse
    {
        public LimitCalculationResponse(List<Samples> lists, Characteristic? characteristic,
            double a2, double d2, double d4, Limit limit)
        {
            this.lists = lists;
            Characteristic = characteristic;
            this.a2 = a2;
            this.d2 = d2;
            this.d4 = d4;
            this.Limit = limit;
        }

        public List<Samples> lists { get; set; }
        public Characteristic? Characteristic { get; }
        public Limit Limit { get; set; }

        public double doubleXBar { get { return LimitCalculate.calculate2XBar(lists); } }
        public double rBar { get { return LimitCalculate.calculateRBar(lists); } }
        public double usl { get { return Characteristic.USL; } }
        public double lsl { get { return Characteristic.LSL; } }
        public double standardDeviation { get { return LimitCalculate.calculateStandardDeviation(lists); } }
        public double? cpk { get { return LimitCalculate.calculateCpk(usl, lsl, doubleXBar, rBar, d2); } }
        public double? ppk { get { return LimitCalculate.calculatePpk(usl, lsl, doubleXBar, standardDeviation); } }        
        public List<String> pointColorsXBar { get { return LimitCalculate.calculateXBarAlarms(Limit.CLx, rBar, lists, a2, Limit.LCLx, Limit.UCLx); } }
        public List<String> pointColorsRbar { get { return LimitCalculate.calculateRAlarms(Limit.CLr, lists, d4, Limit.UCLr, Limit.LCLr); } }

        public double a2 { get; set; }
        public double d2 { get; set; }
        public double d4 { get; set; }
    }
}
