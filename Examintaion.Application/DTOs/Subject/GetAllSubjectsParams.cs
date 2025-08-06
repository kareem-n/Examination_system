namespace Examination.Application.DTOs.Subject
{
    public record GetAllSubjectsParams
    {
        public string? SearchTerm { get; set; }
        public string? sortby { get; set; }

        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
