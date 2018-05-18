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
    public partial class FormServiceResource : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public ServiceResourceViewModel Model { set { model = value; } get { return model; } }

        private readonly IResourceService service;

        private ServiceResourceViewModel model;

        public FormServiceResource(IResourceService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormServiceResource_Load(object sender, EventArgs e)
        {
            try
            {
                List<ResourceViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxResource.DisplayMember = "resourceName";
                    comboBoxResource.ValueMember = "id";
                    comboBoxResource.DataSource = list;
                    comboBoxResource.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxResource.Enabled = false;
                comboBoxResource.SelectedValue = model.resourceId;
                textBoxCount.Text = model.count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxResource.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new ServiceResourceViewModel
                    {
                        resourceId = Convert.ToInt32(comboBoxResource.SelectedValue),
                        resourceName = comboBoxResource.Text,
                        count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.count = Convert.ToInt32(textBoxCount.Text);
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
