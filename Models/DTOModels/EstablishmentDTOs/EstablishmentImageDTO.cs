namespace Models.DTOModels.EstablishmentDTOs
{
    public class EstablishmentImageDTO
    {
        public int EstablishmentImageId { get; set; }
        public string EstablishmentImageURL { get; set; }
        public int EstablishmentId { get; set; }
        public EstablishmentDTO EstablishmentDTO { get; set; }
    }
}