using Microsoft.EntityFrameworkCore;
using PracticeGraphQL2.DataAccess.Entity;

namespace PracticeGraphQL2.DataAccess.DAO
{
    public class TicketRepository
    {
        private readonly SampleAppDbContext _context;

        public TicketRepository(SampleAppDbContext context)
        {
            _context = context;
        }

        public Ticket GetTicketById(int id)
        {
            return _context.Set<Ticket>()
                .Include(t => t.Passenger)
                .Include(t => t.Train)
                .FirstOrDefault(t => t.TicketId == id);
        }

        public List<Ticket> GetAll()
        {
            return _context.Set<Ticket>()
                .Include(t => t.Passenger)
                .Include(t => t.Train)
                .ToList();
        }

        public decimal GetTotalSoldTicketsRevenue(DateTime startDate, DateTime endDate)
        {
            return _context.Set<Ticket>()
                .Where(t => t.IsSold && t.DataProdaji >= startDate && t.DataProdaji <= endDate)
                .Sum(t => t.Price);
        }

        public List<Ticket> GetSoldTicketsByPeriod(DateTime startDate, DateTime endDate)
        {
            return _context.Set<Ticket>()
                .Include(t => t.Passenger)
                .Include(t => t.Train)
                .Where(t => t.IsSold && t.DataProdaji >= startDate && t.DataProdaji <= endDate)
                .OrderByDescending(t => t.DataProdaji)
                .ToList();
        }

        public List<Ticket> GetTicketsByPassenger(int passengerId)
        {
            return _context.Set<Ticket>()
                .Include(t => t.Train)
                .Where(t => t.PassengerId == passengerId)
                .OrderByDescending(t => t.DataProdaji)
                .ToList();
        }

        public List<Ticket> GetTicketsByTrain(int trainId)
        {
            return _context.Set<Ticket>()
                .Include(t => t.Passenger)
                .Where(t => t.TrainId == trainId)
                .ToList();
        }


        public async Task<Ticket> CreateTicket(Ticket tick)
        {
            await _context.Ticket.AddAsync(tick);
            await _context.SaveChangesAsync();
            return tick;
        }
        public async Task<Ticket> EditTicket(Ticket tick)
        {
            try
            {
                var tickToUpdate = GetTicketById(tick.TicketId);
                if (tickToUpdate == null) return null!;
                tickToUpdate.Price = tick.Price;
                tickToUpdate.IsSold = tick.IsSold;
                tickToUpdate.DataProdaji = tick.DataProdaji;
                tickToUpdate.SellerName = tick.SellerName;
                tickToUpdate.TrainId = tick.TrainId;
                tickToUpdate.PassengerId = tick.PassengerId;
                tickToUpdate.Passenger = tick.Passenger;
                await _context.SaveChangesAsync();
                return tickToUpdate;
            }
            catch (Exception) { throw; }
        }
        public async Task<Ticket> DeleteTicket(int id)
        {
            var tickDel = await _context.Ticket.FindAsync(id);
            if (tickDel == null) return null!;
            _context.Ticket.Remove(tickDel);
            await _context.SaveChangesAsync();
            return tickDel;
        }
    }
}

