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

            Console.WriteLine("Начало заполнения базы данных...");
            if (!db.Train.Any())
            {
                Console.WriteLine("Создание поездов...");
                for (int i = 1; i <= 3; i++)
                {
                    db.Train.Add(new Train
                    {
                        TrainName = $"Поезд {i}",
                        TrainNumber = 1000 + i,
                        TrainRoute = $"Маршрут {i}",
                        TrainStanciyaOtpravlenya = $"Станция А{i}",
                        TrainStanciyaPribitiya = $"Станция Б{i}"
                    });
                }
                db.SaveChanges();
            }
            if (!db.Carriage.Any())
            {
                Console.WriteLine("Создание вагонов...");
                var trains = db.Train.ToList();
                foreach (var train in trains)
                {
                    for (int i = 1; i <= 4; i++) 
                    {
                        db.Carriage.Add(new Carriage
                        {
                            Number = i,
                            TrainId = train.TrainId
                        });
                    }
                }
                db.SaveChanges();
            }
            if (!db.Seat.Any())
            {
                Console.WriteLine("Создание мест...");
                var carriages = db.Carriage.ToList();
                foreach (var carriage in carriages)
                {
                    for (int i = 1; i <= 20; i++) 
                    {
                        db.Seat.Add(new Seat
                        {
                            Number = i,
                            IsBooked = false,
                            CarriageId = carriage.CarriageId
                        });
                    }
                }
                db.SaveChanges();
            }
            if (!db.Passenger.Any())
            {
                Console.WriteLine("Создание пассажиров...");
                for (int i = 1; i <= 10; i++)
                {
                    db.Passenger.Add(new Passenger
                    {
                        FirstName = $"Имя{i}",
                        LastName = $"Фамилия{i}",
                        SurName = $"Отчество{i}",
                        Email = $"passenger{i}@test.com",
                        Phone = $"+7{rand.Next(900, 999)}{rand.Next(1000000, 9999999)}",
                        Age = rand.Next(18, 65),
                        Address = $"Адрес {i}"
                    });
                }
                db.SaveChanges();
            }
            if (!db.Ticket.Any())
            {
                Console.WriteLine("Создание билетов...");
                var passengers = db.Passenger.ToList();
                var trains = db.Train.ToList();
                var sellers = new[] { "Иванов", "Петров", "Сидоров" };
                foreach (var train in trains)
                {
                    var trainCarriages = db.Carriage
                        .Where(c => c.TrainId == train.TrainId)
                        .ToList();
                    var trainSeats = db.Seat
                        .Where(s => trainCarriages.Select(c => c.CarriageId).Contains(s.CarriageId))
                        .Where(s => !s.IsBooked)
                        .ToList();
                    for (int i = 0; i < 5 && i < trainSeats.Count; i++)
                    {
                        var passenger = passengers[rand.Next(passengers.Count)];
                        var seat = trainSeats[i];
                        seat.IsBooked = true;

                        db.Ticket.Add(new Ticket
                        {
                            Price = rand.Next(500, 3000),
                            IsSold = true,
                            DataProdaji = DateTime.Now.AddDays(-rand.Next(1, 30)),
                            SellerName = sellers[rand.Next(sellers.Length)],
                            TrainId = train.TrainId,
                            PassengerId = passenger.PassengerId
                        });
                    }
                }
                db.SaveChanges();
                for (int i = 0; i < 5; i++)
                {
                    var train = trains[rand.Next(trains.Count)];

                    db.Ticket.Add(new Ticket
                    {
                        Price = rand.Next(500, 3000),
                        IsSold = false,
                        DataProdaji = DateTime.Now.AddDays(-rand.Next(1, 10)),
                        SellerName = null,
                        TrainId = train.TrainId,
                        PassengerId = null
                    });
                }
                db.SaveChanges();
            }
        }
    }
}