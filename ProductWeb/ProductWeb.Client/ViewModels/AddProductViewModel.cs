using ProductWeb.Model.Models;

namespace ProductWeb.Client.ViewModels
{
    public class AddProductViewModel
    {
        public int Page { get; set; }
        public string Name { get; set; }
        public SelectedModel Selected { get; set; }

        public AddProductViewModel()
        {
            Selected = new SelectedModel();
        }
    }
}
