using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProductWeb.Model.Models;
using ProductWeb.Model.Interfaces;

using ProductWeb.Repository.Interfaces;
using ProductWeb.Repository.Models;

namespace ProductWeb.Model.Services
{
    public class ProductService : IProductService
    {
        private IBaseRepository Database { get; }

        public ProductService(IBaseRepository baseRepository)
        {
            Database = baseRepository;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            var productsModels = new List<ProductModel>();
            var products = await Database.Products.GetAllAsync();

            foreach (var product in products)
            {
                var categories = product.Categories
                    .Select(c => new CategoryModel { Id = c.Id, Name = c.Name })
                    .ToList();
                var productModel = new ProductModel { Id = product.Id, Name = product.Name, Categories = categories};
                productsModels.Add(productModel);
            }

            return productsModels;
        }

        public async Task<bool> TryAddProductAsync(SelectedModel selected, string name)
        {
            if (name == null)
                return false;

            var products = Database.Products.GetAll();

            foreach (var p in products)
            {
                if (p.Name.ToLower() == name.ToLower())
                    return false;
            }

            var product = new Product { Name = name };
            if (selected != null)
            {
                foreach (var item in selected.SelectedList)
                {
                    var category = await Database.Categories.GetByName(item.Category.Name);

                    if (item.IsChecked)
                    {
                        product.Categories.Add(category);
                    }
                }
            }
            await Database.Products.AddAsync(product);
            await Database.Save();

            return true;
        }

        public async Task DeleteProductAsync(int id)
        {
            await Database.Products.Delete(id);
            await Database.Save();
        }

        public async Task EditAsync(ProductModel editProduct, SelectedModel selected)
        {
            var product = await Database.Products.GetById(editProduct.Id);

            if (product == null) 
                return;

            product.Name = editProduct.Name;

            if (selected != null)
            {
                foreach (var item in selected.SelectedList)
                {
                    var select = await Database.Categories.GetByName(item.Category.Name);

                    switch (item.IsChecked)
                    {
                        case true when !product.Categories.Contains(@select):
                            product.Categories.Add(@select);
                            break;
                        case false when product.Categories.Contains(@select):
                            product.Categories.Remove(@select);
                            break;
                    }
                }
            }

            await Database.Save();
        }

        public async Task<AllProductsModel> GetAllAsync(string productName, int categoryId, int page, 
            SortState sortOrder)
        {
            List<Product> products;

            var currentCategory = await Database.Categories.GetById(categoryId);

            if (currentCategory != null && categoryId != 0)
            {
                products = currentCategory.Products;
            }
            else
            {
                products = Database.Products.GetAll().ToList();
            }

            var productsModel = Convert(products);

            if (!string.IsNullOrEmpty(productName))
            {
                productsModel = productsModel
                    .Where(p => p.Name.ToLower()
                    .Contains(productName.ToLower()))
                    .ToList();
            }

            productsModel = Sort(sortOrder, productsModel).ToList();

            var count = productsModel.Count();
            var items = productsModel.Skip((page - 1) * PageModel.GetPageSize()).Take(PageModel.GetPageSize()).ToList();

            var allProductsModel = new AllProductsModel
            {
                PageModel = new PageModel(count, page),
                SortModel = new SortModel(sortOrder),
                Products = items
            };

            return allProductsModel;
        }

        public async Task<ProductModel> GetProductAsync(int id)
        {
            var product = await Database.Products.GetById(id);
            var categories = product.Categories
                    .Select(c => new CategoryModel { Id = c.Id, Name = c.Name })
                    .ToList();

            var productModel = new ProductModel 
            { 
                Id = product.Id, 
                Name = product.Name, 
                Categories = categories 
            };

            return productModel;
        }

        public IEnumerable<ProductModel> Convert(IEnumerable<Product> products)
        {
            var productsModel = new List<ProductModel>();

            foreach (var product in products)
            {
                var categories = product.Categories
                    .Select(c => new CategoryModel { Id = c.Id, Name = c.Name })
                    .ToList();

                var productModel = new ProductModel 
                { 
                    Id = product.Id, 
                    Name = product.Name, 
                    Categories = categories 
                };
                productsModel.Add(productModel);
            }

            return productsModel;
        }

        public IEnumerable<ProductModel> Sort(SortState sortOrder, IEnumerable<ProductModel> products)
        {
            if(sortOrder == SortState.ProductDesc)
                return products = products.OrderByDescending(p => p.Name);

            return products = products.OrderBy(p => p.Name);
        }
    }
}
