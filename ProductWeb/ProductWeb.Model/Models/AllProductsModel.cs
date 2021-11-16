using System.Collections.Generic;

namespace ProductWeb.Model.Models
{
    public class AllProductsModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
        public PageModel PageModel { get; set; }
        public SortModel SortModel { get; set; }
    }
}
