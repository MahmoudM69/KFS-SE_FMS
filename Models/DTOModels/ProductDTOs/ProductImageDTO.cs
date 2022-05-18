namespace Models.DTOModels.ProductDTOs
{
    public class ProductImageDTO
    {
        public int ProductImageId { get; set; }
        public string ProductImageURL { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDTO ProductDTO { get; set; }
    }
}
