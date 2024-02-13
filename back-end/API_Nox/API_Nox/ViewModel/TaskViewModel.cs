using API_Nox.Enums;

namespace API_Nox.ViewModel
{
    public class TaskViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public StatusTask Status { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? Data { get; set; }
    }
}
