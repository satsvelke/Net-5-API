using System.Collections.Generic;

namespace Domain.ViewModel
{
    public partial class Errors
    {
        public List<string> ErrorList { get; set; }
    }

    public partial class GenericMessage
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
        public string message { get; set; }
    }

    public partial class DefaultMessage
    {
        public GenericMessage LoginError { get; set; }
        public GenericMessage DataNotAvailable { get; set; }
        public GenericMessage DefaultSuccess { get; set; }

    }

}