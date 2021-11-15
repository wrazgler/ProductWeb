using ProductWeb.Model.Models;

namespace ProductWeb.Client.ViewModels
{
    public class EditProductViewModel
    {
        public int Page { get; set; }
        public ProductModel Product { get; set; }
        public SelectedModel Selected { get; set; }

        public EditProductViewModel()
        {
            Selected = new SelectedModel();
        }
    }
}
