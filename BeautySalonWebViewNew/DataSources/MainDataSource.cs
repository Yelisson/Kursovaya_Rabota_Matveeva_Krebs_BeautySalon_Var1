using BeautySalonService;
using BeautySalonService.ImplementationsBD;
using System.Data.Entity;
using Unity;

namespace BeautySalonWebView.DataSources
{
    public class MainDataSource : MainServiceBD
    {
        public MainDataSource() : base(UnityConfig.Container.Resolve<DbContext>() as AbstractDataBaseContext) {
        }
    }
}
