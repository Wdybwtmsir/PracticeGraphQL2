using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PracticeGraphQLClient2.DataAccess
{
    public class Query
    {
        private static GraphQLHttpClient graphQLHttpClient;

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

        public static async Task<List<T>> ExecuteQueryReturnListAsync<T>(string graphQLQueryType, string query, object? variables = null)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = query,
                    Variables = variables
                };

                var response = await graphQLHttpClient.SendQueryAsync<JObject>(request);

                if (response.Errors?.Any() == true)
                {
                    var errors = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL errors: {errors}");
                }

                var stringResult = response.Data.ToString();

                if (response.Data.TryGetValue(graphQLQueryType, out var dataToken))
                {
                    return dataToken.ToObject<List<T>>() ?? new List<T>();
                }

                return new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в ExecuteQueryReturnListAsync: {ex.Message}");
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

                var response = await graphQLHttpClient.SendQueryAsync<JObject>(request);

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
    }
}