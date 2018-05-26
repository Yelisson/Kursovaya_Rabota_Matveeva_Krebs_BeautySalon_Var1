using BeautySalonService;
using BeautySalonService.ImplementationsBD;
using System.Data.Entity;
using Unity;

namespace BeautySalonWebView.DataSources
{
   public class ServiceDataSource:ServiceServiceBD
    {
        public ServiceDataSource() : base(UnityConfig.Container.Resolve<DbContext>() as AbstractDataBaseContext)
        {
        }
    }
}
