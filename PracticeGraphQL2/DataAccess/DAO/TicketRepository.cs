using PracticeGraphQL2.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
namespace PracticeGraphQL2.DataAccess.DAO;

    }
    //public async Task<Ticket> CreateTicket(Ticket tick)
    //{
    //    await _context.Ticket.AddAsync(tick);
    //    await _context.SaveChangesAsync();
    //    return tick;
    //}
    //public Ticket GetTicketById(int id)
    //{
    //    var ticket= _context.Ticket.Include(e=>e.TicketId).Where(e=>e.TicketId == id).FirstOrDefault();
    //    if(ticket!=null)return ticket;
    //    return null!;
    }
    //public async Task<Ticket> EditTicket(Ticket tick)
    //{
    //    try
    //    {
    //        var tickToUpdate = GetTicketById(tick.TicketId);
    //        if (tickToUpdate == null) return null!;
    //        tickToUpdate.Price = tick.Price;
    //        tickToUpdate.IsSold = tick.IsSold;
    //        tickToUpdate.DataProdaji = tick.DataProdaji;
    //        tickToUpdate.SellerName = tick.SellerName;
    //        tickToUpdate.TrainId = tick.TrainId;
    //        tickToUpdate.PassengerId = tick.PassengerId;
    //        tickToUpdate.Passenger = tick.Passenger;
    //        tickToUpdate.SeatId = tick.SeatId;
    //        tickToUpdate.Seat = tick.Seat;
    //        await _context.SaveChangesAsync();
    //        return tickToUpdate;
    //    }
    //    catch (Exception) { throw; }
    // }
    //public async Task<Ticket> DeleteTicket (int id)
    //{
    //    var tickDel = await _context.Ticket.FindAsync(id);
    //    if (tickDel == null) return null!;
    //    _context.Ticket.Remove(tickDel);
    //    await _context.SaveChangesAsync();
    //    return tickDel;
    //}
}