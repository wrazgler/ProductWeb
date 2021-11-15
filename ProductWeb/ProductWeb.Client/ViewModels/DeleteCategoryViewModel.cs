using ProductWeb.Model.Models;

namespace ProductWeb.Client.ViewModels
{
    public class DeleteCategoryViewModel
    {
        public int Page { get; set; }
        public SelectedModel Selected { get; set; }

        public DeleteCategoryViewModel()
        {
            Selected = new SelectedModel();
        }
    }
}
