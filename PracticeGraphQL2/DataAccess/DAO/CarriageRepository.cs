using Microsoft.EntityFrameworkCore;
using PracticeGraphQL2.DataAccess.Entity;

namespace PracticeGraphQL2.DataAccess.DAO
{
    public class CarriageRepository
    {
        private readonly SampleAppDbContext _context;

        public CarriageRepository(SampleAppDbContext context)
        {
            _context = context;
        }

        public Carriage GetById(int id)
        {
            return _context.Set<Carriage>()
                .Include(c => c.Seats)
                .Include(c => c.Train)
                .FirstOrDefault(c => c.CarriageId == id);
        }

        public List<Carriage> GetCarriagesByTrain(int trainId)
        {
            return _context.Set<Carriage>()
                .Include(c => c.Seats)
                .Include(c => c.Train)
                .Where(c => c.TrainId == trainId)
                .OrderBy(c => c.Number)
                .ToList();
        }

        public List<Seat> GetAvailableSeatsInCarriage(int carriageId)
        {
            return _context.Set<Seat>()
                .Include(s => s.Carriage) 
                .Where(s => s.CarriageId == carriageId && !s.IsBooked)
                .OrderBy(s => s.Number)
                .ToList();
        }
    }
}