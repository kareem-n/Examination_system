using Examination.Application.DTOs.Exam;
using Examination.Application.DTOs.ExamDto;
using Examination.Application.DTOs.QuestionAnswer;
using Examination.Application.Interfaces.ExamService;
using Examination.Application.Specifications;
using Examination.Domain.Enums;
using Examination.Domain.Interfaces.Repostoreis;
using Examination.Domain.Models;
using Template.Domain.Interfaces.Repostoreis;

namespace Examination.Application.Services.ExamService
{
    public class ExamService : IExamService
    {
        private readonly IGenericRepo<Exam> examRepo;
        private readonly IGenericRepo<Subject> subjectRepo;
        private readonly IQuestionRepo questionRepo;

        public ExamService(
            IGenericRepo<Exam> examRepo,
            IGenericRepo<Subject> subjectRepo,
            IQuestionRepo QuestionRepo
            )
        {
            this.examRepo = examRepo;
            this.subjectRepo = subjectRepo;
            questionRepo = QuestionRepo;
        }


        public async Task<ExamDto> GenerateStudentExam(Guid subjectId, string userId)
        {
            // Check if the subject exists
            var subject = await subjectRepo.GetByIdAsync(subjectId, [subject => subject.SubjectConfiguration]);
            if (subject == null)
            {
                throw new Exception("Invalid Subject ID");
            }

            // Check if the user has already taken the exam for this subject
            var existingExam = await examRepo.GetAllAsync<Exam>(new ExamSpecification(e => e.SubjectId == subjectId && e.StudentId == userId));
            if (existingExam.Any())
            {
                throw new Exception($"User {userId} have already taken the exam for this subject.");
            }

            // Generate exam questions based on the subject configuration
            var questions = await GenerateExamQuestions(subject);
            if (questions.Count() == 0)
            {
                throw new Exception("No questions available for this subject.");
            }

            // save the questions in the examquestions table 
            var exam = new Exam
            {
                SubjectId = subjectId,
                StudentId = userId,
                Questions = questions.ToList(),
                CreatedAt = DateTime.UtcNow,
                // Set exam start and expiration times based on subject configuration
                StartedAt = DateTime.UtcNow
            };
            exam.ExpiresAt = exam.StartedAt.AddMinutes(45);

            exam.Status = ExamStudentState.Pending;

            // Save the exam to the database
            await examRepo.AddAsync(exam);
            // Map the exam to ExamDto
            var examDto = new ExamDto
            {
                Id = exam.Id,
                Status = exam.Status.ToString(),
                DurationInMinutes = (int)(exam.ExpiresAt - exam.StartedAt).TotalMinutes,
                Questions = questions.Select(q => new ExamQuestionDto
                {
                    Id = q.Id,
                    QuestionTxt = q.QuestionText,
                    Options = q.QuestionAnswers.Select(s => new QuestionOptionDto
                    {
                        Id = s.Id,
                        AnswerTxt = s.AnswerText,
                    }).ToList(),
                }).ToList(),
                Title = subject.Title,

                StartTime = exam.StartedAt,
                EndTime = exam.ExpiresAt
            };
            // Return the exam DTO


            return examDto;
        }

        private async Task<IEnumerable<Question>> GenerateExamQuestions(Subject subject)
        {
            // Get the subject configuration  
            var config = subject.SubjectConfiguration;
            if (config == null)
            {
                throw new Exception("Subject configuration is not set.");
            }

            //var spec = new RandomQuestionsGenerations(config);

            // Get questions based on the configuration  
            var questions = await questionRepo.GetRandomQuestionsAsync(
                subject.Id,
                config.NumberOsQuestions,
                config.Easy,
                config.Miduiem,
                config.Hard
                );

            return questions;
        }


    }
}
