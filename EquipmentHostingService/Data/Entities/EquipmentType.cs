using System.ComponentModel.DataAnnotations;

namespace EquipmentHostingService.Data.Entities
{
    public class EquipmentType
    {
        [Key]
        [MaxLength(100)]
        public required string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Area must be a positive integer.")]
        public int Area { get; set; }
    }
}
