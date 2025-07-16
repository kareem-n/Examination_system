using System.ComponentModel.DataAnnotations.Schema;
using Template.Domain.Common;

namespace Examination.Domain.Models
{
    public class Subject : BaseEntity
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ICollection<Question> Questions { get; set; } = [];

        public Guid ConfigId { get; set; }

        [ForeignKey(nameof(ConfigId))]

        public SubjectExamConfiguration SubjectConfiguration { get; set; }

        public ICollection<Exam> Exams { get; set; }


    }
}
