using BeautySalonService.ImplementationsBD;
using BeautySalonService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace BeautySalonWebView
{
    public partial class FormSignIn : System.Web.UI.Page
    {
        private IMainService service = UnityConfig.Container.Resolve<IMainService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSignUp_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormClient.aspx");
        }
        protected void ButtonSignIn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxFirstName.Text) || string.IsNullOrEmpty(TextBoxSecondName.Text) || string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поля');</script>");
                return;
            }

            if (service.GetIdClient(TextBoxFirstName.Text, TextBoxSecondName.Text, TextBoxPassword.Text) == -1){
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('пользователя не существует');</script>");
                return;
            }
            MainServiceBD.iDClient = service.GetIdClient(TextBoxFirstName.Text, TextBoxSecondName.Text, TextBoxPassword.Text);
            Server.Transfer("FormMain.aspx");
        }
    }
}