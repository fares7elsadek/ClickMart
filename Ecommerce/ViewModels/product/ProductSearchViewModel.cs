using ClickMart.Models.Models;

namespace ClickMart.ViewModels.product
{
    public class ProductSearchViewModel
    {
        public List<Product> products;
        public List<Category> categories;
        public int TotalProducts;
        public int NumberOfPages;
        public string SerachString;
        public int CurrentPage;
        public List<string> SelectedCategories;
    }
}
