using Microsoft.EntityFrameworkCore;
using WardrobeBackend.Data;
using WardrobeBackend.Model;
using WardrobeBackend.Repositories;


namespace quizify.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WardrobeDbContext _context;

        public CategoryRepository(WardrobeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categories>> GetCategoriesAsync(int? parentId = null)
        {
            IQueryable<Categories> categoriesQuery = _context.Category;
            if (parentId.HasValue)
            {
                categoriesQuery = categoriesQuery.Where(c => c.ParentId == parentId);
            }
            var categories = await categoriesQuery
                .Select(c => new Categories
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentId = c.ParentId
                })
                .ToListAsync();
            return categories;
        }
    }
}