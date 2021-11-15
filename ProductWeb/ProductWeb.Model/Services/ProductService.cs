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
        IBaseRepository Database { get; set; }

        public ProductService(IBaseRepository baseRepository)
        {
            Database = baseRepository;
        }

        public List<ProductModel> GetAllProducts()
        {
            var productsDTO = new List<ProductModel>();
            var products = Database.Products.GetAll();

            foreach (var product in products)
            {
                var categories = product.Categories
                    .Select(c => new CategoryModel { Id = c.Id, Name = c.Name })
                    .ToList();

                var productDTO = new ProductModel { Id = product.Id, Name = product.Name, Categories = categories};
                productsDTO.Add(productDTO);
            }

            return productsDTO;
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
            Database.Products.Create(product);

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

            if (product == null) return;

            product.Name = editProduct.Name;

            if (selected != null)
            {
                foreach (var item in selected.SelectedList)
                {
                    var select = await Database.Categories.GetByName(item.Category.Name);

                    if (item.IsChecked && !product.Categories.Contains(select))
                    {
                        product.Categories.Add(select);
                    }
                    if (!item.IsChecked && product.Categories.Contains(select))
                    {
                        product.Categories.Remove(select);
                    }
                }
            }

            await Database.Save();
        }

        public async Task<AllProductsModel> GetAllAsync(string productName, int categoryId, int page, 
            SortState sortOrder)
        {
            const int pageSize = 10;

            var products = new List<Product>();

            var currentCategory = await Database.Categories.GetById(categoryId);

            if (currentCategory != null && categoryId != 0)
            {
                products = currentCategory.Products;
            }
            else
            {
                products = Database.Products.GetAll().ToList();
            }

            var productsDTO = Convert(products);

            if (!string.IsNullOrEmpty(productName))
            {
                productsDTO = productsDTO
                    .Where(p => p.Name.ToLower()
                    .Contains(productName.ToLower()))
                    .ToList();
            }

            productsDTO = Sort(sortOrder, productsDTO).ToList();

            var count = productsDTO.Count();
            var items = productsDTO.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var allProductsDTO = new AllProductsModel
            {
                PageDTO = new PageModel(count, page, pageSize),
                SortDTO = new SortModel(sortOrder),
                Products = items
            };

            return allProductsDTO;
        }

        public async Task<ProductModel> GetProductAsync(int id)
        {
            var product = await Database.Products.GetById(id);
            var categories = product.Categories
                    .Select(c => new CategoryModel { Id = c.Id, Name = c.Name })
                    .ToList();

            var productDTO = new ProductModel 
            { 
                Id = product.Id, 
                Name = product.Name, 
                Categories = categories 
            };

            return productDTO;
        }

        public IEnumerable<ProductModel> Convert(IEnumerable<Product> products)
        {
            var productsDTO = new List<ProductModel>();

            foreach (var product in products)
            {
                var categories = product.Categories
                    .Select(c => new CategoryModel { Id = c.Id, Name = c.Name })
                    .ToList();

                var productDTO = new ProductModel 
                { 
                    Id = product.Id, 
                    Name = product.Name, 
                    Categories = categories 
                };
                productsDTO.Add(productDTO);
            }

            return productsDTO;
        }

        public IEnumerable<ProductModel> Sort(SortState sortOrder, IEnumerable<ProductModel> products)
        {
            if(sortOrder == SortState.ProductDesc)
                return products = products.OrderByDescending(p => p.Name);

            return products = products.OrderBy(p => p.Name);
        }
    }
}
