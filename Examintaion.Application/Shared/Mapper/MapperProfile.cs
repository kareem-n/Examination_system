using AutoMapper;
using Examination.Application.DTOs.Exam;
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


            CreateMap<Exam, UserExamDto>()
                .ForMember(dest => dest.SubjectName,
                opt => opt.MapFrom(src => src.Subject.Title))
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.StartTime,
                opt => opt.MapFrom(src => src.StartedAt))
                .ForMember(dest => dest.EndTime,
                opt => opt.MapFrom(src => src.SubmitedAt ?? src.ExpiresAt))
                .ReverseMap();





        }

    }
}
