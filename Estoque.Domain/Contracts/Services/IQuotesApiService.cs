namespace Estoque.Domain.Contracts.Services
{
    public interface IQuotesApiService
    {
        Task<string> GetQuoteByCurrencyCodes(string currencyCode, string anotherCurrencyCode);
    }
}
