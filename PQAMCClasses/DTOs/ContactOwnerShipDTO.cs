namespace PQAMCClasses.DTOs
{
    public class ContactOwnerShipDTO
    {
        public int ContactOwnerShipId { get; set; }
        public string Name { get; set; }
        
        public List<AreaDTO> Areas { get; set; }
    }
}
