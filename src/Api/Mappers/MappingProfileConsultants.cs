namespace Api.Mappers;

using Application.Models;
using AutoMapper;
using Api.Dtos;

public class MappingProfileConsultants : Profile
{
    public MappingProfileConsultants()
    {
        CreateMap<ConsultantModel, ConsultantDto>();
    }
}
