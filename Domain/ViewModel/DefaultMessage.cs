using System.Collections.Generic;

namespace Domain.ViewModel
{
    public partial class Errors
    {
        public List<string> ErrorList { get; set; }
    }

    public partial class GenericMessage
    {
        public Errors errors { get; set; }
#pragma warning disable IDE1006 // Naming Styles
        public string type { get; set; }
#pragma warning restore IDE1006 // Naming Styles
#pragma warning disable IDE1006 // Naming Styles
        public string title { get; set; }
#pragma warning restore IDE1006 // Naming Styles
#pragma warning disable IDE1006 // Naming Styles
        public int status { get; set; }
#pragma warning restore IDE1006 // Naming Styles
#pragma warning disable IDE1006 // Naming Styles
        public string traceId { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }

    public partial class DefaultMessage
    {
        public GenericMessage LoginError { get; set; }
        public GenericMessage DataNotAvailable { get; set; }
    }

}