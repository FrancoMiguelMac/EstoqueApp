using Estoque.Infra.ExternalServices;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace Estoque.Infra.Tests
{
    public class QuotesApiServiceUnitTests
    {
        [Fact]
        public async Task GetQuoteByCurrencyCodes_ValidResponse_ReturnsExpectedConversion()
        {
            // Arrange
            var currencyCode = "USD";
            var anotherCurrencyCode = "EUR";
            var expectedResponseContent = "{\"USDEUR\":{\"ask\":\"0.85\"}}";

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedResponseContent),
                })
                .Verifiable();

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/")
            };
            var service = new QuotesApiService(httpClient);

            // Act
            var result = await service.GetQuoteByCurrencyCodes(currencyCode, anotherCurrencyCode);

            // Assert
            Assert.Equal("0.85", result);

            // Verify that SendAsync was called as expected
            httpMessageHandlerMock.Verify();
        }

        [Fact]
        public async Task GetQuoteByCurrencyCodes_EmptyResponse_ReturnsDefault()
        {
            // Arrange
            var currencyCode = "USD";
            var anotherCurrencyCode = "EUR";
            var expectedResponseContent = "";

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedResponseContent),
                })
                .Verifiable();

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/")
            };
            var service = new QuotesApiService(httpClient);

            // Act
            var result = await service.GetQuoteByCurrencyCodes(currencyCode, anotherCurrencyCode);

            // Assert
            Assert.Equal("1", result);

            // Verify that SendAsync was called as expected
            httpMessageHandlerMock.Verify();
        }
    }
}
