using ClickMart.Models.Models;

namespace ClickMart.ViewModels.product
{
    public class ProductDeatailsViewModel
    {
        public Product product { get; set; }
        public List<Product> SameCategoryProducts { get; set; }
    }
}
