using BeautySalonService;
using BeautySalonService.ImplementationsBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;

namespace BeautySalonWebView.DataSources
{
    public class ReportDataSource: ReportServiceBD
    {
        public ReportDataSource() : base(UnityConfig.Container.Resolve<DbContext>() as AbstractDataBaseContext)
        {
        }
    }
}