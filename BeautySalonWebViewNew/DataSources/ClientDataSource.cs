using BeautySalonService;
using BeautySalonService.ImplementationsBD;
using System.Data.Entity;
using Unity;

namespace BeautySalonWebView.DataSources
{
   public class ClientDataSource:ClientServiceBD
    {
        public ClientDataSource() : base(UnityConfig.Container.Resolve<DbContext>() as AbstractDataBaseContext)
        {
        }
    }
}
