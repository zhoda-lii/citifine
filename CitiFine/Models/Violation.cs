using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CitiFine.Areas.Identity.Data;

namespace CitiFine.Models
{
    public class Violation
    {
        public int ViolationId { get; set; }

        [Required(ErrorMessage = "Please enter a violation type.")]
        public string ViolationType { get; set; } // Red Light, Speeding, Parking

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FineAmount { get; set; }

        public DateTime DateIssued { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public CitiFineUser? User { get; set; }

        public bool IsPaid { get; set; } = false;
    }
}
