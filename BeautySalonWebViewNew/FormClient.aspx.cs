
using BeautySalonService.BindingModels;
using BeautySalonService.ImplementationsBD;
using BeautySalonService.ImplementationsList;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;
using Unity.Attributes;

namespace BeautySalonWebView
{
    public partial class FormClient : System.Web.UI.Page
    {
        public int Id { set { id = value; } }

        private readonly IClientService service = UnityConfig.Container.Resolve<IClientService>();
        private readonly IMainService serviceM = UnityConfig.Container.Resolve<IMainService>();


        private int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    ClientViewModel view = service.GetElement(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            textBoxFirstName.Text = view.clientFirstName;
                            textBoxSecondName.Text = view.clientSecondName;
                            TextBoxPassword.Text = view.password;
                            TextBoxMail.Text = view.mail;
                            textBoxNumber.Text = view.number.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFirstName.Text)|| string.IsNullOrEmpty(textBoxSecondName.Text)|| string.IsNullOrEmpty(textBoxNumber.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поля');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new ClientBindingModel
                    {
                        id = id,
                        clientFirstName = textBoxFirstName.Text,
                        clientSecondName= textBoxSecondName.Text,
                        number=Convert.ToInt32(textBoxNumber.Text),
                        mail = TextBoxMail.Text,
                        password = TextBoxPassword.Text
                    });
                }
                else
                {
                    service.AddElement(new ClientBindingModel
                    {
                        clientFirstName = textBoxFirstName.Text,
                        clientSecondName = textBoxSecondName.Text,
                        number = Convert.ToInt32(textBoxNumber.Text),
                       mail = TextBoxMail.Text,
                       password = TextBoxPassword.Text

                    });
                }
                MainServiceBD.iDClient = serviceM.GetIdClient(textBoxFirstName.Text, textBoxSecondName.Text, TextBoxPassword.Text);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Server.Transfer("FormSignIn.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Server.Transfer("FormMain.aspx");
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("FormSignIn.aspx");
        }
    }
}