using BlazingQuiz.Api.Service;
using BlazingQuiz.Shared;
using BlazingQuiz.Shared.DTOs;

namespace BlazingQuiz.Api.Endpoints
{
    public static class CategoryEndpoits
    {
        public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
        {
            var categoryGroup = app.MapGroup("/api/categories").RequireAuthorization();

            categoryGroup.MapGet("", async(CategoryService categoryServices) =>
            Results.Ok(await categoryServices.GetCategoriesAsync()));

            categoryGroup.MapPost("", async(CategoryDto dto, CategoryService categoryServices) =>
            Results.Ok(await categoryServices.SaveCategoryAsync(dto)))
            .RequireAuthorization(p=> p.RequireRole(nameof(UserRole.Admin)));
           
            return app;
        }
    }
}
