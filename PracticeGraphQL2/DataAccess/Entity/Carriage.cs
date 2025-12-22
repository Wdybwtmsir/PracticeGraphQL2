using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeGraphQL2.DataAccess.Entity
{
    public class Carriage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarriageId { get; set; }
        [Required]
        public int Number { get; set; }
        public int TrainId { get; set; }
        public Train? Train { get; set; }
        public ICollection<Seat>? Seats { get; set; }
    }
}
