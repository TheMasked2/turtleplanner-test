using System.ComponentModel.DataAnnotations;

namespace OfficePlanner.Api.Dtos
{

    public class LoginRequest
    {
        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = default!;

        [Required, MinLength(4), MaxLength(100)]
        public string Password { get; set; } = default!;
    }

    public record EmployeeResponse(int Id, string Email, string Name, string Role);
}
