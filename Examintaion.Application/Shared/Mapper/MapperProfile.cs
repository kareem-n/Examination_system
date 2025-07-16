using AutoMapper;
using Examination.Application.DTOs.Subject;
using Examination.Domain.Models;

namespace Template.Application.Shared.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Subject, SubjectDto>()
                .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description)) // FullName → Name
                .ReverseMap();

            CreateMap<Subject, UpdateSubjectDto>()
                .ReverseMap();


        }

    }
}
