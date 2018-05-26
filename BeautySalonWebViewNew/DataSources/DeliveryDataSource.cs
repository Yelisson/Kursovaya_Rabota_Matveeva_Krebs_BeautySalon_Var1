using BeautySalonService;
using BeautySalonService.ImplementationsBD;
using System.Data.Entity;
using Unity;

namespace BeautySalonWebView.DataSources
{
    public class DeliveryDataSource:DeliveryServiceBD
    {
        public DeliveryDataSource() : base(UnityConfig.Container.Resolve<DbContext>() as AbstractDataBaseContext)
        {
        }
    }
}
