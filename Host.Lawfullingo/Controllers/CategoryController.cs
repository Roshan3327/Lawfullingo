using ApplicationContract.Lawfullingo.Dto.CategoryDto;
using ApplicationContract.Lawfullingo.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Lawfullingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppService _categoryService;

        public CategoryController(ICategoryAppService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGetDto>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetDto>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            await _categoryService.AddAsync(dto);
            return Ok(new { message = "Category created successfully" });
        }

        // PUT: api/category
        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto dto)
        {
            await _categoryService.UpdateAsync(dto);
            return Ok(new { message = "Category updated successfully" });
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok(new { message = "Category deleted successfully" });
        }
    }
}
     