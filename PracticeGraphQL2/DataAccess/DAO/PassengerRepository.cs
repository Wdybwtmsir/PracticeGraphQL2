using PracticeGraphQL2.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
public class PassengerRepository
{
    private readonly SampleAppDbContext _context;
    public PassengerRepository(SampleAppDbContext context)
    {
        _context = context;
    }
    public List<Passenger> GetPassengersByTrain(int trainId)
    {
        return _context.Ticket
            .Where(t => t.TrainId == trainId && t.IsSold)
            .Include(t => t.Passenger)
            .Select(t => t.Passenger!)
            .Distinct()
            .ToList();
    }
    public List<Ticket> GetAllMovementsByPassenger(int passengerId)
    {
        return _context.Ticket
            .Where(t => t.PassengerId == passengerId && t.IsSold)
            .Include(t => t.Train)
            .Include(t => t.Seat)
            .ToList();
    }

  
    public Passenger? GetPassengerBySeatOnTrain(int trainId, int seatNumber)
    {
        var ticket = _context.Ticket
            .Include(t => t.Passenger)
            .Include(t => t.Seat)
            .FirstOrDefault(t => t.TrainId == trainId
                              && t.Seat!.Number == seatNumber
                              && t.IsSold);
        return ticket?.Passenger;
    }

    public List<Passenger> GetPassengersByCarriage(int trainId, int carriageNumber)
    {
        return _context.Ticket
            .Where(t => t.TrainId == trainId
                     && t.IsSold
                     && t.Seat!.Carriage!.Number == carriageNumber)
            .Include(t => t.Passenger)
            .Select(t => t.Passenger!)
            .Distinct()
            .ToList();
    }
}