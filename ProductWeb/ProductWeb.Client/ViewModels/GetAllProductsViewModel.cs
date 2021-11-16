using ProductWeb.Model.Models;

namespace ProductWeb.Client.ViewModels
{
    public class GetAllProductsViewModel
    {
        public AllProductsModel AllProductsModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
