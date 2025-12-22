using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeGraphQL2.DataAccess.Entity
{
    public class Train
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainId { get; set; }
        [Required]
        public string? TrainName { get; set; }
        [Required]
        public int TrainNumber { get; set; }
        [Required]
        public string? TrainRoute { get; set; }
        [Required]
        public string? TrainStanciyaOtpravlenya { get; set; }
        [Required]
        public string? TrainStanciyaPribitiya { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<Carriage> Carriages { get; set; }
    }
}
