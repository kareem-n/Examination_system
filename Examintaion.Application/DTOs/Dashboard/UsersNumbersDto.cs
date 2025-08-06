namespace Examination.Application.DTOs.Dashboard
{
    public class UsersNumbersDto
    {
        public int TotalUsers { get; set; }
        public List<UserPerMonth> Users { get; set; } = [];
    }
}
