using System.ComponentModel.DataAnnotations;

namespace Examination.Application.DTOs.Subject
{
    public record CreateSubjectDto
    {

        [Required(ErrorMessage = "Subject Title is required")]
        public string Title { get; set; } = null!;


        [Required(ErrorMessage = "Subject Description is required")]
        [MinLength(20, ErrorMessage = "Subject Min Length is 20")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Number of Questions is required")]
        [Range(10, 100)]
        public int NumberOfQuestions { get; set; }

        [Required]
        [Range(1, 100)]
        public int Easy { get; set; } = 0;
        [Required]
        [Range(1, 100)]
        public int Normal { get; set; } = 0;
        [Required]
        [Range(1, 100)]
        public int Hard { get; set; } = 0;


    }
}
