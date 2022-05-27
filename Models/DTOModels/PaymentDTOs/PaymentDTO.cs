using System;
using System.Collections.Generic;
using Models.DTOModels.OrderDTOs;

namespace Models.DTOModels.PaymentDTOs
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public int PaymentServiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public PaymentServiceDTO PaymentServiceDTO { get; set; }
        public ICollection<OrderDTO> OrderDTOs { get; set; }
    }
}
