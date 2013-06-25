using System.Web;
using Infrastructure.Data;

namespace HomeBudget.Helpers
{
    public static class ContextFactory
    {
        public static HomeBudgetContext DbContext
        {
            get
            {
                return HttpContext.Current.Items["_HomeBudgetContext"] as HomeBudgetContext;
            }
            set
            {
                HttpContext.Current.Items["_HomeBudgetContext"] = value;
            }
        }
    }
}
