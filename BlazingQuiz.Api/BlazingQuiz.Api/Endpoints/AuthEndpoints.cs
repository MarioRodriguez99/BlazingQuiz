using BlazingQuiz.Api.Service;
using BlazingQuiz.Shared.DTOs;

namespace BlazingQuiz.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        { 
            app.MapPost("/api/auth/login",async(LoginDto login, AuthService service) => Results.Ok(await service.LoginAsync(login)));
            return app;
        }
    }
}
