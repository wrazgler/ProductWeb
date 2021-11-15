using ProductWeb.Model.Models;

namespace ProductWeb.Client.ViewModels
{
    public class GetAllProductsViewModel
    {
        public AllProductsModel AllProductsDTO { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
