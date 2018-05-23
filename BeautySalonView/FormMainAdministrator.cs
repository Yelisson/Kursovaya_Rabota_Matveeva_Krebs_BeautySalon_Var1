using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace BeautySalonView
{
    public partial class FormMainAdministrator : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMainService service;
        private readonly IResourceService serviceR;

        public FormMainAdministrator(IMainService service, IResourceService serviceR)
        {
            InitializeComponent();
            this.service = service;
            this.serviceR = serviceR;
        }
        private void FormMainAdministrator_Load(object sender, EventArgs e)
        {
           LoadDataOrders();
           LoadDataResources();
        }

        private void LoadDataOrders()
        {
            try
            {
                List<OrderViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewOrders.DataSource = list;
                    dataGridViewOrders.Columns[1].Visible = false;
                    dataGridViewOrders.Columns[3].Visible = false;
                    dataGridViewOrders.Columns[5].Visible = false;
                    dataGridViewOrders.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDataResources()
        {
            try
            {
                List<ResourceViewModel> list = serviceR.GetList();
                if (list != null)
                {
                    dataGridViewResources.DataSource = list;
                    dataGridViewResources.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonReady_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.FinishOrder(id);
                    LoadDataOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonPay_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.PayOrder(id);
                    LoadDataOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonRenew_Click(object sender, EventArgs e)
        {
            LoadDataOrders();
            LoadDataResources();
        }

        private void buttonTakeOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.TakeOrderInWork(id);
                    LoadDataOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ресурсыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormResources>();
            form.ShowDialog();
        }

        private void услугиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormServices>();
            form.ShowDialog();
        }

        private void сформироватьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormMakeDelivery>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDataResources();
            }
        }
    }
}
