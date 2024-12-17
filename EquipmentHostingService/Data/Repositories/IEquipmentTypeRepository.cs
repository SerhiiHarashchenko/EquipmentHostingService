using EquipmentHostingService.Data.Entities;

namespace EquipmentHostingService.Data.Repositories
{
    public interface IEquipmentTypeRepository
    {
        Task<EquipmentType?> GetEquipmentTypeByCodeAsync(string code);
    }
}