namespace App.Domain.Models.Response
{
    public class GetBranchResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ManagerName { get; set; }
        public TimeSpan OpenningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
        public TimeSpan WorkingHours
        {
            get
            {
                return ClosingHour.Subtract(OpenningHour);
            }
            set
            {
            }
        }
    }
}
