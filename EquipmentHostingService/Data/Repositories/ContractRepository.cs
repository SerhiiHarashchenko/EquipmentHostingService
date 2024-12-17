using EquipmentHostingService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipmentHostingService.Data.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly EquipmentHostingServiceDbContext _context;

        public ContractRepository(EquipmentHostingServiceDbContext context)
        {
            _context = context;
        }

        public async Task<EquipmentPlacementContract> CreateAsync(string facilityCode, string equipmentTypeCode, int equipmentQuantity)
        {
            var contract = new EquipmentPlacementContract
            {
                FacilityCode = facilityCode,
                EquipmentTypeCode = equipmentTypeCode,
                EquipmentQuantity = equipmentQuantity
            };

            _context.EquipmentPlacementContracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<IEnumerable<EquipmentPlacementContract>> GetAllContractsAsync()
        {
            return await _context.EquipmentPlacementContracts
                                 .Include(c => c.Facility)
                                 .Include(c => c.EquipmentType)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<EquipmentPlacementContract>> GetByFacilityCodeAsync(string facilityCode)
        {
            return await _context.EquipmentPlacementContracts
                .Include(c => c.EquipmentType)
                .Where(c => c.FacilityCode == facilityCode)
                .ToListAsync();
        }
    }
}