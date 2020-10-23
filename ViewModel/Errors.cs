using System.Collections.Generic;

namespace ViewModel
{
    public partial class Errors
    {
        public List<string> ErrorList { get; set; }
    }

    public partial class ErrorMessage
    {
        public Errors errors { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
    }

    public partial class DefaultMessage
    {
        public ErrorMessage LoginError { get; set; }
        public ErrorMessage DataNotAvailable { get; set; }
    }

}