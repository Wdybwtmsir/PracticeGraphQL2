using Microsoft.EntityFrameworkCore;
using PracticeGraphQL2.DataAccess.Entity;

public class PassengerRepository
{
    private readonly DbContext _context;

    public PassengerRepository(DbContext context)
    {
        _context = context;
    }

    public Passenger GetById(int id)
    {
        return _context.Set<Passenger>()
            .FirstOrDefault(p => p.PassengerId == id);
    }

    public List<Passenger> GetAll()
    {
        return _context.Set<Passenger>().ToList();
    }