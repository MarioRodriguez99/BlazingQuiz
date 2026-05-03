using System.Text.Json.Serialization;

namespace BlazingQuiz.Shared.DTOs
{
    public record AuthResponseDto(LoggedUser User, string? errorMessage = null)
    {
        [JsonIgnore]
        public bool HasError => errorMessage != null;
    }
}
