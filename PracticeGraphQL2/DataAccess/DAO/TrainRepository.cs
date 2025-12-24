using Microsoft.EntityFrameworkCore;
using PracticeGraphQL2.DataAccess.Entity;

namespace PracticeGraphQL2.DataAccess.DAO
{
    public class TrainRepository
    {
        private readonly SampleAppDbContext _context;

        public TrainRepository(SampleAppDbContext context)
        {
            _context = context;
        }

        public Train GetById(int trainId)
        {
            return _context.Set<Train>()
                .Include(t => t.Carriages)
                    .ThenInclude(c => c.Seats)
                .Include(t => t.Tickets)
                .FirstOrDefault(t => t.TrainId == trainId);
        }

        public List<Train> GetAll()
        {
            return _context.Set<Train>()
                .Include(t => t.Carriages)
                    .ThenInclude(c => c.Seats)
                .Include(t => t.Tickets)
                .ToList();
        }

        public List<Seat> GetAvailableSeats(int trainId)
        {
            return _context.Set<Seat>()
                .Include(s => s.Carriage) 
                .Where(s => s.Carriage != null &&
                       s.Carriage.TrainId == trainId &&
                       !s.IsBooked)
                .ToList();
        }

        public List<Seat> GetAvailableSeatsByPriceRange(int trainId, decimal minPrice, decimal maxPrice)
        {
            var availableSeats = GetAvailableSeats(trainId);
            return availableSeats
                .Where(s =>
                {
                    var seatPrice = CalculateSeatPrice(s);
                    return seatPrice >= minPrice && seatPrice <= maxPrice;
                })
                .ToList();
        }

        private decimal CalculateSeatPrice(Seat seat)
        {
            if (seat.Carriage == null) return 1000m;
            return seat.Carriage.Number switch
            {
                1 or 2 => 1500m,
                3 or 4 => 2500m,
                _ => 5000m
            };
        }

        public decimal GetTotalSoldTicketsPriceForTrain(int trainId)
        {
            return _context.Set<Ticket>()
                .Where(t => t.TrainId == trainId && t.IsSold)
                .Sum(t => t.Price);
        }

        public decimal GetTotalUnsoldTicketsPriceForTrain(int trainId)
        {
            return _context.Set<Ticket>()
                .Where(t => t.TrainId == trainId && !t.IsSold)
                .Sum(t => t.Price);
        }
    }
}