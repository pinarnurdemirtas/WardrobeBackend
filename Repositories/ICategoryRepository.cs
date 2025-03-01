using WardrobeBackend.Model;

namespace WardrobeBackend.Repositories;

public interface ICategoryRepository
{
    Task<List<Categories>> GetCategoriesAsync(int? parentId = null);
}