namespace BeautySalonView
{
    partial class FormClientOrders
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.buttonMakeReport = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ClientOrdersModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ClientOrdersModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(13, 13);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(17, 13);
            this.labelFrom.TabIndex = 0;
            this.labelFrom.Text = "С:";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(204, 13);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(24, 13);
            this.labelTo.TabIndex = 1;
            this.labelTo.Text = "По:";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(54, 7);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(131, 20);
            this.dateTimePickerFrom.TabIndex = 2;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(245, 7);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(131, 20);
            this.dateTimePickerTo.TabIndex = 3;
            // 
            // buttonMakeReport
            // 
            this.buttonMakeReport.Location = new System.Drawing.Point(412, 7);
            this.buttonMakeReport.Name = "buttonMakeReport";
            this.buttonMakeReport.Size = new System.Drawing.Size(128, 23);
            this.buttonMakeReport.TabIndex = 4;
            this.buttonMakeReport.Text = "Сформировать";
            this.buttonMakeReport.UseVisualStyleBackColor = true;
            this.buttonMakeReport.Click += new System.EventHandler(this.buttonMakeReport_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(612, 7);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(109, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "В pdf";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSetOrders";
            reportDataSource1.Value = this.ClientOrdersModelBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BeautySalonView.ReportClientOrders.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 36);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(709, 313);
            this.reportViewer1.TabIndex = 6;
            // 
            // ClientOrdersModelBindingSource
            // 
            this.ClientOrdersModelBindingSource.DataSource = typeof(BeautySalonService.ViewModels.ClientOrdersModel);
            // 
            // FormClientOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 361);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonMakeReport);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Name = "FormClientOrders";
            this.Text = "FormClientOrders";
            this.Load += new System.EventHandler(this.FormClientOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ClientOrdersModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Button buttonMakeReport;
        private System.Windows.Forms.Button buttonSave;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ClientOrdersModelBindingSource;
    }
}