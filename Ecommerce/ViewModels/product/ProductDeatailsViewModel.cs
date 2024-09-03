using ClickMart.Models.Models;

namespace ClickMart.ViewModels.product
{
    public class ProductDeatailsViewModel
    {
        public Product product { get; set; }
        public List<Product> SameCategoryProducts { get; set; }

        public List<Reviews> reviews { get; set; }

        public Cart cart { get; set; }  

    }
}
