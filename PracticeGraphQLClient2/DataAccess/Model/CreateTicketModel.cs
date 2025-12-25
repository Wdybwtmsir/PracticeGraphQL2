using System.ComponentModel.DataAnnotations;

namespace PracticeGraphQLClient2.DataAccess.Model
{
    public class CreateTicketModel
    {
            [Required(ErrorMessage ="Price is required")]
            public decimal Price { get; set; }
            [Required(ErrorMessage ="Sold State is required")]
            public bool IsSold { get; set; }
            [Required(ErrorMessage ="DataProdaji is required")]
            public DateTime DataProdaji { get; set; }
            [Required(ErrorMessage ="SellerName is required")]
            public string SellerName { get; set; }
            [Required(ErrorMessage ="TrainId is required")]
            public int TrainId { get; set; }
            [Required(ErrorMessage ="PassengerId is required")]
            public int? PassengerId { get; set; }
        }
}
