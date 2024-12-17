using EquipmentHostingService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipmentHostingService.Data.Repositories
{
    public class EquipmentTypeRepository : IEquipmentTypeRepository
    {
        private readonly EquipmentHostingServiceDbContext _context;

        public EquipmentTypeRepository(EquipmentHostingServiceDbContext context)
        {
            _context = context;
        }

        public async Task<EquipmentType?> GetEquipmentTypeByCodeAsync(string code)
        {
            return await _context.EquipmentTypes.FirstOrDefaultAsync(e => e.Code == code);
        }
    }
}