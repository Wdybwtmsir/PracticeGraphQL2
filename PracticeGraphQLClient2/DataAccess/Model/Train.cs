using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeGraphQLClient2.DataAccess.Model
{
    public class Train
    {
        public int TrainId { get; set; }
        public string? TrainName { get; set; }
        public int TrainNumber { get; set; }
        public string? TrainRoute { get; set; }
        public string? TrainStanciyaOtpravlenya { get; set; }
        public string? TrainStanciyaPribitiya { get; set; }
        public override string ToString()
        {
            return $"TrainId:{TrainId},\n" +
                $"TrainName:{TrainName},\n" +
                $"TrainNumber:{TrainNumber},\n" +
                $"TrainRoute:{TrainRoute},\n" +
                $"TrainStanciyaOtpravleniya:{TrainStanciyaOtpravlenya}\n" +
                $"TrainStanciyaPribitiya:{TrainStanciyaPribitiya},\n";
        }
    }
}

