namespace Examination.Application.DTOs.Exam
{
    public class UserExamDto
    {
        public string SubjectName { get; set; } = null!;

        public string Status { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
