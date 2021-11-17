using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ProductWeb.Client.ViewModels;

using ProductWeb.Model.Interfaces;
using ProductWeb.Model.Models;

namespace ProductWeb.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public HomeController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(string productName, int categoryId, int page = 1,
            SortState sortOrder = SortState.ProductAsc)
        {
            var products = await _productService.GetAllProductsAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();
            var model = new GetAllProductsViewModel
            { 
                FilterViewModel = new FilterViewModel(products, productName, categories, categoryId),
                AllProductsModel = await _productService
                    .GetAllAsync(productName, categoryId, page, sortOrder)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct(int page = 1)
        {
            var selected = await _categoryService.CreateSelectedAsync();
            var model = new AddProductViewModel() 
            { 
                Page = page, 
                Selected = selected 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _productService.TryAddProductAsync(model.Selected, model.Name);

            return RedirectToAction("GetAllProducts", "Home", new { page = model.Page });
        }

        [HttpGet]
        public async Task<IActionResult> AddCategory(string previousPage, int id = 1, int page = 1)
        {
            var model = await Task.Run(() => new AddCategoryViewModel()
            {
                Id = id,
                Page = page,
                PreviousPage = previousPage
            });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _categoryService.TryAddCategoryAsync(model.Name);

            return RedirectToAction($"{model.PreviousPage}", "Home", new { id = model.Id, page = model.Page });

        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id, int page = 1)
        {
            var product = await _productService.GetProductAsync(id);
            var model = new DeleteProductViewModel { Product = product, Page = page };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(DeleteProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _productService.DeleteProductAsync(model.Product.Id);

            return RedirectToAction("GetAllProducts", "Home", new { page = model.Page });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int page = 1)
        {
            var selected = await _categoryService.CreateSelectedAsync();
            var model = new DeleteCategoryViewModel() { Page = page, Selected = selected };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(DeleteCategoryViewModel model)
        {
            await _categoryService.DeleteCategoryAsync(model.Selected);

            return RedirectToAction("GetAllProducts", "Home", new { page = model.Page });
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id, int page = 1)
        {
            var model = new EditProductViewModel() 
            { 
                Page = page,
                Product = await _productService.GetProductAsync(id),
                Selected = await _categoryService.GetSelectedCategories(id)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _productService.EditAsync(model.Product, model.Selected);

            return RedirectToAction("GetAllProducts", "Home", new { page = model.Page });
        }
    }
}