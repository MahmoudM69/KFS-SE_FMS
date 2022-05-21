using Models.DTOModels.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IPaymentRepositories
{
    public interface IPaymentRepository
    {
        public Task<PaymentDTO> CreatePayment(PaymentDTO PaymentDTO);
        public Task<PaymentDTO> GetPayment(int Id);
        public Task<ICollection<PaymentDTO>> GetAllPayments();
        public Task<PaymentDTO> UpdatePayment(int id, PaymentDTO PaymentDTO);
        public void DeletePayment(int Id);
    }
}
