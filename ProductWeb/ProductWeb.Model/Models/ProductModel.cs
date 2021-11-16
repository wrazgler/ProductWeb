using System.Collections.Generic;

namespace ProductWeb.Model.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public ProductModel()
        {
            Categories = new List<CategoryModel>();
        }
    }
}
