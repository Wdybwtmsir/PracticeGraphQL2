namespace PracticeGraphQLClient2.DataAccess.Model
{
        public class PassengerMovement
        {
            public int TicketId { get; set; }
            public string TrainName { get; set; } = string.Empty;
            public int TrainNumber { get; set; }
            public string Route { get; set; } = string.Empty;
            public string DepartureStation { get; set; } = string.Empty;
            public string ArrivalStation { get; set; } = string.Empty;
            public DateTime PurchaseDate { get; set; }
            public decimal TicketPrice { get; set; }
            public string SellerName { get; set; } = string.Empty;
        }
    }


