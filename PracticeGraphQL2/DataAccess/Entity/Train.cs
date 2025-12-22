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
        public string TrainName { get; set; }
        [Required]
        public string TrainRoute { get; set; }
        [Required]
        public string TrainStanciyaOtpravlenya { get; set; }
        [Required]
        public string TrainStanciyaPribitiya { get; set; }
        public ICollection<Passenger> Passengers { get; set; }
    }
}
