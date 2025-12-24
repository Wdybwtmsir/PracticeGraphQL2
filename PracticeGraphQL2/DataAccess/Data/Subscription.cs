using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using PracticeGraphQL2.DataAccess.Entity;

namespace PracticeGraphQL2.DataAccess.Data
{
    public class Subscription
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Ticket>> OnTicketCreate([Service] ITopicEventReceiver eventReceiver, CancellationToken cancalltionToken)
        {
            return await eventReceiver.SubscribeAsync<Ticket>("TicketCreated", cancalltionToken);
        }
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Ticket>> OnTicketGet([Service] ITopicEventReceiver eventReceiver, CancellationToken cancalltionToken)
        {
            return await eventReceiver.SubscribeAsync<Ticket>("ReturnedTicket", cancalltionToken);
        }
    }
}
