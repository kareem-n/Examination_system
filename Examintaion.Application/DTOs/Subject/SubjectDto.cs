namespace Examination.Application.DTOs.Subject
{
    public record SubjectDto()
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
