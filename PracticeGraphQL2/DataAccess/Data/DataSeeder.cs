using Faker;
using PracticeGraphQL2.DataAccess.Entity;
using System;
using System.Linq;

namespace PracticeGraphQL2.DataAccess.Data
{
    public static class DataSeeder
    {
        public static void SeedData(SampleAppDbContext db)
        {
            var rand = new Random();

            // Сидим поезда и вагоны (Carriage)
            if (!db.Train.Any())
            {
                for (int i = 1; i <= 3; i++)
                {
                    var train = new Train
                    {
                        TrainName = $"Train {i}",
                        TrainNumber = rand.Next(1000, 9999).ToString()
                    };
                    db.Train.Add(train);
                    db.SaveChanges();

                    // Создаем 2 вагона на каждый поезд
                    for (int c = 1; c <= 2; c++)
                    {
                        var carriage = new Carriage
                        {
                            TrainId = train.Id,
                            Number = c,
                            Type = c % 2 == 0 ? "Coupe" : "Platzkart"
                        };
                        db.Carriage.Add(carriage);
                        db.SaveChanges();

                        // Создаем места для каждого вагона
                        for (int s = 1; s <= 5; s++)
                        {
                            var seat = new Seat
                            {
                                CarriageId = carriage.Id,
                                Number = s,
                                SeatType = "Standard"
                            };
                            db.Seat.Add(seat);
                        }
                        db.SaveChanges();
                    }
                }
            }

            // Сидим пассажиров
            if (!db.Passenger.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var passenger = new Passenger
                    {
                        FirstName = Name.First(),
                        LastName = Name.Last(),
                        SurName = Name.Suffix(),
                        Email = Internet.Email(),
                        Phone = Phone.Number(),
                        Age = rand.Next(15, 70),
                        Address = Address.StreetAddress()
                    };
                    db.Passenger.Add(passenger);
                }
                db.SaveChanges();
            }

            // Билеты с местами и пассажирами
            if (!db.Ticket.Any())
            {
                var passengers = db.Passenger.Take(5).ToList();
                var seats = db.Seat.Take(5).ToList();

                for (int i = 0; i < Math.Min(passengers.Count, seats.Count); i++)
                {
                    var ticket = new Ticket
                    {
                        PassengerId = passengers[i].Id,
                        SeatId = seats[i].Id,
                        Price = rand.Next(500, 2000),
                        PurchaseDate = DateTime.Now.AddDays(-rand.Next(1, 30))
                    };
                    db.Ticket.Add(ticket);
                }
                db.SaveChanges();
            }
        }
    }
}