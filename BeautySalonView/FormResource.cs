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
    public partial class FormResource : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IResourceService service;

        private int? id;

        public FormResource(IResourceService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormResource_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ResourceViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.resourceName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new ResourceBindingModel
                    {
                        id = id.Value,
                        resourceName = textBoxName.Text,
                        price = Convert.ToInt32(textBoxPrice.Text)
                    });
                }
                else
                {
                    service.AddElement(new ResourceBindingModel
                    {
                        resourceName = textBoxName.Text,
                        price = Convert.ToInt32(textBoxPrice.Text)
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