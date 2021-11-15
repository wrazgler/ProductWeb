using System.Collections.Generic;

namespace ProductWeb.Model.Models
{
    public class SelectedModel
    {
        public List<SelectItem> SelectedList { get; set; } = new List<SelectItem>();

        public SelectedModel()
        {
            SelectedList = new List<SelectItem>();
        }
    }
}
