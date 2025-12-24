using Faker;
using PracticeGraphQL2.DataAccess.Entity;
using System;
using System.Linq;

namespace PracticeGraphQL2.DataAccess.Data
{

    public static class DataSeeder
    {
        //public static void SeedData(SampleAppDbContext db)
        //{
        //    var rand = new Random();
        //    if (!db.Train.Any())
        //    {
        //        for (int i = 1; i <= 3; i++)
        //        {
        //            var train = new Train
        //            {
        //                TrainName = $"Train {i}",
        //                TrainNumber = rand.Next(1000, 9999),
        //                TrainRoute = $"Route {i}",
        //                TrainStanciyaOtpravlenya = $"Station {i}A",
        //                TrainStanciyaPribitiya = $"Station {i}B",
        //                Carriages = new List<Carriage>()
        //            };
        //            db.Train.Add(train);
        //        }
        //        db.SaveChanges();
        //    }
        //    if (!db.Carriage.Any())
        //    {
        //        var trains = db.Train.ToList();
        //        foreach (var train in trains)
        //        {
        //            for (int n = 1; n <= 5; n++)
        //            {
        //                var carriage = new Carriage
        //                {
        //                    Number = n,
        //                    TrainId = train.TrainId
        //                };
        //                db.Carriage.Add(carriage);
        //            }
        //        }
        //        db.SaveChanges();
        //    }
        //    if (!db.Seat.Any())
        //    {
        //        var carriages = db.Carriage.ToList();
        //        foreach (var carriage in carriages)
        //        {
        //            for (int s = 1; s <= 20; s++)
        //            {
        //                var seat = new Seat
        //                {
        //                    Number = s,
        //                    Price = 100 + 10 * carriage.Number,
        //                    IsBooked = false,
        //                    CarriageId = carriage.CarriageId
        //                };
        //                db.Seat.Add(seat);
        //            }
        //        }
        //        db.SaveChanges();
        //    }
        //    if (!db.Passenger.Any())
        //    {
        //        for (int p = 1; p <= 10; p++)
        //        {
        //            var passenger = new Passenger
        //            {
        //                FirstName = $"FirstName{p}",
        //                LastName = $"LastName{p}",
        //                SurName = $"SurName{p}",
        //                Email = $"passenger{p}@example.com",
        //                Phone = $"+1000000{p:D4}",
        //                Age = rand.Next(18, 80),
        //                Address = $"Address {p}"
        //            };
        //            db.Passenger.Add(passenger);
        //        }
        //        db.SaveChanges();
        //    }
        //    if (!db.Ticket.Any())
        //    {
        //        var passengers = db.Passenger.ToList();
        //        var trains = db.Train.ToList();
        //        var seats = db.Seat.ToList();

        //        for (int t = 0; t < 10; t++)
        //        {
        //            var passenger = passengers[rand.Next(passengers.Count)];
        //            var train = trains[rand.Next(trains.Count)];
        //            var seatsInTrain = seats.Where(s => s.Carriage != null && s.Carriage.TrainId == train.TrainId && s.IsBooked == false).ToList();
        //            if (seatsInTrain.Count == 0) break;

        //            var seat = seatsInTrain[rand.Next(seatsInTrain.Count)];
        //            seat.IsBooked = true;

        //            var ticket = new Ticket
        //            {
        //                Price = seat.Price,
        //                IsSold = true,
        //                DataProdaji = DateTime.Now,
        //                SellerName = $"Seller {rand.Next(1, 5)}",
        //                TrainId = train.TrainId,
        //                PassengerId = passenger.PassengerId,
        //                SeatId = seat.SeatId
        //            };

        //            db.Ticket.Add(ticket);
        //        }
        //        db.SaveChanges();
        //    }
        //}
    }
}