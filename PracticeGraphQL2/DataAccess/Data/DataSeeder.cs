using Faker;
using PracticeGraphQL2.DataAccess.Entity;
using System.ComponentModel.DataAnnotations;

namespace PracticeGraphQL2.DataAccess.Data
{
    public static class DataSeeder
        {
            public static void SeedData(SampleAppDbContext db)
            {
                if (!db.Passenger.Any())
                {
                    for (int = 0; int < 5; int++)
                    {
                        var pas = new Passenger
                        {
                            FirstName = Name.First(),
                            LastName = Name.Last(),
                            SurName = Name.Suffix(),
                            Email = Faker.Internet.Email(),
                            Phone = Faker.Phone.Number(),
                            Age = new Random().Next(15, 70),
                            Address = Faker.Address.StreetAddress(),
                            Ticket = tick
                        };
                        db.Passenger.Add(pas);
                        for (int j = 0; j < 5; j++)
                        {
                            var tick = new Ticket
                            {
                                Price = new Random().Next(200,500),
                                NomerVagona = new Random().Next(0,100),
                                NomerMesta = new Random().Next(0,100),
                                DataProdaji = Faker. 
                            };
                        db.Ticket.Add(tick);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}

