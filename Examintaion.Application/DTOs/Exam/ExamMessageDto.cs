namespace Examination.Application.DTOs.Exam
{
    internal class ExamMessageDto
    {
        public string ExamId { get; set; } = null!;

        public ExamAnswers StudentAnswer { get; set; }

        public IEnumerable<string> CorrectAnswers { get; set; }
    }
}
