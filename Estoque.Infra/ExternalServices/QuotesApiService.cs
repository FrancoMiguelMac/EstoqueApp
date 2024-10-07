using Estoque.Crosscutting.Dtos;
using Estoque.Domain.Contracts.Services;
using Polly.CircuitBreaker;
using System.Text.Json;

namespace Estoque.Infra.ExternalServices
{
    public class QuotesApiService : IQuotesApiService
    {
        private readonly HttpClient _httpClient;

        public QuotesApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetQuoteByCurrencyCodes(string currencyCode, string anotherCurrencyCode)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{currencyCode}-{anotherCurrencyCode}");
                Console.WriteLine(response.IsSuccessStatusCode);

                string responseBody = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseBody))
                    return "1"; // Valor padrão para deixar o valor da moeda inalterado quando não consegue a integração.
                    
                Console.WriteLine(responseBody);

                using JsonDocument document = JsonDocument.Parse(responseBody);
                JsonElement root = document.RootElement;
                var json = root.GetProperty($"{currencyCode}{anotherCurrencyCode}").ToString();
                Console.WriteLine(json);

                QuoteServiceResponse? responseObj = JsonSerializer.Deserialize<QuoteServiceResponse>(json);

                return responseObj.Conversion;
            }
            catch (BrokenCircuitException)
            {
                Console.WriteLine("Circuit Breaker - Requisição bloqueada.");
                return "1"; // Valor padrão para deixar o valor da moeda inalterado quando não consegue a integração.
            }
        }
    }
}
