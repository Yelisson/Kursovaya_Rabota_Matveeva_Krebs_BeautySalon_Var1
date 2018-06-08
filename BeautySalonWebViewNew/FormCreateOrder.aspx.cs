using BeautySalonService.Interfaces;
using BeautySalonService.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BeautySalonService.ImplementationsBD;
using Unity;

namespace BeautySalonWebView
{
    public partial class FormCreateOrder : System.Web.UI.Page
    {
        public static List<ServiceBindingModel> listServices;
        private readonly IMainService serviceM = UnityConfig.Container.Resolve<IMainService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public List<ServiceBindingModel> getListOrderServices() {
            return listServices;
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormOrder.aspx");
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            listServices.Clear();
            Server.Transfer("FormMain.aspx");
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            serviceM.CreateOrder(new OrderBindingModel
            {
                clientId = MainServiceBD.iDClient,
                services = new List<ServiceBindingModel>(listServices)
               
            });
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Server.Transfer("FormMain.aspx");
        }
    }
}