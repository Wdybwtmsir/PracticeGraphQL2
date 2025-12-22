using PracticeGraphQL2.DataAccess.Entity;

public class TrainRepository
{
    private readonly SampleAppDbContext _context;
    public TrainRepository(SampleAppDbContext context)
    {
        _context = context;
    }

    public List<Train> GetAllTrains()
    {
        return _context.Train.ToList();
    }

    public decimal GetTotalSoldTicketsCostOnTrain(int trainId)
    {
        return _context.Ticket
            .Where(t => t.TrainId == trainId && t.IsSold)
            .Sum(t => t.Price);
    }
    public decimal GetTotalUnsoldTicketsCostOnTrain(int trainId)
    {
        return _context.Ticket
            .Where(t => t.TrainId == trainId && !t.IsSold)
            .Sum(t => t.Price);
    }
}