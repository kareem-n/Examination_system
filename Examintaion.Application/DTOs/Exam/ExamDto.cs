using Examination.Application.DTOs.Exam;

namespace Examination.Application.DTOs.ExamDto
{
    public class ExamDto
    {

        public Guid Id { get; set; }

        public string Status { get; set; }

        public string Title { get; set; }

        public int DurationInMinutes { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


        public List<ExamQuestionDto> Questions { get; set; } = [];


    }
}
