using HotChocolate.Subscriptions;
using PracticeGraphQL2.DataAccess.DAO;
using PracticeGraphQL2.DataAccess.Entity;

namespace PracticeGraphQL2.DataAccess.Data
{
    public class Query
    {
        public List<Passenger> AllPassengersByTrain([Service] PassengerRepository passengerRepository,int trainId) => passengerRepository.GetPassengersByTrain(trainId);
        //public List<Ticket> AllMovementsByPassenger([Service] PassengerRepository passengerRepository,int passengerId) =>passengerRepository.GetAllMovementsByPassenger(passengerId);
        //public Passenger? PassengerBySeatOnTrain([Service] PassengerRepository passengerRepository,int trainId,int seatNumber)=>passengerRepository.GetPassengerBySeatOnTrain(trainId,seatNumber);
        //public List<Passenger> PassengersByCarriage([Service] PassengerRepository passengerRepository,int trainId, int carriageNumber)=>passengerRepository.GetPassengersByCarriage(trainId,carriageNumber);

        //public List<Seat> FreeSeatsOnTrain([Service] SeatRepository seatRepository,int trainId)=>seatRepository.GetFreeSeatsOnTrain(trainId);
        //public List<Seat> FreeSeatsByPriceRange([Service] SeatRepository seatRepository,int trainId, decimal minPrice, decimal maxPrice)=>seatRepository.GetFreeSeatsByPriceRange(trainId,minPrice,maxPrice);

        //public decimal TotalSoldTicketsCostsByPeriod([Service] TicketRepository ticketRepository, DateTime start, DateTime end) => ticketRepository.GetTotalSoldTicketsCostsByPeriod(start, end);
        public Ticket TicketById([Service]TicketRepository ticketRepository,int id)=>ticketRepository.GetTicketById(id);

        public List<Train> AllTrains([Service]TrainRepository trainRepository)=>trainRepository.GetAllTrains();
    }

}
