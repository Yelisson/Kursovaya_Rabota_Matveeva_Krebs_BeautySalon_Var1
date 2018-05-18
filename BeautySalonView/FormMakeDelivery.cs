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
    public partial class FormMakeDelivery : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDeliveryService serviceS;

        private readonly IResourceService serviceC;

        private readonly IMainService serviceM;

        public FormMakeDelivery(IDeliveryService serviceS, IResourceService serviceC, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }

        private void FormMakeDelivery_Load(object sender, EventArgs e)
        {
            try
            {
                List<ResourceViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxResource.DisplayMember = "resourceName";
                    comboBoxResource.ValueMember = "id";
                    comboBoxResource.DataSource = listC;
                    comboBoxResource.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxDelivery.Text))
            {
                MessageBox.Show("Заполните поле номер доставки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxResource.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceS.AddElement(new DeliveryBindingModel
                {
                    id = Convert.ToInt32(textBoxDelivery.Text)
                });
                
                serviceM.PutComponent(new DeliveryResourceBindingModel
                {
                    resourceId = Convert.ToInt32(comboBoxResource.SelectedValue),
                    count = Convert.ToInt32(textBoxCount.Text)
                });


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
