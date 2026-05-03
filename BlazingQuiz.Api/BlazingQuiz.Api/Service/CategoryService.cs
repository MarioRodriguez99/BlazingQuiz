using BlazingQuiz.Api.Data;
using BlazingQuiz.Api.Data.Entities;
using BlazingQuiz.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazingQuiz.Api.Service
{
    public class CategoryService
    {
        private readonly QuizContext _context;

        public CategoryService(QuizContext context)
        {
            _context = context;
        }

        public async Task<QuizApiResponse> SaveCategoryAsync(CategoryDto dto)
        {
            if (await _context.Categories.AsNoTracking().AnyAsync(c => c.Name == dto.Name && c.Id != dto.Id))
            {
                return QuizApiResponse.Fail("Category with the same name already exists.");
            }

            if (dto.Id == 0)
            {
                var category = new Category
                {
                    Name = dto.Name
                };
                _context.Categories.Add(category);
            }
            else
            {
                var dbcategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == dto.Id);
                if (dbcategory == null)
                {
                    return QuizApiResponse.Fail("Category not found.");
                }
                dbcategory!.Name = dto.Name;
                _context.Categories.Update(dbcategory);
            }
            await _context.SaveChangesAsync();
            return QuizApiResponse.Success();
        }
        public async Task<CategoryDto[]> GetCategoriesAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToArrayAsync();
        }
    }
}
