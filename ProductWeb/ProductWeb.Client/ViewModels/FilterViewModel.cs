using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

using ProductWeb.Model.Models;

namespace ProductWeb.Client.ViewModels
{
    public class FilterViewModel
    {
        public List<ProductModel> Products { get; }
        public SelectList Categories { get; }
        public string SelectedProduct { get; }
        public int SelectedCategory{ get; }

        public FilterViewModel(List<ProductModel> products, string product, List<CategoryModel> categories, int category)
        {
            Products = products;
            SelectedProduct = product;

            categories.Insert(0, new CategoryModel { Name = "Все", Id = 0 });
            Categories = new SelectList(categories, "Id", "Name", category);
            SelectedCategory = category;
        }
    }
}
