using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.UI;
using Unity;

namespace BeautySalonWebView
{
    public partial class FormOrder : System.Web.UI.Page
    {
 
        private readonly IClientService serviceC = UnityConfig.Container.Resolve<IClientService>();

        private readonly IServiceService serviceS = UnityConfig.Container.Resolve<IServiceService>();

        private readonly IMainService serviceM = UnityConfig.Container.Resolve<IMainService>();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!Page.IsPostBack)
                {
                   
                    List<ServiceViewModel> listP = serviceS.GetList();
                    if (listP != null)
                    {
                        DropDownListService.DataSource = listP;
                        DropDownListService.DataBind();
                        DropDownListService.DataTextField = "serviceName";
                        DropDownListService.DataValueField = "id";
                    }
                    Page.DataBind();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void CalcSum()
        {
            
            if (DropDownListService.SelectedValue != null && !string.IsNullOrEmpty(TextBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(DropDownListService.SelectedValue);
                    ServiceViewModel product = serviceS.GetElement(id);
                    int count = Convert.ToInt32(TextBoxCount.Text);
                    TextBoxSum.Text = (count * product.price).ToString();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void DropDownListService_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        protected void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            if (DropDownListService.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите услугу');</script>");
                return;
            }
            try
            {
                FormCreateOrder.listServices.Add(new ServiceBindingModel {

                    //serviceName=DropDownListService.SelectedValue,//так изначально работало, потом сломалось непонятно из-за чего

                    //serviceName= DropDownListService.SelectedItem.ToString(),//так должно быть, но всё равно не работает

                    id = serviceS.GetElement(Convert.ToInt32(DropDownListService.SelectedValue)).id,
                    price = serviceS.GetElement(Convert.ToInt32(DropDownListService.SelectedValue)).price,
                    count = Convert.ToInt32(TextBoxCount.Text),
                    serviceName = serviceS.GetElement(Convert.ToInt32(DropDownListService.SelectedValue)).serviceName,
                    serviceResources = serviceS.GetElementBM(Convert.ToInt32(DropDownListService.SelectedValue)).serviceResources
                });
                Server.Transfer("FormCreateOrder.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormCreateOrder.aspx");
        }
    }
}