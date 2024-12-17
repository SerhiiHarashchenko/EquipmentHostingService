using System.ComponentModel.DataAnnotations;

namespace EquipmentHostingService.DTOs
{
    public class CreateContractDto
    {
        [Required(ErrorMessage = "FacilityCode is required.")]
        [MaxLength(100, ErrorMessage = "FacilityCode must not exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "FacilityCode must be alphanumeric.")]
        public required string FacilityCode { get; set; }

        [Required(ErrorMessage = "EquipmentTypeCode is required.")]
        [MaxLength(100, ErrorMessage = "EquipmentTypeCode must not exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "EquipmentTypeCode must be alphanumeric.")]
        public required string EquipmentTypeCode { get; set; }

        [Range(1, 10000, ErrorMessage = "EquipmentQuantity must be between 1 and 10000.")]
        public int EquipmentQuantity { get; set; }
    }
}
