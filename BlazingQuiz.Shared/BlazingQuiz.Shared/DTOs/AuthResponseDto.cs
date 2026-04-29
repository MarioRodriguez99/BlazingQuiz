using System.Text.Json.Serialization;

namespace BlazingQuiz.Shared.DTOs
{
    public record AuthResponseDto(string? token, string? errorMessage = null)
    {
        [JsonIgnore]
        public bool HasError => errorMessage != null;
    }
}
