using System.Collections.Generic;

namespace ProductWeb.Model.Models
{
    public class AllProductsModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
        public PageModel PageDTO { get; set; }
        public SortModel SortDTO { get; set; }
    }
}
