using AutoMapper;
using EquipmentHostingService.Data.Entities;
using EquipmentHostingService.DTOs;

namespace EquipmentHostingService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EquipmentPlacementContract, ContractDto>()
                .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Facility.Name))
                .ForMember(dest => dest.EquipmentTypeName, opt => opt.MapFrom(src => src.EquipmentType.Name));

            CreateMap<CreateContractDto, EquipmentPlacementContract>();
        }
    }
}