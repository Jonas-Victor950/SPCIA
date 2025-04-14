using BRCSystem.ClassLibrary.Authentication.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class ExposureTimeConfiguration
    {
        public int? Id { get; set; }

        //Entrada em minutos
        [Required(ErrorMessage = "AverageOfPackingTime cannot be null or negative"), Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int AverageOfPackingTime { get; set; }

        public string MaterialExposureControl { get; set; }
        public int AverageOfTapeAndReelTime { get; set; }
        public int AverageOfFinalQualityAssurance { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        [NotMapped]
        public List<int> MaterialExposureControls
        {
            get => MaterialExposureControl.Split(",").Select(x => int.Parse(x)).ToList();
        }
    }
}