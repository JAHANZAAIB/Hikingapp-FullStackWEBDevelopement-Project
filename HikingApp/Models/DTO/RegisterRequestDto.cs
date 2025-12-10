using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace HikingApp.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Invalid Email Adress")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]

        public string password { get; set; }
        [Required]
        public string[] Roles { get; set; }
    }
}
