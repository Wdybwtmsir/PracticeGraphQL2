using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeGraphQLClient2.DataAccess.Model
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int Number { get; set; }
        public bool IsBooked { get; set; }
        public int CarriageId { get; set; }
        public Carriage? Carriage { get; set; }
        public override string ToString()
        {
            return $"SeatId:{SeatId},\n" +
                $"Number:{Number},\n" +
                $"IsBooked:{IsBooked},\n" +
                $"CarriageId:{CarriageId},\n" +
                $"Carriage:[{Carriage!.ToString()}]\n";
        }
    }
}
