using System.Collections.Generic;
using System.Threading.Tasks;

using ProductWeb.Model.Models;

using ProductWeb.Repository.Models;

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
        IEnumerable<ProductModel> Convert(IEnumerable<Product> products);
        IEnumerable<ProductModel> Sort(SortState sortOrder, IEnumerable<ProductModel> products);
    }
}
