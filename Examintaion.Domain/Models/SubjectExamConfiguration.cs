using System.ComponentModel.DataAnnotations.Schema;
using Template.Domain.Common;

namespace Examination.Domain.Models
{
    public class SubjectExamConfiguration : BaseEntity
    {

        [ForeignKey(nameof(SubjectId))]
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public short NumberOsQuestions { get; set; }

        public short Easy { get; set; }
        public short Miduiem { get; set; }
        public short Hard { get; set; }

    }
}
