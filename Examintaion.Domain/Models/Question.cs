using System.ComponentModel.DataAnnotations.Schema;
using Examination.Domain.Enums;
using Template.Domain.Common;

namespace Examination.Domain.Models
{
    public class Question : BaseEntity
    {
        public string QuestionText { get; set; } = null!;

        public DifficultyLevel DifficultyLevel { get; set; }

        public Guid SubjectId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;

        public ICollection<QuestionAnswer> QuestionAnswers { get; set; } = [];

        public List<Exam> Exams { get; set; }

    }
}
