using System.Collections.Generic;
using System.Threading.Tasks;

using ProductWeb.Model.Models;

namespace ProductWeb.Model.Interfaces
{
    public interface IProductService
    {
        List<ProductModel> GetAllProducts();
        Task<bool> TryAddProductAsync(SelectedModel selected, string name);
        Task DeleteProductAsync(int id);
        Task EditAsync(ProductModel editProduct, SelectedModel selected);
        Task<AllProductsModel> GetAllAsync(string productName, int categoryId, int page, SortState sortOrder);
        Task<ProductModel> GetProductAsync(int id);
    }
}
