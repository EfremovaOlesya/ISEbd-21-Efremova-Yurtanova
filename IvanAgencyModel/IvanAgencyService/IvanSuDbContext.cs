using System;
using System.Data.Entity;
using IvanAgencyModel;
namespace IvanAgencyService
{   
    public class IvanSuDbContext : DbContext
    {
        public IvanSuDbContext() 
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;          
        }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Tour> Tours { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Travel> Travels { get; set; }

        public virtual DbSet<TravelTour> TravelTours { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw;
            }
        }
    }
}