using System.ComponentModel.DataAnnotations;

namespace Arhitecture.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public EmailAddress Email { get; set; }

        [Required]
        public Password Password { get; set; }

        public User(EmailAddress email, Password password)
        {
            throw new NotImplementedException();
        }
    }
}
