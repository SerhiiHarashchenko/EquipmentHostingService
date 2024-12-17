using EquipmentHostingService.Data.Entities;

namespace EquipmentHostingService.Data.Repositories
{
    public interface IFacilityRepository
    {
        Task<Facility?> GetFacilityByCodeAsync(string code);
    }
}