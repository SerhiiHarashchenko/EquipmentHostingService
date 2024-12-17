using AutoMapper;
using EquipmentHostingService.BackgroundProcessing;
using EquipmentHostingService.Data.Entities;
using EquipmentHostingService.Data.Repositories;
using EquipmentHostingService.DTOs;

namespace EquipmentHostingService.Services
{
    public class ContractsService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;
        private readonly IMapper _mapper;
        private readonly IMessageQueue _messageQueue; 

        public ContractsService(
            IContractRepository contractRepository,
            IFacilityRepository facilityRepository,
            IEquipmentTypeRepository equipmentTypeRepository,
            IMapper mapper,
            IMessageQueue messageQueue)               
        {
            _contractRepository = contractRepository;
            _facilityRepository = facilityRepository;
            _equipmentTypeRepository = equipmentTypeRepository;
            _mapper = mapper;
            _messageQueue = messageQueue;             
        }

        public async Task<ContractDto> CreateContractAsync(CreateContractDto createContractDto)
        {
            var facility = await ValidateFacilityAsync(createContractDto.FacilityCode);
            var equipmentType = await ValidateEquipmentTypeAsync(createContractDto.EquipmentTypeCode);

            ValidateAreaAvailability(facility, equipmentType, createContractDto.EquipmentQuantity);

            var contractEntity = _mapper.Map<EquipmentPlacementContract>(createContractDto);

            var savedContract = await _contractRepository.CreateAsync(
                contractEntity.FacilityCode,
                contractEntity.EquipmentTypeCode,
                contractEntity.EquipmentQuantity);

            _messageQueue.Enqueue($"Contract created: {createContractDto}"); 

            return _mapper.Map<ContractDto>(savedContract);
        }

        public async Task<IEnumerable<ContractDto>> GetAllContractsAsync()
        {
            var contracts = await _contractRepository.GetAllContractsAsync();
            return _mapper.Map<IEnumerable<ContractDto>>(contracts);
        }

        private async Task<Facility> ValidateFacilityAsync(string facilityCode)
        {
            var facility = await _facilityRepository.GetFacilityByCodeAsync(facilityCode);
            if (facility == null)
            {
                throw new KeyNotFoundException($"Facility with code '{facilityCode}' not found.");
            }
            if (facility.StandardArea <= 0)
            {
                throw new InvalidOperationException($"Facility '{facility.Name}' has an invalid StandardArea value: {facility.StandardArea}.");
            }
            return facility;
        }

        private async Task<EquipmentType> ValidateEquipmentTypeAsync(string equipmentTypeCode)
        {
            var equipmentType = await _equipmentTypeRepository.GetEquipmentTypeByCodeAsync(equipmentTypeCode);
            if (equipmentType == null)
            {
                throw new KeyNotFoundException($"Equipment type with code '{equipmentTypeCode}' not found.");
            }
            if (equipmentType.Area <= 0)
            {
                throw new InvalidOperationException($"Equipment type '{equipmentType.Name}' has an invalid Area value: {equipmentType.Area}.");
            }
            return equipmentType;
        }

        private void ValidateAreaAvailability(Facility facility, EquipmentType equipmentType, int equipmentQuantity)
        {
            var existingContracts = _contractRepository.GetByFacilityCodeAsync(facility.Code).Result;

            var usedArea = existingContracts.Sum(c => c.EquipmentQuantity * c.EquipmentType.Area);
            var freeArea = facility.StandardArea - usedArea;

            var requiredArea = equipmentQuantity * equipmentType.Area;

            if (requiredArea > freeArea)
            {
                throw new InvalidOperationException($"Insufficient space in facility '{facility.Name}'. " +
                    $"Required area: {requiredArea}, Available area: {freeArea}.");
            }
        }
    }
}