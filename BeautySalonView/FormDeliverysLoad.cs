using BeautySalonService.BindingModel;
using BeautySalonService.ImplementationsBD;
using BeautySalonService.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace BeautySalonView
{
    public partial class FormDeliverysLoad : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IReportService service;
        private readonly IDeliveryService serviceS ;
        public FormDeliverysLoad(IReportService service, IDeliveryService ServiceS)
        {
            InitializeComponent();
            this.service = service;
            this.serviceS = ServiceS;
        }

        private void FormDeliverysLoad_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = service.GetDeliverysLoad();
                if (dict != null)
                {
                    dataGridView1.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        foreach (var listElem in elem.Resources)
                        {
                            dataGridView1.Rows.Add(new object[] { elem.deliveryName, listElem.Item1, listElem.Item2 });
                        }                  
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    service.SaveDeliverysLoad(new ReportBindingModel
                    {
                        FileName = sfd.FileName
                        
                    });
                    /*
                    service.SaveDeliverysLoad(new ReportBindingModel
                    {
                        FileName = "D://deliverys.xls"

                    });*/
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
                try
                {
                    service.SaveDeliverysLoad(new ReportBindingModel
                    {
                        FileName = "D:\\deliverys.xls"
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            string mail = textBoxMail.Text;
            if (!string.IsNullOrEmpty(mail))
            {
                if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                    MessageBox.Show("Неверный формат для электронной почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            try
            {
                serviceS.SendEmail(mail, "Оповещение по заказам",
                string.Format("Отчет по доставкам"));
                MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
