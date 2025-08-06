namespace Examination.Application.DTOs.Dashboard
{
    public class TotalsDto
    {
        public int TotalUsers { get; set; }
        public int TotalExams { get; set; }
        public int TotalSubjects { get; set; }

        public double AVGScore { get; set; }
        public double MaxScore { get; set; }
        public double MinScore { get; set; }

    }
}
