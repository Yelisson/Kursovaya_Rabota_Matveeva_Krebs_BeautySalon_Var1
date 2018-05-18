using BeautySalonService.BindingModel;
using BeautySalonService.BindingModels;
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
    public partial class FormService : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IServiceService service;

        private int? id;

        private List<ServiceResourceViewModel> serviceResources;

        public FormService(IServiceService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormService_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ServiceViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.serviceName;
                        textBoxPrice.Text = view.price.ToString();
                        serviceResources = view.ServiceResources;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                serviceResources = new List<ServiceResourceViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (serviceResources != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = serviceResources;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormServiceResource>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.serviceId = id.Value;
                    }
                    serviceResources.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormServiceResource>();
                form.Model = serviceResources[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    serviceResources[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        serviceResources.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (serviceResources == null || serviceResources.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<ServiceResourceBindingModel> serviceResourceBM = new List<ServiceResourceBindingModel>();
                for (int i = 0; i < serviceResources.Count; ++i)
                {
                    serviceResourceBM.Add(new ServiceResourceBindingModel
                    {
                        id = serviceResources[i].id,
                        serviceId = serviceResources[i].serviceId,
                        resourceId = serviceResources[i].resourceId,
                        count = serviceResources[i].count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new ServiceBindingModel
                    {
                        id = id.Value,
                        serviceName = textBoxName.Text,
                        price = Convert.ToInt32(textBoxPrice.Text),
                        serviceResources = serviceResourceBM
                    });
                }
                else
                {
                    service.AddElement(new ServiceBindingModel
                    {
                        serviceName = textBoxName.Text,
                        price = Convert.ToInt32(textBoxPrice.Text),
                        serviceResources = serviceResourceBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
