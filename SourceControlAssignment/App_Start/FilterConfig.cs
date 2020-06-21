using SourceControlAssignment.Attributes;
using System.Web;
using System.Web.Mvc;

namespace SourceControlAssignment
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new BasicAuthAttribute());
            filters.Add(new OutputCacheAttribute()
            {
                VaryByParam = "*",
                Duration = 0,
                NoStore = true
            });
        }
    }
}
