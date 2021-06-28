using System;

namespace Domain.Model
{
    // Core class that has common properties that every table have 
    public partial class Core
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}