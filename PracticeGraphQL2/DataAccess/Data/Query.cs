using HotChocolate.Subscriptions;
using PracticeGraphQL2.DataAccess.DAO;
using PracticeGraphQL2.DataAccess.Entity;

namespace PracticeGraphQL2.DataAccess.Data
{
    public class Query
    {
        public Carriage CarriageById([Service] CarriageRepository carriageRepository, int id) => carriageRepository.GetById(id);
        public List<Carriage> CarriagesByTrain([Service] CarriageRepository carriageRepository, int trainId) => carriageRepository.GetCarriagesByTrain(trainId);
        public List<Seat> AvailableSeatsInCarriage([Service] CarriageRepository carriageRepository,int carriageId)=>carriageRepository.GetAvailableSeatsInCarriage(carriageId);

        public Passenger PassengerById([Service] PassengerRepository passengerRepository,int id)=>passengerRepository.GetById(id);
        public List<Passenger> All([Service] PassengerRepository passengerRepository)=>passengerRepository.GetAll();
        public List<PassengerMovement> PassengerMovement([Service] PassengerRepository passengerRepository,int passengerId) => passengerRepository.GetPassengerMovements(passengerId);
        public List<Passenger> PassengersByTrain([Service] PassengerRepository passengerRepository,int trainId) => passengerRepository.GetPassengersByTrain(trainId);
        public List<Passenger> PassengersByCarriage([Service] PassengerRepository passengerRepository,int trainId, int carriageNumber)=>passengerRepository.GetPassengersByCarriage(trainId,carriageNumber);
        public Passenger PassengerBySeat([Service] PassengerRepository passengerRepository, int trainId, int carriageNumber, int seatNumber) => passengerRepository.GetPassengerBySeat(trainId, carriageNumber, seatNumber);


        public Seat SeatById([Service] SeatRepository seatRepository,int id)=>seatRepository.GetById(id);
        public List<Seat> SeatsByCarriage([Service]SeatRepository seatRepository,int carriageId)=>seatRepository.GetSeatsByCarriage(carriageId);
        public List<Seat> BookedSeatsByTrain([Service] SeatRepository seatRepository,int trainId)=>seatRepository.GetBookedSeatsByTrain(trainId);


        public Ticket TicketById([Service]TicketRepository ticketRepository,int id)=>ticketRepository.GetTicketById(id);
        public List<Ticket> All([Service] TicketRepository ticketRepository)=>ticketRepository.GetAll();
        public decimal TotalSoldTicketsRevenue([Service]TicketRepository ticketRepository,DateTime startDate, DateTime endDate)=>ticketRepository.GetTotalSoldTicketsRevenue(startDate, endDate);
        public List<Ticket> SoldTicketsByPeriod([Service]TicketRepository ticketRepository,DateTime startDate, DateTime endDate)=>ticketRepository.GetSoldTicketsByPeriod(startDate, endDate);
        public List<Ticket> TicketsByPassenger([Service]TicketRepository ticketRepository,int passengerId)=>ticketRepository.GetTicketsByPassenger(passengerId);
        public List<Ticket> TicketsByTrain([Service]TicketRepository ticketRepository,int trainId)=>ticketRepository.GetTicketsByTrain(trainId);


        public Train TrainById([Service]TrainRepository trainRepository,int trainId)=>trainRepository.GetById(trainId);
        public List<Train> AllTrains([Service]TrainRepository trainRepository)=>trainRepository.GetAll();
        public List<Seat> AvailableSeats([Service]TrainRepository trainRepository,int trainId)=>trainRepository.GetAvailableSeats(trainId);
        public List<Seat> AvailableSeatsByPriceRange([Service] TrainRepository trainRepository, int trainId, decimal minPrice, decimal maxPrice) => trainRepository.GetAvailableSeatsByPriceRange(trainId, minPrice, maxPrice);
        public decimal TotalSoldTicketsPriceForTrain([Service]TrainRepository trainRepository,int trainId)=>trainRepository.GetTotalSoldTicketsPriceForTrain(trainId);
        public decimal TotalUnsoldTicketsPriceForTrain([Service] TrainRepository trainRepository, int trainId) => trainRepository.GetTotalUnsoldTicketsPriceForTrain(trainId);
    }
}
