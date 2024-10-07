using System.Text.Json.Serialization;

namespace Estoque.Crosscutting.Dtos
{
    public record QuoteServiceResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("codein")]
        public string CodeIn { get; set; } = string.Empty;

        [JsonPropertyName("ask")]
        public string Conversion { get; set; } = string.Empty;
    }
}
