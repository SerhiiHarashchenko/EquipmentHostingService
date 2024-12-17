using EquipmentHostingService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipmentHostingService.Data.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly EquipmentHostingServiceDbContext _context;

        public FacilityRepository(EquipmentHostingServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Facility?> GetFacilityByCodeAsync(string code)
        {
            return await _context.Facilities.FirstOrDefaultAsync(f => f.Code == code);
        }
    }
}