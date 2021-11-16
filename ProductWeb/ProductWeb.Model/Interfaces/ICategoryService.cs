using System.Collections.Generic;
using System.Threading.Tasks;

using ProductWeb.Model.Models;

namespace ProductWeb.Model.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryModel> GetAllCategories();
        Task<bool> TryAddCategoryAsync(string name);
        Task DeleteCategoryAsync(SelectedModel selected);
        SelectedModel CreateSelected();
        Task<SelectedModel> GetSelectedCategories(int productId);
    }
}