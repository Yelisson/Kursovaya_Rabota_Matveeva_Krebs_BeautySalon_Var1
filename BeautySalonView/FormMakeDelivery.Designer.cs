namespace BeautySalonView
{
    partial class FormMakeDelivery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelCount = new System.Windows.Forms.Label();
            this.comboBoxResource = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.labelResource = new System.Windows.Forms.Label();
            this.labelDelivery = new System.Windows.Forms.Label();
            this.textBoxDelivery = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(12, 63);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 4;
            this.labelCount.Text = "Количество:";
            // 
            // comboBoxResource
            // 
            this.comboBoxResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResource.FormattingEnabled = true;
            this.comboBoxResource.Location = new System.Drawing.Point(87, 33);
            this.comboBoxResource.Name = "comboBoxResource";
            this.comboBoxResource.Size = new System.Drawing.Size(217, 21);
            this.comboBoxResource.TabIndex = 3;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(218, 86);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(137, 86);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(87, 60);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(217, 20);
            this.textBoxCount.TabIndex = 5;
            // 
            // labelResource
            // 
            this.labelResource.AutoSize = true;
            this.labelResource.Location = new System.Drawing.Point(12, 36);
            this.labelResource.Name = "labelResource";
            this.labelResource.Size = new System.Drawing.Size(66, 13);
            this.labelResource.TabIndex = 2;
            this.labelResource.Text = "Компонент:";
            // 
            // labelDelivery
            // 
            this.labelDelivery.AutoSize = true;
            this.labelDelivery.Location = new System.Drawing.Point(12, 9);
            this.labelDelivery.Name = "labelDelivery";
            this.labelDelivery.Size = new System.Drawing.Size(60, 13);
            this.labelDelivery.TabIndex = 0;
            this.labelDelivery.Text = "Доставка:";
            // 
            // textBoxDelivery
            // 
            this.textBoxDelivery.Location = new System.Drawing.Point(87, 6);
            this.textBoxDelivery.Name = "textBoxDelivery";
            this.textBoxDelivery.Size = new System.Drawing.Size(217, 20);
            this.textBoxDelivery.TabIndex = 8;
            // 
            // FormMakeDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 122);
            this.Controls.Add(this.textBoxDelivery);
            this.Controls.Add(this.labelDelivery);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.comboBoxResource);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.labelResource);
            this.Name = "FormMakeDelivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создать заявку";
            this.Load += new System.EventHandler(this.FormMakeDelivery_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ComboBox comboBoxResource;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label labelResource;
        private System.Windows.Forms.Label labelDelivery;
        private System.Windows.Forms.TextBox textBoxDelivery;
    }
}