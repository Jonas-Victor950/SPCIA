using BRCSystem.ClassLibrary.SPCIA.Entities;

namespace SPCIA.API.Models
{
    public class ChartControlResponse
    {
        public required double Clr { get; set; }
        public required double Clx { get; set; }
        public required double? Cpk { get; set; }
        public required double? Lclx { get; set; }
        public required double? Lclr { get; set; }
        public required double? Ppk { get; set; }
        public required double Uclr { get; set; }
        public required double Uclx { get; set; }
        public required List<DateTime> Timestamp { get; set; } = [];
        public required List<Classifier> Classifiers { get; set; } = [];
        public required List<string> Rcolors { get; set; } = [];
        public required List<double> R { get; set; } = [];
        public required List<double> Xbar { get; set; } = [];
        public required List<string> Xbarcolors { get; set; } = [];
        public bool HasLimit { get; set; } = true;
    }
}
