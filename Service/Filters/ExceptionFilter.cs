using Microsoft.AspNetCore.Mvc.Filters;


/// <summary>
/// deprecated, replaced by custom exception middleware 
/// </summary>
namespace Service.Filters
{
    public partial class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
        }
    }
}