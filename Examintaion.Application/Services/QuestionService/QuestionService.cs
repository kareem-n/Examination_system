using Examination.Application.DTOs.Question;
using Examination.Application.Interfaces.QuestionService;
using Examination.Application.Specifications;
using Examination.Domain.Common;
using Examination.Domain.Models;
using Template.Domain.Interfaces.Repostoreis;

namespace Examination.Application.Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly IGenericRepo<Question> questionRepo;

        public QuestionService(IGenericRepo<Question> questionRepo)
        {
            this.questionRepo = questionRepo;
        }


        public async Task<PageModel<QuestionDto>> GetAllQuestions(GetAllQuestionsParams @params)
        {
            var spec = new QuestionSpecification(@params);
            var count = await questionRepo.GetCountAsync(spec);
            var result = await questionRepo.GetAllAsync<QuestionDto>(spec);

            if (result is null)
            {
                throw new Exception("No questions found");
            }


            return new PageModel<QuestionDto>(result, count, @params.PageNumber, @params.PageSize);

        }

        public async Task<QuestionDto> CreateQuestion(CreateQuestionDto questionDto)
        {

            if (questionDto == null)
            {
                throw new ArgumentNullException(nameof(questionDto), "Question data cannot be null");
            }
            var question = new Question
            {
                QuestionText = questionDto.QuestionTitle,
                DifficultyLevel = questionDto.DifficultyLevel,
                SubjectId = questionDto.SubjectId,
                QuestionAnswers = questionDto.Choices.Select(c => new QuestionAnswer
                {
                    AnswerText = c.AnswerTxt,
                    IsCorrect = c.IsCorrect
                }).ToList()
            };

            var result = await questionRepo.AddAsync(question);

            if (result == null)
            {
                throw new Exception("Failed to create question");
            }

            // Assuming you have a method to map Question to QuestionDto
            var questionDtoResult = new QuestionDto
            {
                Id = result.Id.ToString(),
                QuestionTitle = result.QuestionText,
            };

            return questionDtoResult;

        }

        public async Task<IEnumerable<QuestionDto>> GetSubjectQuestions(Guid subjectId)
        {

            var spec = new QuestionSpecification(new GetAllQuestionsParams() { SubjectId = subjectId });

            var result = await questionRepo.GetAllAsync<QuestionDto>(spec);

            if (result is null || !result.Any())
            {
                throw new Exception("No questions found for this subject");
            }

            return result;


        }
    }
}
