using ClickMart.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository: IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);

        void UpdateStatus(string Id, string OrderStatus, string? PaymentStatus = null);

        void UpdateStripePaymentId(string Id, string sessionId, string paymentIntentId);

        void UpdateShippingInformation(string Id, string Carrier, string TrakingNumber);
        
    }
}
