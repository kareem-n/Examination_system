using AutoMapper;
using Examination.Application.DTOs.Exam;
using Examination.Application.DTOs.ExamDto;
using Examination.Application.DTOs.QuestionAnswer;
using Examination.Application.Interfaces.ExamService;
using Examination.Application.Specifications;
using Examination.Domain.Common;
using Examination.Domain.Enums;
using Examination.Domain.Interfaces.Repostoreis;
using Examination.Domain.Models;
using Examintaion.Infrastructure.Repostories;
using Template.Domain.Interfaces.Repostoreis;

namespace Examination.Application.Services.ExamService
{
    public class ExamService : IExamService
    {
        private readonly IMapper mapper;
        private readonly IGenericRepo<Subject> subjectRepo;
        private readonly IQuestionRepo questionRepo;
        private readonly ExamRepo examRepo;
        private readonly IMessagePublisher messagePublisher;

        public ExamService(
            //IGenericRepo<Exam> examRepo,
            IMapper mapper,
            IGenericRepo<Subject> subjectRepo,
            IQuestionRepo QuestionRepo,
            ExamRepo examRepo, IMessagePublisher messagePublisher
            )
        {
            this.mapper = mapper;
            this.subjectRepo = subjectRepo;
            questionRepo = QuestionRepo;
            this.examRepo = examRepo;
            this.messagePublisher = messagePublisher;
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
            //var existingExam = await examRepo.GetExamsAsync<Exam>(new ExamSpecification(e => e.SubjectId == subjectId && e.StudentId == userId));
            var existingExam = await examRepo.GetExamsAsync(subjectId, userId);
            if (existingExam.Any())
            {
                var e = existingExam.First();

                if (e.ExpiresAt <= DateTime.Now)
                    throw new Exception("Already have the exam, and it has expired");

                return new ExamDto
                {
                    Id = e.Id,
                    Status = e.Status.ToString(),
                    DurationInMinutes = (int)(e.ExpiresAt - e.StartedAt).TotalMinutes,
                    Questions = e.Questions.Select(q => new ExamQuestionDto
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
                    StartTime = e.StartedAt,
                    EndTime = e.ExpiresAt
                };

                //throw new Exception($"User {userId} have already taken the exam for this subject.");
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
                CreatedAt = DateTime.Now,
                // Set exam start and expiration times based on subject configuration
                StartedAt = DateTime.Now
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

        public async Task<PageModel<UserExamDto>> GetUserExams(string userId, UserExamsHistoryParams userExamsHistoryParams)
        {

            var count = await examRepo.GetUserExamsCount(userId);
            var result = await examRepo.GetAllAsync<Exam>(new UserExamsSpecification(userId, userExamsHistoryParams));

            if (result == null || !result.Any())
            {
                throw new Exception("No exams found for the user.");
            }

            var x = mapper.Map<IEnumerable<UserExamDto>>(result);




            return new PageModel<UserExamDto>(x, count, userExamsHistoryParams.PageNumber, userExamsHistoryParams.PageSize);



        }

        public async Task<bool> SubmitExam(Guid examId, ExamAnswers examAnswers)
        {
            var existsExam = await examRepo.GetByIdAsync(examId);

            if (existsExam == null)
            {
                throw new Exception("Exam Not Found");
            }

            existsExam.ExamQuestionsAnswers = examAnswers.ExamQuestionsAnswers
                .Select(s => new ExamQuestionsAnswer
                {
                    ExamId = existsExam.Id,
                    QuestionId = Guid.Parse(s.QuestionId),
                    QuestionAnswerId = s.AnswerId != null ? Guid.Parse(s.AnswerId) : Guid.Empty,
                }).ToList();
            existsExam.SubmitedAt = DateTime.Now;
            existsExam.Status = ExamStudentState.Completed;
            var result = await examRepo.UpdateAsync(existsExam);

            if (result == null)
            {
                throw new Exception("Could not update");
            }

            var re = await examRepo.GetExamCorrectAnswersIDs(existsExam.Id.ToString());

            var examMessage = new ExamMessageDto
            {
                ExamId = existsExam.Id.ToString(),
                CorrectAnswers = re,
                StudentAnswer = examAnswers,
            };

            // Calculate the score based on the answers
            //var x = new { test = "first message " };
            await messagePublisher.PublishAsync(examMessage, "exam-submitted");

            return true;
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
