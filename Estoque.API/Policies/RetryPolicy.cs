using Polly.Extensions.Http;
using Polly;
using System.Net;

namespace Estoque.API.Policies
{
    public class RetryPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetGenericRetryPolicy(int retryCount)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                //Calcula a duração de espera entre as tentativas de forma exponencial
                // 2^1 = 2 segundos
                // 2^2 = 4 segundos
                // 2^3 = 8 segundos
                // .....
                .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
