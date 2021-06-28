using Domain.ViewModel;
using System.Collections.Generic;

namespace Domain.ViewModel
{
    public class Response
    {
        public List<GenericMessage> Statuses { get; set; }
        public dynamic Transaction { get; set; }
    }
}
