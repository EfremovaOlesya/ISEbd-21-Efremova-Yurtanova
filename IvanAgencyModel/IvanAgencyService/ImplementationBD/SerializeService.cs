using IvanAgencyModel;
using IvanAgencyService.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
namespace IvanAgencyService.ImplementationBD
{
   public class SerializeService : ISerialize
    {
        private IvanSuDbContext context;

        public SerializeService(IvanSuDbContext context)
        {
            this.context = context;
        }

        public string GetData()
        {
            DataContractJsonSerializer clientJS = new DataContractJsonSerializer(typeof(List<Client>));
            MemoryStream msClient = new MemoryStream();
            clientJS.WriteObject(msClient, context.Clients.ToList());
            msClient.Position = 0;
            StreamReader srClient = new StreamReader(msClient);
            string clientsJSON = srClient.ReadToEnd();
            srClient.Close();
            msClient.Close();

            DataContractJsonSerializer adminJS = new DataContractJsonSerializer(typeof(List<Admin>));
            MemoryStream msAdmin = new MemoryStream();
            adminJS.WriteObject(msAdmin, context.Admins.ToList());
            msAdmin.Position = 0;
            StreamReader srAdmin = new StreamReader(msAdmin);
            string adminsJSON = srAdmin.ReadToEnd();
            srAdmin.Close();
            msAdmin.Close();

            DataContractJsonSerializer tourJS = new DataContractJsonSerializer(typeof(List<Tour>));
            MemoryStream msTour = new MemoryStream();
            tourJS.WriteObject(msTour, context.Tours.ToList());
            msTour.Position = 0;
            StreamReader srTour = new StreamReader(msTour);
            string toursJSON = srTour.ReadToEnd();
            srTour.Close();
            msTour.Close();

            DataContractJsonSerializer travelTourJS = new DataContractJsonSerializer(typeof(List<TravelTour>));
            MemoryStream msTravelTour = new MemoryStream();
            travelTourJS.WriteObject(msTravelTour, context.TravelTours.ToList());
            msTravelTour.Position = 0;
            StreamReader srTravelTour = new StreamReader(msTravelTour);
            string travelToursJSON = srTravelTour.ReadToEnd();
            srTravelTour.Close();
            msTravelTour.Close();

            DataContractJsonSerializer orderJS = new DataContractJsonSerializer(typeof(List<Order>));
            MemoryStream msOrder = new MemoryStream();
            orderJS.WriteObject(msOrder, context.Orders.ToList());
            msOrder.Position = 0;
            StreamReader srOrder = new StreamReader(msOrder);
            string ordersJSON = srOrder.ReadToEnd();
            srOrder.Close();
            msOrder.Close();
       
            DataContractJsonSerializer travelJS = new DataContractJsonSerializer(typeof(List<Travel>));
            MemoryStream msTravel = new MemoryStream();
            travelJS.WriteObject(msTravel, context.Travels.ToList());
            msTravel.Position = 0;
            StreamReader srTravel = new StreamReader(msTravel);
            string travelsJSON = srTravel.ReadToEnd();
            srTravel.Close();
            msTravel.Close();
           
            return
                "{\n" +
                "    \"Clients\": " + clientsJSON + ",\n" +
                 "    \"Admins\": " + adminsJSON + ",\n" +
                "    \"Tours\": " + toursJSON + ",\n" +
                "    \"TravelTours\": " + travelToursJSON + ",\n" +
                "    \"Orders\": " + ordersJSON + ",\n" +            
                "    \"Travels\": " + travelsJSON + ",\n" +   
                "}";
        }
    }
}