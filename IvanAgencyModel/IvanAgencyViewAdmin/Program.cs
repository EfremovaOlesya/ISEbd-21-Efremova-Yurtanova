using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IvanAgencyService;
using IvanAgencyService.ImplementationBD;
using IvanAgencyService.Interfaces;
using Unity;
using Unity.Lifetime;

namespace IvanAgencyViewAdmin
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
            Application.Run(container.Resolve<FormMainAdmin>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, IvanSuDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClient, ClientService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITour, TourService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAdmin, AdminService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITravel, TravelService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMain, MainService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReport, ReportService>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
