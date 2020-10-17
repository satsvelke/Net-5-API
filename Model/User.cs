using System;
using Newtonsoft.Json;

namespace Model
{
    public partial class User : Core
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

    }
}