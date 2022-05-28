using DataAcesss.Data.PaymentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IPaymentRepositories
{
    public interface IPaymentServiceRepository
    {
        public Task<PaymentService> CreatePaymentService(PaymentService paymentService);
        public Task<PaymentService> GetPaymentService(int Id);
        public List<PaymentService> GetAllPaymentServices();
        public Task<PaymentService> UpdatePaymentService(PaymentService paymentService);
        public void DeletePaymentService(int Id);
    }
}
