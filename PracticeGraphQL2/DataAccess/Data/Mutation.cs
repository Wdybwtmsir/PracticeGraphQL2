using PracticeGraphQL2.DataAccess.Data;
using PracticeGraphQL2.DataAccess.Entity;
using PracticeGraphQL2.DataAccess.DAO;
using HotChocolate.Subscriptions;

namespace PracticeGraphQL2.DataAccess.Data
{
    public class Mutation
    {
        public async Task<Ticket> CreateTicketWithPassengerId([Service] TicketRepository ticketRepository, [Service] ITopicEventSender eventSender, decimal price,
            bool isSold, DateTime dataProdaji, string sellerName, int trainId, int passengerId)
        {
            Ticket tick = new Ticket
            { 
                Price = price,
                IsSold = isSold,
                DataProdaji = dataProdaji,
                SellerName = sellerName,
                TrainId = trainId,
                PassengerId = passengerId,
                
            };
            var createTick = await ticketRepository.CreateTicket(tick);
            return createTick;
        }
        public async Task<Ticket> EditTicketWithId([Service] TicketRepository ticketRepository, [Service] ITopicEventSender eventSender, int id, decimal price,
               bool isSold, DateTime dataProdaji, string sellerName, int trainId, int passengerId)
        {
            Ticket tick = new Ticket
            {
                TicketId = id,
                Price = price,
                IsSold = isSold,
                DataProdaji = dataProdaji,
                SellerName = sellerName,
                TrainId = trainId,
                PassengerId = passengerId,               
            };
            var editTick = await ticketRepository.EditTicket(tick);
            return editTick;
        }
        public async Task<Ticket> DeleteTicket([Service] TicketRepository ticketRepository, [Service] ITopicEventSender eventSender, int id)
        {
            return await ticketRepository.DeleteTicket(id);
        }
    }
}
