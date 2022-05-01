using DataAcesss.Data.ProductModels;
using DataAcesss.Data.EstablishmentModels;

namespace DataAcesss.Data.Shared
{
    public class Establishment_Product
    {
        public int Id { get; set; }
        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }
        public float Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
