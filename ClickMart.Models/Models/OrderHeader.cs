﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class OrderHeader
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShippingDate { get; set; }

        public double OrderTotal { get; set; }

        public string? OrderStatus { get; set; }

        public string? PaymentStatus { get; set; }

        public string? TrackingNumber { get; set; }

        public string? Carrier {  get; set; }

        public DateTime PaymentDate { get; set; }

        public DateOnly PaymentDueDate { get; set; }

        public string? PaymentIntentId { get; set; }

        public string ShippingMethodId { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        public string AddressId { get; set; }

        public string? CouponId { get; set; }
        public Coupons? Coupons { get; set; }

        public Address Address { get; set; }

        public string OrderDetailsId { get; set; }

        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
