using BeautySalonService.ImplementationsBD;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace BeautySalonWebView
{
    public partial class FormMain : System.Web.UI.Page
    {
        private IMainService service = UnityConfig.Container.Resolve<IMainService>();

        List<OrderViewModel> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                list = service.GetListForClient(MainServiceBD.iDClient);
                dataGridView1.Columns[0].Visible = false;
                Page.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCreateIndent_Click(object sender, EventArgs e)
        {
            FormCreateOrder.listServices = new List<BeautySalonService.BindingModels.ServiceBindingModel>();
            Server.Transfer("FormCreateOrder.aspx");
        }

        protected void ButtonShowServices_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormServices.aspx");
        }

        protected void ButtonTakeIndentInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                string index = list[dataGridView1.SelectedIndex].id.ToString();
                Session["id"] = index;
                Server.Transfer("TakeOrderInWork.aspx");
            }
        }

        protected void ButtonIndentReady_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                int id = list[dataGridView1.SelectedIndex].id;
                try
                {
                    service.FinishOrder(id);
                    LoadData();
                    Server.Transfer("FormMain.aspx");
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonIndentPayed_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                int id = list[dataGridView1.SelectedIndex].id;
                try
                {
                    service.PayOrder(id);
                    LoadData();
                    Server.Transfer("FormMain.aspx");
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
            Server.Transfer("FormMain.aspx");
        }

        protected void ButtonDownloadOrders_Click(object sender, EventArgs e)
        {
            // отчет по заказам
            Server.Transfer("FormReportPdf.aspx");

        }
    }
}