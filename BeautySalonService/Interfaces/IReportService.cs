using BeautySalonService.BindingModel;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.Interfaces
{
    public interface IReportService
    {
        void SaveServicePrice(ReportBindingModel model);
        List<DeliverysLoadViewModel> GetDeliverysLoad();
        void SaveDeliverysLoad(ReportBindingModel model);
        List<ClientOrdersModel> GetClientOrders(ReportBindingModel model);
        void SaveClientOrders(ReportBindingModel model);

        List<DeliveryViewModel> GetAdminDeliverys(ReportBindingModel model);
        void SaveAdminDeliverys(ReportBindingModel model);
    }
}
