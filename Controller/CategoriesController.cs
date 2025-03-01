using Microsoft.AspNetCore.Mvc;
using WardrobeBackend.Data;
using WardrobeBackend.Model;
using WardrobeBackend.Repositories;
using WardrobeBackend.Services;


namespace WardrobeBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categories>>> GetCategories(int? parentId = null)
        {
            var categories = await _categoryService.GetCategoriesAsync(parentId);
            return Ok(categories);
        }
    }

}