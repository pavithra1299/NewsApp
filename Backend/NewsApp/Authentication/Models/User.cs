using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
