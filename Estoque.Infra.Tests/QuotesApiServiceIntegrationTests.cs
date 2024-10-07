using Estoque.Infra.ExternalServices;
using Xunit;

namespace Estoque.Infra.Tests
{
    public class QuotesApiServiceIntegrationTests
    {
        [Fact]
        public async Task GetQuoteByCurrencyCodes_IntegrationTest()
        {
            // Arrange
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://economia.awesomeapi.com.br/json/last/")
            };

            var service = new QuotesApiService(httpClient);

            // Act
            var result = await service.GetQuoteByCurrencyCodes("USD", "EUR");

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual("1", result); // Verifica se a resposta não é o valor padrão
        }
    }
}
