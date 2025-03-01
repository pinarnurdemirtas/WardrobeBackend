using WardrobeBackend.Model;
using WardrobeBackend.Repositories;

namespace WardrobeBackend.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Categories>> GetCategoriesAsync(int? parentId = null)
    {
        return await _categoryRepository.GetCategoriesAsync(parentId);
    }
}
