using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProductWeb.Model.Models;
using ProductWeb.Model.Interfaces;

using ProductWeb.Repository.Interfaces;
using ProductWeb.Repository.Models;

namespace ProductWeb.Model.Services
{
    public class CategoryService : ICategoryService
    {
        IBaseRepository Database { get; set; }

        public CategoryService(IBaseRepository baseRepository) 
        {
            Database = baseRepository;
        }

        public List<CategoryModel> GetAllCategories()
        {
            return Database.Categories.GetAll()
                .Select(c => new CategoryModel { Id = c.Id, Name = c.Name })
                .ToList();
        }

        public async Task<bool> TryAddCategoryAsync(string name)
        {
            if (name == null)
                return false;

            var categories = Database.Categories.GetAll();

            foreach (var c in categories)
            {
                if (c.Name.ToLower() == name.ToLower())
                    return false;
            }

            var category = new Category { Name = name };
            Database.Categories.Create(category);

            await Database.Save();

            return true;
        }

        public async Task DeleteCategoryAsync(SelectedModel selected)
        {
            if (selected != null)
            {
                foreach (var item in selected.SelectedList)
                {
                    if (item.IsChecked)
                    {
                        await Database.Categories.Delete(item.Category.Id);
                    }
                }
            }
            await Database.Save();
        }

        public SelectedModel CreateSelected()
        {
            var categories = Database.Categories.GetAll();

            var selected = new SelectedModel() 
            { 
                SelectedList = categories
                .OrderBy(c => c.Name)
                .Select(c => new SelectItem() { Category = c })
                .ToList()
            };
            return selected;
        }

        public async Task<SelectedModel> GetSelctedCategories(int productId)
        {
            var product = await Database.Products.GetById(productId);

            var categories = Database.Categories.GetAll();

            var selected = new SelectedModel()
            {
                SelectedList = categories
                .OrderBy(c => c.Name)
                .Select(c => new SelectItem() { Category = c })
                .ToList()
            };

            foreach (var item in selected.SelectedList)
            {
                var select = categories.FirstOrDefault(c => c.Name == item.Category.Name);

                if (product.Categories.Contains(select))
                {
                    item.IsChecked = true;
                }
                else
                {
                    item.IsChecked = false;
                }
            }

            return selected;
        }
    }
}
