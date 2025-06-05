using Microsoft.EntityFrameworkCore;
using Uninventory.Interfaces;
using WebApplication1.Models.Equipment;
using WebApplication1.Persistence.Models;
using WebApplication1.Persistence;

namespace Uninventory.Services
{
  public class CategoryService : ICategoryService
  {
    private readonly UninventoryDBContext _context;

    public CategoryService(UninventoryDBContext context)
    {
      _context = context;
    }
    public async Task<CategoryDTO> AddCategory(CategoryDTO add)
    {
      var category = new Categories
      {
        Name = add.Name
      };
      await _context.Categories.AddAsync(category);

      await _context.SaveChangesAsync();

      return new CategoryDTO
      {
        CategoryId = category.CategoryId,
        Name = category.Name
      };


    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories(int? CategoryId)
    {
      var query = _context.Categories.AsQueryable();


      var categories = await query.ToListAsync();
      return categories.Select(x => new CategoryDTO
      {
        CategoryId = x.CategoryId,
        Name = x.Name
      });
    }
  }
}
