using ClickMart.Models.Models;

namespace ClickMart.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        public User User { get; set; }
    }
}
