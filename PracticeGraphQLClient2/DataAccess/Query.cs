using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
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
            var uri = new Uri("https://localhost:7038/graphql/");
            var graphQLOptions = new GraphQLHttpClientOptions
            {
                EndPoint = uri
            };
            graphQLHttpClient = new GraphQLHttpClient(graphQLOptions, new NewtonsoftJsonSerializer());
        }

        // Универсальный метод для получения списка
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
                return new List<T>();
            }
        }

        // Универсальный метод для получения одного объекта
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

        // Метод для получения decimal (для выручки)
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
                return 0;
            }
        }

        // === TICKETS ===
        public static async Task<List<Ticket>> GetAllTickets()
        {
            string query = @"
                query {
                    all {
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
            return await ExecuteQueryListAsync<Ticket>("all", query);
        }

        public static async Task<Ticket> GetTicketById(int ticketId)
        {
            string query = @"
                query GetTicketById($id: Int!) {
                    ticketById(id: $id) {
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
            return await ExecuteQueryAsync<Ticket>("ticketById", query, variables);
        }

        public static async Task<List<Ticket>> GetSoldTicketsByPeriod(DateTime startDate, DateTime endDate)
        {
            string query = @"
                query GetSoldTicketsByPeriod($startDate: DateTime!, $endDate: DateTime!) {
                    soldTicketsByPeriod(startDate: $startDate, endDate: $endDate) {
                        ticketId
                        price
                        isSold
                        dataProdaji
                        sellerName
                        trainId
                        passengerId
                    }
                }";
            var variables = new
            {
                startDate = startDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                endDate = endDate.ToString("yyyy-MM-ddTHH:mm:ss")
            };
            return await ExecuteQueryListAsync<Ticket>("soldTicketsByPeriod", query, variables);
        }

        public static async Task<decimal> GetTotalSoldTicketsRevenue(DateTime startDate, DateTime endDate)
        {
            string query = @"
                query GetTotalSoldTicketsRevenue($startDate: DateTime!, $endDate: DateTime!) {
                    totalSoldTicketsRevenue(startDate: $startDate, endDate: $endDate)
                }";
            var variables = new
            {
                startDate = startDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                endDate = endDate.ToString("yyyy-MM-ddTHH:mm:ss")
            };
            return await ExecuteQueryDecimalAsync("totalSoldTicketsRevenue", query, variables);
        }

        public static async Task<List<Ticket>> GetTicketsByPassenger(int passengerId)
        {
            string query = @"
                query GetTicketsByPassenger($passengerId: Int!) {
                    ticketsByPassenger(passengerId: $passengerId) {
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
            return await ExecuteQueryListAsync<Ticket>("ticketsByPassenger", query, variables);
        }

        public static async Task<List<Ticket>> GetTicketsByTrain(int trainId)
        {
            string query = @"
                query GetTicketsByTrain($trainId: Int!) {
                    ticketsByTrain(trainId: $trainId) {
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
            return await ExecuteQueryListAsync<Ticket>("ticketsByTrain", query, variables);
        }

        // === TRAINS ===
        public static async Task<List<Train>> GetAllTrains()
        {
            string query = @"
                query {
                    allTrains {
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
            return await ExecuteQueryListAsync<Train>("allTrains", query);
        }

        public static async Task<Train> GetTrainById(int trainId)
        {
            string query = @"
                query GetTrainById($trainId: Int!) {
                    trainById(trainId: $trainId) {
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
            return await ExecuteQueryAsync<Train>("trainById", query, variables);
        }

        public static async Task<List<Seat>> GetAvailableSeats(int trainId)
        {
            string query = @"
                query GetAvailableSeats($trainId: Int!) {
                    availableSeats(trainId: $trainId) {
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
            return await ExecuteQueryListAsync<Seat>("availableSeats", query, variables);
        }

        public static async Task<List<Seat>> GetAvailableSeatsByPriceRange(int trainId, decimal minPrice, decimal maxPrice)
        {
            string query = @"
                query GetAvailableSeatsByPriceRange($trainId: Int!, $minPrice: Decimal!, $maxPrice: Decimal!) {
                    availableSeatsByPriceRange(trainId: $trainId, minPrice: $minPrice, maxPrice: $maxPrice) {
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
            return await ExecuteQueryListAsync<Seat>("availableSeatsByPriceRange", query, variables);
        }

        public static async Task<decimal> GetTotalSoldTicketsPriceForTrain(int trainId)
        {
            string query = @"
                query GetTotalSoldTicketsPriceForTrain($trainId: Int!) {
                    totalSoldTicketsPriceForTrain(trainId: $trainId)
                }";
            var variables = new { trainId };
            return await ExecuteQueryDecimalAsync("totalSoldTicketsPriceForTrain", query, variables);
        }

        public static async Task<decimal> GetTotalUnsoldTicketsPriceForTrain(int trainId)
        {
            string query = @"
                query GetTotalUnsoldTicketsPriceForTrain($trainId: Int!) {
                    totalUnsoldTicketsPriceForTrain(trainId: $trainId)
                }";
            var variables = new { trainId };
            return await ExecuteQueryDecimalAsync("totalUnsoldTicketsPriceForTrain", query, variables);
        }

        // === PASSENGERS ===
        public static async Task<List<Passenger>> GetAllPassengers()
        {
            string query = @"
                query {
                    all {
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
            return await ExecuteQueryListAsync<Passenger>("all", query);
        }

        public static async Task<Passenger> GetPassengerById(int passengerId)
        {
            string query = @"
                query GetPassengerById($id: Int!) {
                    passengerById(id: $id) {
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
            var variables = new { id = passengerId };
            return await ExecuteQueryAsync<Passenger>("passengerById", query, variables);
        }

        public static async Task<List<PassengerMovement>> GetPassengerMovements(int passengerId)
        {
            string query = @"
                query GetPassengerMovements($passengerId: Int!) {
                    passengerMovement(passengerId: $passengerId) {
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
            return await ExecuteQueryListAsync<PassengerMovement>("passengerMovement", query, variables);
        }

        public static async Task<List<Passenger>> GetPassengersByTrain(int trainId)
        {
            string query = @"
                query GetPassengersByTrain($trainId: Int!) {
                    passengersByTrain(trainId: $trainId) {
                        passengerId
                        firstName
                        lastName
                        surName
                    }
                }";
            var variables = new { trainId };
            return await ExecuteQueryListAsync<Passenger>("passengersByTrain", query, variables);
        }

        public static async Task<Passenger> GetPassengerBySeat(int trainId, int carriageNumber, int seatNumber)
        {
            string query = @"
                query GetPassengerBySeat($trainId: Int!, $carriageNumber: Int!, $seatNumber: Int!) {
                    passengerBySeat(trainId: $trainId, carriageNumber: $carriageNumber, seatNumber: $seatNumber) {
                        passengerId
                        firstName
                        lastName
                        surName
                        email
                    }
                }";
            var variables = new { trainId, carriageNumber, seatNumber };
            return await ExecuteQueryAsync<Passenger>("passengerBySeat", query, variables);
        }

        public static async Task<List<Passenger>> GetPassengersByCarriage(int trainId, int carriageNumber)
        {
            string query = @"
                query GetPassengersByCarriage($trainId: Int!, $carriageNumber: Int!) {
                    passengersByCarriage(trainId: $trainId, carriageNumber: $carriageNumber) {
                        passengerId
                        firstName
                        lastName
                        surName
                    }
                }";
            var variables = new { trainId, carriageNumber };
            return await ExecuteQueryListAsync<Passenger>("passengersByCarriage", query, variables);
        }

        // === CARRIAGES ===
        public static async Task<Carriage> GetCarriageById(int carriageId)
        {
            string query = @"
                query GetCarriageById($id: Int!) {
                    carriageById(id: $id) {
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
            return await ExecuteQueryAsync<Carriage>("carriageById", query, variables);
        }

        public static async Task<List<Carriage>> GetCarriagesByTrain(int trainId)
        {
            string query = @"
                query GetCarriagesByTrain($trainId: Int!) {
                    carriagesByTrain(trainId: $trainId) {
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
            return await ExecuteQueryListAsync<Carriage>("carriagesByTrain", query, variables);
        }

        public static async Task<List<Seat>> GetAvailableSeatsInCarriage(int carriageId)
        {
            string query = @"
                query GetAvailableSeatsInCarriage($carriageId: Int!) {
                    availableSeatsInCarriage(carriageId: $carriageId) {
                        seatId
                        number
                        isBooked
                    }
                }";
            var variables = new { carriageId };
            return await ExecuteQueryListAsync<Seat>("availableSeatsInCarriage", query, variables);
        }

        // === SEATS ===
        public static async Task<Seat> GetSeatById(int seatId)
        {
            string query = @"
                query GetSeatById($id: Int!) {
                    seatById(id: $id) {
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
            return await ExecuteQueryAsync<Seat>("seatById", query, variables);
        }

        public static async Task<List<Seat>> GetSeatsByCarriage(int carriageId)
        {
            string query = @"
                query GetSeatsByCarriage($carriageId: Int!) {
                    seatsByCarriage(carriageId: $carriageId) {
                        seatId
                        number
                        isBooked
                    }
                }";
            var variables = new { carriageId };
            return await ExecuteQueryListAsync<Seat>("seatsByCarriage", query, variables);
        }

        public static async Task<List<Seat>> GetBookedSeatsByTrain(int trainId)
        {
            string query = @"
                query GetBookedSeatsByTrain($trainId: Int!) {
                    bookedSeatsByTrain(trainId: $trainId) {
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
            return await ExecuteQueryListAsync<Seat>("bookedSeatsByTrain", query, variables);
        }
    }
}