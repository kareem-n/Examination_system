namespace EvalutaionService.cs.DTOs
{
    internal class ExamMessageRecieved
    {

        public string ExamId { get; set; } = null!;

        public ExamAnswers StudentAnswer { get; set; }

        public IEnumerable<string> CorrectAnswers { get; set; }

    }
}
