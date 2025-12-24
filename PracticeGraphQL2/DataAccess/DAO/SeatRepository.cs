using Microsoft.EntityFrameworkCore;
using PracticeGraphQL2.DataAccess.Entity;

namespace PracticeGraphQL2.DataAccess.DAO
{
    public class SeatRepository
    {
        private readonly SampleAppDbContext _context;

        public SeatRepository(SampleAppDbContext context)
        {
            _context = context;
        }

        public Seat GetById(int id)
        {
            return _context.Set<Seat>()
                .Include(s => s.Carriage) 
                .FirstOrDefault(s => s.SeatId == id);
        }

        public List<Seat> GetSeatsByCarriage(int carriageId)
        {
            return _context.Set<Seat>()
                .Include(s => s.Carriage) 
                .Where(s => s.CarriageId == carriageId)
                .OrderBy(s => s.Number)
                .ToList();
        }

        public List<Seat> GetBookedSeatsByTrain(int trainId)
        {
            return _context.Set<Seat>()
                .Include(s => s.Carriage) 
                .Where(s => s.Carriage != null &&
                       s.Carriage.TrainId == trainId &&
                       s.IsBooked)
                .ToList();
        }
    }
}