using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using System;

namespace ClickMart.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        public AppDbContext _db;
        public OrderHeaderRepository(AppDbContext db): base(db) 
        {
            this._db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
           this._db.OrderHeaders.Update(orderHeader);
        }

        public void UpdateShippingInformation(string Id, string Carrier, string TrakingNumber)
        {
            var orderHeader = _db.OrderHeaders.FirstOrDefault(x => x.Id == Id);
            if (orderHeader != null)
            {
                orderHeader.Carrier = Carrier;
                orderHeader.TrackingNumber = TrakingNumber;
            }
            
        }

        public void UpdateStatus(string Id, string OrderStatus, string? PaymentStatus = null)
        {
            var orderHeader = _db.OrderHeaders.FirstOrDefault(x => x.Id == Id);
            if(orderHeader != null)
            {
                orderHeader.OrderStatus = OrderStatus;
                if (!string.IsNullOrEmpty(PaymentStatus))
                {
                    orderHeader.PaymentStatus = PaymentStatus;
                }
            }
        }
        
        public void UpdateStripePaymentId(string Id, string sessionId, string paymentIntentId)
        {
            var orderHeader = _db.OrderHeaders.FirstOrDefault(x => x.Id == Id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderHeader.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderHeader.PaymentIntentId = paymentIntentId;
                orderHeader.PaymentDate = DateTime.Now;
            }
        }
    }
}
