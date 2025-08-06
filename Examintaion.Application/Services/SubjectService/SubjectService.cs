using AutoMapper;
using Examination.Application.DTOs.Subject;
using Examination.Application.Interfaces.SubjectService;
using Examination.Application.Specifications;
using Examination.Domain.Common;
using Examination.Domain.Models;
using Template.Domain.Interfaces.Repostoreis;

namespace Examination.Application.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private readonly IGenericRepo<Subject> _subjectRepo;
        private readonly IMapper _mapper;

        public SubjectService(IGenericRepo<Subject> subjectRepo, IMapper mapper)
        {
            _subjectRepo = subjectRepo;
            _mapper = mapper;
        }


        public async Task<SubjectDto> CreateSubject(CreateSubjectDto createSubjectDto)
        {
            if (createSubjectDto == null)
            {
                throw new ArgumentNullException(nameof(createSubjectDto), "Subject data cannot be null");
            }
            var subject = new Subject
            {
                Title = createSubjectDto.Title,
                Description = createSubjectDto.Description,
                SubjectConfiguration = new SubjectExamConfiguration
                {
                    Hard = (short)createSubjectDto.Hard,
                    Easy = (short)createSubjectDto.Easy,
                    Miduiem = (short)createSubjectDto.Normal,
                    NumberOsQuestions = (short)createSubjectDto.NumberOfQuestions,
                },
            };

            var result = await _subjectRepo.AddAsync(subject);

            if (result == null)
            {
                throw new Exception("Failed to create subject");
            }

            return _mapper.Map<SubjectDto>(result);

        }

        public async Task<PageModel<SubjectDto>> GetAllSubjects(GetAllSubjectsParams @params)
        {

            var spec = new SubjectSpecification(@params);
            var count = await _subjectRepo.GetCountAsync();
            var result = await _subjectRepo.GetAllAsync<SubjectDto>(spec);



            if (result == null)
            {
                return null!;
            }

            //if (!result.Any())
            //{
            //    throw new Exception("No subjects found");
            //}

            return new PageModel<SubjectDto>(result, count, @params.PageIndex, @params.PageSize);


            //return result.ToList().AsReadOnly();

        }

        public async Task<bool> DeleteSubject(Guid subjectId)
        {

            if (subjectId == Guid.Empty)
            {
                throw new ArgumentException("Subject ID cannot be empty", nameof(subjectId));
            }
            return await _subjectRepo.DeleteAsync(subjectId);

        }

        public async Task<SubjectDto> UpdateSubject(Guid subjectId, UpdateSubjectDto updateSubjectDto)
        {

            if (updateSubjectDto == null)
            {
                throw new ArgumentNullException(nameof(updateSubjectDto), "Subject data cannot be null");
            }

            var subject = await _subjectRepo.GetByIdAsync(subjectId, [x => x.SubjectConfiguration]);

            if (subject == null)
            {
                throw new Exception($"Subject with ID {subjectId} is not valid");
            }

            subject.Title = updateSubjectDto.Title ?? subject.Title;
            subject.Description = updateSubjectDto.Description ?? subject.Description;

            if (updateSubjectDto.Easy + updateSubjectDto.Hard + updateSubjectDto.Normal != 100)
            {
                throw new ArgumentException("The sum of Easy, Normal, and Hard must equal 100");
            }

            subject.SubjectConfiguration.Hard = (short)updateSubjectDto.Hard;
            subject.SubjectConfiguration.Easy = (short)updateSubjectDto.Easy;
            subject.SubjectConfiguration.Miduiem = (short)updateSubjectDto.Normal;
            subject.SubjectConfiguration.NumberOsQuestions = (short)updateSubjectDto.NumberOfQuestions;



            subject.UpdatedAt = DateTime.UtcNow.ToLocalTime();

            var result = await _subjectRepo.UpdateAsync(subject);
            if (result == null)
            {
                throw new Exception("Failed to update subject");
            }
            return _mapper.Map<SubjectDto>(result);


        }


    }
}
