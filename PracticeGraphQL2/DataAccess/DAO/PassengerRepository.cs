using PracticeGraphQL2.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace PracticeGraphQL2.DataAccess.DAO
{
    public class PassengerRepository
    {
        private readonly SampleAppDbContext _context;
        public PassengerRepository(SampleAppDbContext context)
        {
            _context = context;
        }
        public List<Passenger> GetAllPassengers()
        {
            return _context.Passengers.ToList();
        }
        public List<Passenger> GetAllPassengersWithTicket()
        {
            return _context.Passenger.Include(d=>d.Ticket).ToList();
        }
        public async Task<Passenger> CreatePassenger(Passenger passenger)
        {
            await _context.Passenger.AddAsync(passenger);
            await _context.SaveChangesAsync();
            return passenger;
        }
    }
}
