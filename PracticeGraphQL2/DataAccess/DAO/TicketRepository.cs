using PracticeGraphQL2.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
public class TicketRepository
{
    private readonly SampleAppDbContext _context;
    public TicketRepository(SampleAppDbContext context)
    {
        _context = context;
    }

    public decimal GetTotalSoldTicketsCostsByPeriod(DateTime start, DateTime end)
    {
        return _context.Ticket
            .Where(t => t.IsSold && t.DataProdaji >= start && t.DataProdaji <= end)
            .Sum(t => t.Price);
    }
}