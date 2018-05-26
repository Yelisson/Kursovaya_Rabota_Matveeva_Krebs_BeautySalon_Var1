using BeautySalonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService
{
   public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Client> Clients { get; set; }
        public List<Resource> Resources { get; set; }
        public List<Order> Orders { get; set; }
        public List<Service> Services { get; set; }
        public List<DeliveryResource> DeliveryResources { get; set; }
        public List<Delivery> Deliverys { get; set; }
        public List<ServiceResource> ServiceResources { get; set; }

        private DataListSingleton()
        {
            Clients = new List<Client>();
            Resources = new List<Resource>();
            Orders = new List<Order>();
            Services = new List<Service>();
            DeliveryResources = new List<DeliveryResource>();
            Deliverys = new List<Delivery>();
            ServiceResources = new List<ServiceResource>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
