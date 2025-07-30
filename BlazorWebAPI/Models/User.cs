using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Place of Birth is required.")]
        public string POB { get; set; }
        [Required(ErrorMessage = "Email-Id is reuired.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "User-Id is required.")]
        public string UserId { get; set; }
        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; set; }
        
        public byte[] myArray { get; set; }
    }
}
