using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EquipmentHostingService.Data.Entities
{
    public class EquipmentPlacementContract
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string FacilityCode { get; set; }

        [Required]
        public required string EquipmentTypeCode { get; set; }

        public int EquipmentQuantity { get; set; }

        [ForeignKey("FacilityCode")]
        public Facility Facility { get; set; }

        [ForeignKey("EquipmentTypeCode")]
        public EquipmentType EquipmentType { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
