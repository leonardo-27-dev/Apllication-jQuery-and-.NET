
using API_Nox.Enums;
using System.ComponentModel.DataAnnotations;

namespace API_Nox.Model
{
    public class User
    {
        [Key]
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set;}
        public string Password { get; set;}

        public User(string? Name, string? Email, string Password)
        {
            this.Name = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Email = Email ?? throw new ArgumentNullException(nameof(Email));
            this.Password = Password;
        }
    }
}
