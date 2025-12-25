using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PracticeGraphQLClient2.DataAccess.Model
{
    public class Passenger
    {
        public int PassengerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public override string ToString()
        {
            return $"PassengerId:{PassengerId},\n" +
                $"FirstName:{FirstName},\n" +
                $"LastName:{LastName},\n" +
                $"SurName:{SurName},\n" +
                $"Email:{Email},\n" +
                $"Phone:{Phone},\n" +
                $"Age:{Age},\n" +
                $"Address:{Address},\n";
        }
    }
}
