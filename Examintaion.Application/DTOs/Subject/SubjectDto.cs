namespace Examination.Application.DTOs.Subject
{
    public record SubjectDto()
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int TotalQuestions { get; set; }
        public int Easy { get; set; }
        public int Hard { get; set; }
        public int Meduim { get; set; }
    }
}
