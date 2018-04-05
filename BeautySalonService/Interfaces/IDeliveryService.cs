using BeautySalonService.BindingModels;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.Interfaces
{
   public interface IDeliveryService
    {
        List<DeliveryViewModel> GetList();
        DeliveryViewModel GetElement(int id);
        void AddElement(DeliveryBindingModel model);
        void UpdElement(DeliveryBindingModel model);
        void DelElement(int id);
    }
}
