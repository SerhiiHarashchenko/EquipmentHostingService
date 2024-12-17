using EquipmentHostingService.Data.Entities;

namespace EquipmentHostingService.Data.Repositories
{
    public interface IContractRepository
    {
        Task<EquipmentPlacementContract> CreateAsync(string facilityCode, string equipmentTypeCode, int equipmentQuantity);
        Task<IEnumerable<EquipmentPlacementContract>> GetAllContractsAsync();
        Task<IEnumerable<EquipmentPlacementContract>> GetByFacilityCodeAsync(string facilityCode);
    }
}