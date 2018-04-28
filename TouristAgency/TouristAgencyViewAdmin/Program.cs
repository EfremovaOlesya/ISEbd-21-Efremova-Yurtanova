using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TouristAgencyService;
using TouristAgencyService.ImplementationsList;
using TouristAgencyService.Interfaces;
using TouristAgencyView;
using Unity;
using Unity.Lifetime;

namespace TouristAgencyViewAdmin
{

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, TouristDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientService, ClientService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITourService, TourService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkerService, WorkerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITravelService, TravelService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainService>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
