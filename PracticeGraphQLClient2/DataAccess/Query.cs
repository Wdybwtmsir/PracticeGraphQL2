using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PracticeGraphQLClient2.DataAccess.Model;

namespace PracticeGraphQLClient2.DataAccess
{
    public class Query
    {
        private static GraphQLHttpClient? graphQLHttpClient;

        static Query()
        {
            var uri = new Uri("https://localhost:7153/graphql/");
            var graphQLOptions = new GraphQLHttpClientOptions
            {
                EndPoint = uri,
                HttpMessageHandler = new NativeMessageHandler(),
            };
            graphQLHttpClient = new GraphQLHttpClient(graphQLOptions, new NewtonsoftJsonSerializer());
        }
        public static async Task<List<T>> ExecuteQueryListAsync<T>(string graphQLQueryType, string query, object? variables = null)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = query,
                    Variables = variables
                };

                var response = await graphQLHttpClient!.SendQueryAsync<JObject>(request);

                if (response.Errors?.Any() == true)
                {
                    var errors = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL errors: {errors}");
                }

                if (response.Data.TryGetValue(graphQLQueryType, out var dataToken))
                {
                    return dataToken.ToObject<List<T>>() ?? new List<T>();
                }

                return new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в ExecuteQueryListAsync: {ex.Message}");
                throw;
            }
        }
        public static async Task<T> ExecuteQueryAsync<T>(string graphQLQueryType, string query, object? variables = null)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = query,
                    Variables = variables
                };

                var response = await graphQLHttpClient!.SendQueryAsync<JObject>(request);

                if (response.Errors?.Any() == true)
                {
                    var errors = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL errors: {errors}");
                }

                if (response.Data.TryGetValue(graphQLQueryType, out var dataToken))
                {
                    return dataToken.ToObject<T>()!;
                }

                return default!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в ExecuteQueryAsync: {ex.Message}");
                throw;
            }
        }
        public static async Task<decimal> ExecuteQueryDecimalAsync(string graphQLQueryType, string query, object? variables = null)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = query,
                    Variables = variables
                };

                var response = await graphQLHttpClient!.SendQueryAsync<JObject>(request);

                if (response.Errors?.Any() == true)
                {
                    var errors = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL errors: {errors}");
                }

                if (response.Data.TryGetValue(graphQLQueryType, out var dataToken))
                {
                    return dataToken.Value<decimal>();
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в ExecuteQueryDecimalAsync: {ex.Message}");
                throw;
            }
        }
        public static async Task<List<Ticket>> GetAllTickets()
        {
            string query = @"
                query {
                    All {
                        ticketId
                        price
                        isSold
                        dataProdaji
                        sellerName
                        trainId
                        passengerId
                        train {
                            trainId
                            trainName
                            trainNumber
                            trainRoute
                        }
                        passenger {
                            passengerId
                            firstName
                            lastName
                            surName
                            email
                        }
                    }
                }";
            return await ExecuteQueryListAsync<Ticket>("All", query);
        }

        public static async Task<Ticket> GetTicketById(int ticketId)
        {
            string query = @"
                query GetTicketById($id: Int!) {
                    TicketById(id: $id) {
                        ticketId
                        price
                        isSold
                        dataProdaji
                        sellerName
                        trainId
                        passengerId
                        train {
                            trainId
                            trainName
                            trainNumber
                            trainRoute
                            trainStanciyaOtpravlenya
                            trainStanciyaPribitiya
                        }
                        passenger {
                            passengerId
                            firstName
                            lastName
                            surName
                            email
                            phone
                        }
                    }
                }";
            var variables = new { id = ticketId };
            return await ExecuteQueryAsync<Ticket>("TicketById", query, variables);
        }

        public static async Task<List<Ticket>> GetSoldTicketsByPeriod(DateTime startDate, DateTime endDate)
        {
            string query = @"
                query GetSoldTicketsByPeriod($startDate: DateTime!, $endDate: DateTime!) {
                    SoldTicketsByPeriod(startDate: $startDate, endDate: $endDate) {
                        ticketId
                        price
                        isSold
                        dataProdaji
                        sellerName
                        trainId
                        passengerId
                    }
                }";
            var variables = new { startDate = startDate.ToString("yyyy-MM-ddTHH:mm:ss"), endDate = endDate.ToString("yyyy-MM-ddTHH:mm:ss") };
            return await ExecuteQueryListAsync<Ticket>("SoldTicketsByPeriod", query, variables);
        }

        public static async Task<decimal> GetTotalSoldTicketsRevenue(DateTime startDate, DateTime endDate)
        {
            string query = @"
                query GetTotalSoldTicketsRevenue($startDate: DateTime!, $endDate: DateTime!) {
                    TotalSoldTicketsRevenue(startDate: $startDate, endDate: $endDate)
                }";
            var variables = new { startDate = startDate.ToString("yyyy-MM-ddTHH:mm:ss"), endDate = endDate.ToString("yyyy-MM-ddTHH:mm:ss") };
            return await ExecuteQueryDecimalAsync("TotalSoldTicketsRevenue", query, variables);
        }

        public static async Task<List<Ticket>> GetTicketsByPassenger(int passengerId)
        {
            string query = @"
                query GetTicketsByPassenger($passengerId: Int!) {
                    TicketsByPassenger(passengerId: $passengerId) {
                        ticketId
                        price
                        isSold
                        dataProdaji
                        sellerName
                        trainId
                        train {
                            trainName
                            trainNumber
                        }
                    }
                }";
            var variables = new { passengerId };
            return await ExecuteQueryListAsync<Ticket>("TicketsByPassenger", query, variables);
        }

        public static async Task<List<Ticket>> GetTicketsByTrain(int trainId)
        {
            string query = @"
                query GetTicketsByTrain($trainId: Int!) {
                    TicketsByTrain(trainId: $trainId) {
                        ticketId
                        price
                        isSold
                        dataProdaji
                        sellerName
                        passengerId
                        passenger {
                            firstName
                            lastName
                        }
                    }
                }";
            var variables = new { trainId };
            return await ExecuteQueryListAsync<Ticket>("TicketsByTrain", query, variables);
        }
        public static async Task<List<Train>> GetAllTrains()
        {
            string query = @"
                query {
                    AllTrains {
                        trainId
                        trainName
                        trainNumber
                        trainRoute
                        trainStanciyaOtpravlenya
                        trainStanciyaPribitiya
                        carriages {
                            carriageId
                            number
                        }
                    }
                }";
            return await ExecuteQueryListAsync<Train>("AllTrains", query);
        }

        public static async Task<Train> GetTrainById(int trainId)
        {
            string query = @"
                query GetTrainById($trainId: Int!) {
                    TrainById(trainId: $trainId) {
                        trainId
                        trainName
                        trainNumber
                        trainRoute
                        trainStanciyaOtpravlenya
                        trainStanciyaPribitiya
                        carriages {
                            carriageId
                            number
                            seats {
                                seatId
                                number
                                isBooked
                            }
                        }
                    }
                }";
            var variables = new { trainId };
            return await ExecuteQueryAsync<Train>("TrainById", query, variables);
        }

        public static async Task<List<Seat>> GetAvailableSeats(int trainId)
        {
            string query = @"
                query GetAvailableSeats($trainId: Int!) {
                    AvailableSeats(trainId: $trainId) {
                        seatId
                        number
                        isBooked
                        carriageId
                        carriage {
                            carriageId
                            number
                        }
                    }
                }";
            var variables = new { trainId };
            return await ExecuteQueryListAsync<Seat>("AvailableSeats", query, variables);
        }

        public static async Task<List<Seat>> GetAvailableSeatsByPriceRange(int trainId, decimal minPrice, decimal maxPrice)
        {
            string query = @"
                query GetAvailableSeatsByPriceRange($trainId: Int!, $minPrice: Decimal!, $maxPrice: Decimal!) {
                    AvailableSeatsByPriceRange(trainId: $trainId, minPrice: $minPrice, maxPrice: $maxPrice) {
                        seatId
                        number
                        isBooked
                        carriageId
                        carriage {
                            carriageId
                            number
                            trainId
                        }
                    }
                }";
            var variables = new { trainId, minPrice, maxPrice };
            return await ExecuteQueryListAsync<Seat>("AvailableSeatsByPriceRange", query, variables);
        }

        public static async Task<decimal> GetTotalSoldTicketsPriceForTrain(int trainId)
        {
            string query = @"
                query GetTotalSoldTicketsPriceForTrain($trainId: Int!) {
                    TotalSoldTicketsPriceForTrain(trainId: $trainId)
                }";
            var variables = new { trainId };
            return await ExecuteQueryDecimalAsync("TotalSoldTicketsPriceForTrain", query, variables);
        }

        public static async Task<decimal> GetTotalUnsoldTicketsPriceForTrain(int trainId)
        {
            string query = @"
                query GetTotalUnsoldTicketsPriceForTrain($trainId: Int!) {
                    TotalUnsoldTicketsPriceForTrain(trainId: $trainId)
                }";
            var variables = new { trainId };
            return await ExecuteQueryDecimalAsync("TotalUnsoldTicketsPriceForTrain", query, variables);
        }
        public static async Task<List<Passenger>> GetAllPassengers()
        {
            string query = @"
                query {
                    All {
                        passengerId
                        firstName
                        lastName
                        surName
                        email
                        phone
                        age
                        address
                    }
                }";
            return await ExecuteQueryListAsync<Passenger>("All", query);
        }

        public static async Task<Passenger> GetPassengerById(int passengerId)
        {
            string query = @"
                query GetPassengerById($id: Int!) {
                    PassengerById(id: $id) {
                        passengerId
                        firstName
                        lastName
                        surName
                        email
                        phone
                        age
                        address
                        tickets {
                            ticketId
                            price
                            dataProdaji
                            train {
                                trainName
                                trainNumber
                            }
                        }
                    }
                }";
            var variables = new { id = passengerId };
            return await ExecuteQueryAsync<Passenger>("PassengerById", query, variables);
        }

        public static async Task<List<PassengerMovement>> GetPassengerMovements(int passengerId)
        {
            string query = @"
                query GetPassengerMovements($passengerId: Int!) {
                    PassengerMovement(passengerId: $passengerId) {
                        ticketId
                        trainName
                        trainNumber
                        route
                        departureStation
                        arrivalStation
                        purchaseDate
                        ticketPrice
                        sellerName
                    }
                }";
            var variables = new { passengerId };
            return await ExecuteQueryListAsync<PassengerMovement>("PassengerMovement", query, variables);
        }

        public static async Task<List<Passenger>> GetPassengersByTrain(int trainId)
        {
            string query = @"
                query GetPassengersByTrain($trainId: Int!) {
                    PassengersByTrain(trainId: $trainId) {
                        passengerId
                        firstName
                        lastName
                        surName
                    }
                }";
            var variables = new { trainId };
            return await ExecuteQueryListAsync<Passenger>("PassengersByTrain", query, variables);
        }

        public static async Task<Passenger> GetPassengerBySeat(int trainId, int carriageNumber, int seatNumber)
        {
            string query = @"
                query GetPassengerBySeat($trainId: Int!, $carriageNumber: Int!, $seatNumber: Int!) {
                    PassengerBySeat(trainId: $trainId, carriageNumber: $carriageNumber, seatNumber: $seatNumber) {
                        passengerId
                        firstName
                        lastName
                        surName
                        email
                    }
                }";
            var variables = new { trainId, carriageNumber, seatNumber };
            return await ExecuteQueryAsync<Passenger>("PassengerBySeat", query, variables);
        }
        public static async Task<Carriage> GetCarriageById(int carriageId)
        {
            string query = @"
                query GetCarriageById($id: Int!) {
                    CarriageById(id: $id) {
                        carriageId
                        number
                        trainId
                        train {
                            trainName
                            trainNumber
                        }
                        seats {
                            seatId
                            number
                            isBooked
                        }
                    }
                }";
            var variables = new { id = carriageId };
            return await ExecuteQueryAsync<Carriage>("CarriageById", query, variables);
        }

        public static async Task<List<Carriage>> GetCarriagesByTrain(int trainId)
        {
            string query = @"
                query GetCarriagesByTrain($trainId: Int!) {
                    CarriagesByTrain(trainId: $trainId) {
                        carriageId
                        number
                        seats {
                            seatId
                            number
                            isBooked
                        }
                    }
                }";
            var variables = new { trainId };
            return await ExecuteQueryListAsync<Carriage>("CarriagesByTrain", query, variables);
        }

        public static async Task<List<Seat>> GetAvailableSeatsInCarriage(int carriageId)
        {
            string query = @"
                query GetAvailableSeatsInCarriage($carriageId: Int!) {
                    AvailableSeatsInCarriage(carriageId: $carriageId) {
                        seatId
                        number
                        isBooked
                    }
                }";
            var variables = new { carriageId };
            return await ExecuteQueryListAsync<Seat>("AvailableSeatsInCarriage", query, variables);
        }
        public static async Task<Seat> GetSeatById(int seatId)
        {
            string query = @"
                query GetSeatById($id: Int!) {
                    SeatById(id: $id) {
                        seatId
                        number
                        isBooked
                        carriageId
                        carriage {
                            carriageId
                            number
                            train {
                                trainName
                                trainNumber
                            }
                        }
                    }
                }";
            var variables = new { id = seatId };
            return await ExecuteQueryAsync<Seat>("SeatById", query, variables);
        }

        public static async Task<List<Seat>> GetSeatsByCarriage(int carriageId)
        {
            string query = @"
                query GetSeatsByCarriage($carriageId: Int!) {
                    SeatsByCarriage(carriageId: $carriageId) {
                        seatId
                        number
                        isBooked
                    }
                }";
            var variables = new { carriageId };
            return await ExecuteQueryListAsync<Seat>("SeatsByCarriage", query, variables);
        }

        public static async Task<List<Seat>> GetBookedSeatsByTrain(int trainId)
        {
            string query = @"
                query GetBookedSeatsByTrain($trainId: Int!) {
                    BookedSeatsByTrain(trainId: $trainId) {
                        seatId
                        number
                        isBooked
                        carriageId
                        carriage {
                            carriageId
                            number
                        }
                    }
                }";
            var variables = new { trainId };
            return await ExecuteQueryListAsync<Seat>("BookedSeatsByTrain", query, variables);
        }
    }
}