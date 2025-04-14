using BRCSystem.ClassLibrary.Authentication.Entities;
using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class LotStatusHistory
    {
        public int? Id { get; set; }
        public LotStatus LotStatus { get; set; }
        public string? Motive { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime HistoryDate { get; set; }
        public int ProductLotId { get; set; }
        public ProductLot? ProductLot { get; set; }

        [MaxLength(500, ErrorMessage = "{0} can have a max of {1} characters")]
        public string? Problem { get; set; }

        [MaxLength(500, ErrorMessage = "{0} can have a max of {1} characters")]
        public string? RootCause { get; set; }

        [MaxLength(500, ErrorMessage = "{0} can have a max of {1} characters")]
        public string? CorrectiveAction { get; set; }

        [MaxLength(500, ErrorMessage = "{0} can have a max of {1} characters")]
        public string? Who { get; set; }

        public DateTime? When { get; set; }
    }
}