using System.ComponentModel.DataAnnotations;

namespace Park.DTO
{
  public class LoginUserDTO
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  
  }
}