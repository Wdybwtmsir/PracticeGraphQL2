using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeGraphQL2.DataAccess.Entity
{
    public class Seat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsBooked { get; set; }
        public int CarriageId { get; set; }
        public Carriage? Carriage { get; set; }
    }
}
