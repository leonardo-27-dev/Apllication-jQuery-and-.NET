using API_Nox.Enums;
using API_Nox.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Nox.Model
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public StatusTask Status { get; set; }
        public Guid? UserId { get; set; }
        public virtual UserViewModel? User { get; set; }

        public Task(string? Name, string? Description, StatusTask Status, Guid UserId)
        {
            this.Name = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Description = Description ?? throw new ArgumentNullException(nameof(Description));
            this.Status = Status;
            this.UserId = UserId;
        }
    }
}
