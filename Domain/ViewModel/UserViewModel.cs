using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel
{
    public partial class UserViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}