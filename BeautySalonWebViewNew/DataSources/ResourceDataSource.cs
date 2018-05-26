using BeautySalonService;
using BeautySalonService.ImplementationsBD;
using System.Data.Entity;
using Unity;

namespace BeautySalonWebView.DataSources
{
   public class ResourceDataSource:ResourceServiceBD
    {
        public ResourceDataSource() : base(UnityConfig.Container.Resolve<DbContext>() as AbstractDataBaseContext)
        {
        }
    }
}
