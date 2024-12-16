﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindIt.Shared.DTOs
{
    public class OrderRequestDTO
    {
        [Required(ErrorMessage = "OrderId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "OrderId must be a positive number.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "OrderItems are required.")]
        [MinLength(1, ErrorMessage = "At least one OrderItem is required.")]
        public List<OrderItemDTO> OrderItems { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public AddressRequestDTO Address { get; set; }

        [Required(ErrorMessage = "ShippingDate is required.")]
        [DataType(DataType.Date, ErrorMessage = "ShippingDate must be a valid date.")]
        public DateTime ShippingDate { get; set; }

        [Required(ErrorMessage = "ShippingFee is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "ShippingFee must be zero or a positive number.")]
        public double ShippingFee { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive number.")]
        public int UserId { get; set; }

        public double SubTotal { get; set; }//From Order item 
        public double Total { get; set; }// SubTotal+ShippingFee

    }
}