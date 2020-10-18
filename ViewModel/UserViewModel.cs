using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ViewModel
{
    public partial class UserViewModel
    {

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}