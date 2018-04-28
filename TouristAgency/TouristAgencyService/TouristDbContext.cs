using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyModel;
using TouristAgencyService.Migrations;

namespace TouristAgencyService
{
    [Table("TouristDatabase")]
    public class TouristDbContext : DbContext
    {
        public TouristDbContext()
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Tour> Tours { get; set; }

        public virtual DbSet<Worker> Workers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Travel> Travels { get; set; }

        public virtual DbSet<TravelTour> TravelTours { get; set; }      
    }
}