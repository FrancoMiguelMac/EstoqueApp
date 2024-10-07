using Polly.CircuitBreaker;
using Polly;
using System.Net;

namespace Estoque.API.Policies
{
    public class FallbackPolicy
    {
        public IAsyncPolicy<HttpResponseMessage> GetFallback()
        {
            return Policy<HttpResponseMessage>
                .Handle<BrokenCircuitException>()
                .FallbackAsync(FallbackAction, OnFallbackAsync);
        }

        private Task OnFallbackAsync(DelegateResult<HttpResponseMessage> response, Context context)
        {
            Console.WriteLine("Prestes a chamar a ação de fallback. Este é um bom lugar para fazer algum registro em log.");
            return Task.CompletedTask;
        }

        private Task<HttpResponseMessage> FallbackAction(DelegateResult<HttpResponseMessage> responseToFailedRequest, Context context, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage = new(HttpStatusCode.OK)
            {
                Content = new StringContent("") //Irá fazer a resposta da api de cotações retornar vazio e cairá no fator de conversão 1 que não impacta no valor da moeda.
            };
            return Task.FromResult(httpResponseMessage);
        }
    }
}
