using ClickMart.Models.Models;

namespace ClickMart.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalCustomers { get; set; }
        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public List<User> Users { get; set; }

        public User User { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderHeader> OrderHeaders { get; set; }
    }
}
