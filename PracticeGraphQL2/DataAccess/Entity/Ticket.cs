using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeGraphQL2.DataAccess.Entity
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int NomerVagona { get; set; }
        [Required]
        public int NomerMesta { get; set; }
        [Required]
        public DateTime DataProdaji { get; set; }
        [Required]
        public string SellerName { get; set; }
        public ICollection<Passenger> Passengers { get; set; }
    }
}
