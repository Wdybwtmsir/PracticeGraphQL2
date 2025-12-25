using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PracticeGraphQLClient2.DataAccess.Model
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public DateTime DataProdaji { get; set; }
        public string SellerName { get; set; }
        public int TrainId { get; set; }
        public Train? Train { get; set; }
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
        public override string ToString()
        {
            return $"TicketId:{TicketId},\n" +
                $"Price:{Price},\n" +
                $"IsSold:{IsSold},\n" +
                $"DataProdaji:{DataProdaji},\n" +
                $"SellerName:{SellerName}\n"+
                $"TrainId:{TrainId},\n" +
                $"Train:[{Train!.ToString()}],\n" +
                $"PassengerId:{PassengerId},\n" +
                $"Passenger:[{Passenger!.ToString()}]\n";
        }
    }
}
