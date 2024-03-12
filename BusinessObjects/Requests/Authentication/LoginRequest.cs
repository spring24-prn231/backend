using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Requests.Authentication
{
    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
