using BeautySalonService.ImplementationsList;
using BeautySalonService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace BeautySalonView
{
   public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
       public static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMainAdministrator>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IClientService, ClientServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IResourceService, ResourceServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceService, ServiceServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDeliveryService, DeliveryServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
