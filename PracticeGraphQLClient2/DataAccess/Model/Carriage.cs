using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeGraphQLClient2.DataAccess.Model
{

    public class Carriage
    {
        public int CarriageId { get; set; }
        public int Number { get; set; }
        public int TrainId { get; set; }
        public Train? Train { get; set; }
        public override string ToString()
        {
            return $"CarriageId:{CarriageId},\n" +
                $"Number:{Number},\n" +
                $"TrainId:{TrainId},\n" +
                $"Train:[{Train!.ToString()}]\n";
        }
    }
}