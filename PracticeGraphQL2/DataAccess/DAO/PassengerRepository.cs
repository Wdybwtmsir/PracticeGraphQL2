using Microsoft.EntityFrameworkCore;
using PracticeGraphQL2.DataAccess.Entity;

namespace PracticeGraphQL2.DataAccess.DAO
{
    public class PassengerRepository
    {
        private readonly SampleAppDbContext _context;

        public PassengerRepository(SampleAppDbContext context)
        {
            _context = context;
        }

        public Passenger GetById(int id)
        {
            return _context.Set<Passenger>()
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Train)
                .FirstOrDefault(p => p.PassengerId == id);
        }

        public List<Passenger> GetAll()
        {
            return _context.Set<Passenger>().ToList();
        }

        public List<Passenger> GetPassengersByTrain(int trainId)
        {
            return _context.Set<Passenger>()
                .Include(p => p.Tickets)
                .Where(p => p.Tickets.Any(t => t.TrainId == trainId && t.IsSold))
                .Distinct()
                .ToList();
        }

        public List<Passenger> GetPassengersByCarriage(int trainId, int carriageNumber)
        {
            return GetPassengersByTrain(trainId);
        }

        public List<PassengerMovement> GetPassengerMovements(int passengerId)
        {
            return _context.Set<Ticket>()
                .Include(t => t.Train)
                .Include(t => t.Passenger)
                .Where(t => t.PassengerId == passengerId && t.IsSold)
                .OrderBy(t => t.DataProdaji)
                .Select(t => new PassengerMovement
                {
                    TicketId = t.TicketId,
                    TrainName = t.Train.TrainName,
                    TrainNumber = t.Train.TrainNumber,
                    Route = t.Train.TrainRoute,
                    DepartureStation = t.Train.TrainStanciyaOtpravlenya,
                    ArrivalStation = t.Train.TrainStanciyaPribitiya,
                    PurchaseDate = t.DataProdaji,
                    TicketPrice = t.Price,
                    SellerName = t.SellerName
                })
                .ToList();
        }

        public Passenger GetPassengerBySeat(int trainId, int carriageNumber, int seatNumber)
        {
            var tickets = _context.Set<Ticket>()
                .Include(t => t.Passenger)
                .Include(t => t.Train)
                .Where(t => t.TrainId == trainId && t.IsSold)
                .ToList();

            return tickets.FirstOrDefault()?.Passenger;
        }
    }
    public class PassengerMovement
    {
        public int TicketId { get; set; }
        public string TrainName { get; set; }
        public int TrainNumber { get; set; }
        public string Route { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TicketPrice { get; set; }
        public string SellerName { get; set; }
    }
}