using System.Security.Claims;
using System.Text.Json;

namespace BlazingQuiz.Shared.DTOs
{
    public record LoggedUser(int Id, string Name, string Role, string Token)
    {
        public string ToJson() => JsonSerializer.Serialize(this);

        public Claim[] ToClaim() => 
            [
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Name, Name),
                new Claim(ClaimTypes.Role, Role.ToString()),
                new Claim(nameof(Token), Token)
            ];

        public static LoggedUser? LoadFromJson(string json) =>
            !string.IsNullOrWhiteSpace(json)
            ? JsonSerializer.Deserialize<LoggedUser>(json)
            : null;
                
    }
  
}
