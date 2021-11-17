using System.Collections.Generic;
using System.Threading.Tasks;

using ProductWeb.Model.Models;

namespace ProductWeb.Model.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetAllCategoriesAsync();
        Task<bool> TryAddCategoryAsync(string name);
        Task DeleteCategoryAsync(SelectedModel selected);
        Task<SelectedModel> CreateSelectedAsync();
        Task<SelectedModel> GetSelectedCategories(int productId);
    }
}