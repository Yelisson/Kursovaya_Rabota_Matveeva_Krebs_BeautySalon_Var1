
using BeautySalonModel;
using BeautySalonService.BindingModel;
using BeautySalonService.ImplementationsBD;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace BeautySalonWebView
{
    public partial class FormServices : System.Web.UI.Page
    {
        private readonly IServiceService service = UnityConfig.Container.Resolve<IServiceService>();
        private readonly IMainService serviceM = UnityConfig.Container.Resolve<IMainService>();
        private readonly IReportService serviceR = UnityConfig.Container.Resolve<IReportService>();

        List<ServiceViewModel> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                list = service.GetList();
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[3].Visible = false;
                Page.DataBind();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
            Server.Transfer("FormServices.aspx");
        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormMain.aspx");
        }

        protected void ButtonDownloadPrice_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormSaveServicePrice.aspx");
        }

        protected void ButtonMail_Click(object sender, EventArgs e)
        {
            // отправка на почту
            try
            {
                string strMail = serviceM.getClientMail(MainServiceBD.iDClient);
                service.SendEmail(strMail, "Оповещение по заказам",
                string.Format("Прайс изделий"));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + "Документ отправлен" + "');</script>");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");

            }


        }
    }
}