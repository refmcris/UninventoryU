
using WebApplication1.Models.Equipment;

namespace Uninventory.Interfaces
{
  public interface ICategoryService
  {
    public Task<CategoryDTO> AddCategory(CategoryDTO add);
    public Task<IEnumerable<CategoryDTO>> GetCategories(int? CategoryId);
  }
}
