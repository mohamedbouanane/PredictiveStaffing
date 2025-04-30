namespace Api.Mappers;

using Domain.Entities;
using AutoMapper;

// todo supprimer
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CreateMap<Consultant, ConsultantDto>()
        //    .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src =>
        //        src.Missions.Any(m => m.Status == MissionStatus.Active) ? "🔴" : "🟢")); // Symboles visuels

        //CreateMap<Mission, MissionDto>();
    }
}

