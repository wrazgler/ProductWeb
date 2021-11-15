namespace ProductWeb.Model.Models
{
    public class SortModel
    {
        public SortState ProductSort { get; }
        public SortState Current { get; }

        public SortModel(SortState sortOrder)
        {
            ProductSort = sortOrder == SortState.ProductAsc ? SortState.ProductDesc : SortState.ProductAsc;
            Current = sortOrder;
        }
    }
}
