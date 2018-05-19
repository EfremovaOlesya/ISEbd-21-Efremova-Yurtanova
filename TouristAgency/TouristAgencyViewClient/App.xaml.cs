using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IvanAgencyService;
using IvanAgencyService.ImplementationBD;
using IvanAgencyService.Interfaces;
using Unity;
using Unity.Lifetime;

namespace IvanAgencyViewClient
{
    public partial class App : Application
    {
        /*App()
                   {
                       InitializeComponent();
                   }*/

        [STAThread]
        public static void Main()
        {
            var container = BuildUnityContainer();

            var application = new App();
            //application.InitializeComponent();
            application.Run(container.Resolve<FormGlav>());
            //App app = new App();
            //app.Run(container.Resolve<FormMain>());
          
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