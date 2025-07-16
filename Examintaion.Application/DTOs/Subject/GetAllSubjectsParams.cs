namespace Examination.Application.DTOs.Subject
{
    public record GetAllSubjectsParams
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
