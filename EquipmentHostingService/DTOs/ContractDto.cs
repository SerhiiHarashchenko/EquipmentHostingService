namespace EquipmentHostingService.DTOs
{
    public class ContractDto
    {
        public int Id { get; set; }
        public required string FacilityName { get; set; }
        public required string EquipmentTypeName { get; set; }
        public int EquipmentQuantity { get; set; }
    }
}
