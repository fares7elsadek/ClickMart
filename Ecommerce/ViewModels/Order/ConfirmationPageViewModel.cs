namespace ClickMart.ViewModels.Order
{
    public class ConfirmationPageViewModel
    {
        public string OrderNumber { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentMethod {  get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime PyamentDate { get; set; }
    }
}
