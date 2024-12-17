using EquipmentHostingService.DTOs;

namespace EquipmentHostingService.Services
{
    public interface IContractService
    {
        Task<ContractDto> CreateContractAsync(CreateContractDto createContractDto);
        Task<IEnumerable<ContractDto>> GetAllContractsAsync();
    }
}
