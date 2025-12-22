using PracticeGraphQL2.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

public class SeatRepository
{
    private readonly SampleAppDbContext _context;
    public SeatRepository(SampleAppDbContext context)
    {
        _context = context;
    }

    public List<Seat> GetFreeSeatsOnTrain(int trainId)
    {
        var seats = _context.Seat
            .Include(s => s.Carriage)
            .Where(s => s.Carriage != null
                        && s.Carriage.TrainId == trainId
                        && !s.IsBooked)
            .ToList();

        var soldSeatIds = _context.Ticket
            .Where(t => t.TrainId == trainId && t.IsSold)
            .Select(t => t.SeatId)
            .Distinct()
            .ToList();

        return seats.Where(s => !soldSeatIds.Contains(s.SeatId)).ToList();
    }
    public List<Seat> GetFreeSeatsByPriceRange(int trainId, decimal minPrice, decimal maxPrice)
    {
        var freeSeats = GetFreeSeatsOnTrain(trainId);
        return freeSeats.Where(s => s.Price >= minPrice && s.Price <= maxPrice).ToList();
    }
}
