using Microsoft.EntityFrameworkCore;
using PracticeGraphQL2.DataAccess.Entity;

public class TrainRepository
{
    private readonly DbContext _context;

    public TrainRepository(DbContext context)
    {
        _context = context;
    }

    public Train GetById(int trainId)
    {
        return _context.Set<Train>()
            .FirstOrDefault(t => t.Id == trainId);
    }

    public List<Train> GetAll()
    {
        return _context.Set<Train>()
            .ToList();
    }

    public List<Seat> GetAvailableSeats(int trainId)
    {
        
        var occupiedSeatIds = _context.Set<Ticket>()
            .Where(t => t.TrainId == trainId)
            .Select(t => t.SeatId)
            .ToList();

        
        return _context.Set<Seat>()
            .Where(s => s.TrainId == trainId && !occupiedSeatIds.Contains(s.Id))
            .ToList();
    }
}