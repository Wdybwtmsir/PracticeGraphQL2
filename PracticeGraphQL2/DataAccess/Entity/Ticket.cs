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
        public bool IsSold { get; set; }
        [Required]
        public DateTime DataProdaji { get; set; }
        
        public string SellerName { get; set; }
        public int TrainId { get; set; }
        public Train? Train { get; set; }
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
    }
}
