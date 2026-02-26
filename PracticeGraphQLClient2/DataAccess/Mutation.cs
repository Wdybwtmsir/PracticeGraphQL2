using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PracticeGraphQLClient2.DataAccess.Model;

namespace PracticeGraphQLClient2.DataAccess
{
    public class Mutation
    {
        private static GraphQLHttpClient? graphQLHttpClient;

        static Mutation()
        {
            var uri = new Uri("https://localhost:7153/graphql/");
            var graphQLOptions = new GraphQLHttpClientOptions
            {
                EndPoint = uri,
                HttpMessageHandler = new NativeMessageHandler(),
            };
            graphQLHttpClient = new GraphQLHttpClient(graphQLOptions, new NewtonsoftJsonSerializer());
        }

        public static async Task<T> ExecuteMutationAsync<T>(string graphQLQueryType, string query, object? variables = null)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = query,
                    Variables = variables
                };

                var response = await graphQLHttpClient!.SendMutationAsync<JObject>(request);

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
                Console.WriteLine($"Ошибка в ExecuteMutationAsync: {ex.Message}");
                throw;
            }
        }
        public static async Task<Ticket> CreateTicketWithPassengerId(decimal price, bool isSold, DateTime dataProdaji, string sellerName, int trainId, int passengerId)
        {
            string mutation = @"
                mutation CreateTicketWithPassengerId($price: Decimal!, $isSold: Boolean!, $dataProdaji: DateTime!, $sellerName: String!, $trainId: Int!, $passengerId: Int!) {
                    createTicketWithPassengerId(price: $price, isSold: $isSold, dataProdaji: $dataProdaji, sellerName: $sellerName, trainId: $trainId, passengerId: $passengerId) {
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
                price,
                isSold,
                dataProdaji = dataProdaji.ToString("yyyy-MM-ddTHH:mm:ss"),
                sellerName,
                trainId,
                passengerId
            };

            return await ExecuteMutationAsync<Ticket>("createTicketWithPassengerId", mutation, variables);
        }

        public static async Task<Ticket> EditTicketWithId(int id, decimal price, bool isSold, DateTime dataProdaji, string sellerName, int trainId, int passengerId)
        {
            string mutation = @"
                mutation EditTicketWithId($id: Int!, $price: Decimal!, $isSold: Boolean!, $dataProdaji: DateTime!, $sellerName: String!, $trainId: Int!, $passengerId: Int!) {
                    editTicketWithId(id: $id, price: $price, isSold: $isSold, dataProdaji: $dataProdaji, sellerName: $sellerName, trainId: $trainId, passengerId: $passengerId) {
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
                id,
                price,
                isSold,
                dataProdaji = dataProdaji.ToString("yyyy-MM-ddTHH:mm:ss"),
                sellerName,
                trainId,
                passengerId
            };

            return await ExecuteMutationAsync<Ticket>("editTicketWithId", mutation, variables);
        }

        public static async Task<Ticket> DeleteTicket(int ticketId)
        {
            string mutation = @"
                mutation DeleteTicket($id: Int!) {
                    deleteTicket(id: $id) {
                        ticketId
                    }
                }";

            var variables = new { id = ticketId };
            return await ExecuteMutationAsync<Ticket>("deleteTicket", mutation, variables);
        }
    }
}