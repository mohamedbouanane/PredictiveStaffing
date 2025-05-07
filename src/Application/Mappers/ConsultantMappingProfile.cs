namespace Application.Mappers;

using Application.Dtos;
using Domain.Entities;
using Domain.Enums;
using AutoMapper;

public sealed class ConsultantMappingProfile : Profile
{
    public ConsultantMappingProfile()
    {
        CreateMap<Consultant, ConsultantDto>()
            .ForMember(
                c => c.AvailabilityStatus, 
                opt => opt.MapFrom(src => src.Missions.Any(m => m.Status == MissionStatus.Active) ? "🔴" : "🟢")
            )
            .ForMember(
                c => c.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );
    }
}

