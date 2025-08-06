namespace Examination.Application.DTOs.Question
{
    public class GetAllQuestionsParams
    {

        public Guid? SubjectId { get; set; }

        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

    }
}
