using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using GSTInvoiceData;

namespace Gstinvoice
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<GSTInvoiceDBContext>(new DropCreateDatabaseIfModelChanges<GSTInvoiceDBContext>());
           
        }
    }
}
