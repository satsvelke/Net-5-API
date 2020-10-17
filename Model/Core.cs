using System;

namespace Model
{
    public partial class Core
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}