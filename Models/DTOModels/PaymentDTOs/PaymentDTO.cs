using System.Collections.Generic;
using Models.DTOModels.OrderDTOs;

namespace Models.DTOModels.PaymentDTOs
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public int PaymentServiceDTOId { get; set; }
        public PaymentServiceDTO PaymentServiceDTO { get; set; }
        public ICollection<OrderDTO> OrderDTOs { get; set; }
    }
}
