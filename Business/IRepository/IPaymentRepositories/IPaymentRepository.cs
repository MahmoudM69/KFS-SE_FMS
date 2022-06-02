using DataAcesss.Data.PaymentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IPaymentRepositories
{
    public interface IPaymentRepository
    {
        public Payment CreatePayment(Payment Payment);
        public Task<Payment> GetPayment(int Id);
        public Task<List<Payment>> GetAllCustomerPayments(string id);
        public Task<List<Payment>> GetAllPayments();
        public Task<Payment> UpdatePayment(Payment Payment);
        public void DeletePayment(int Id);
    }
}
