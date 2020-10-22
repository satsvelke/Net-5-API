using System.Collections.Generic;

namespace ViewModel
{
    public partial class Errors
    {
        public List<string> ErrorList { get; set; }
    }

    public partial class ErrorMessages
    {
        public Errors errors { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
    }

}