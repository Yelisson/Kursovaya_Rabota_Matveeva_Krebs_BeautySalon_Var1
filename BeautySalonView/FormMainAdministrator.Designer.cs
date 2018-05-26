namespace BeautySalonView
{
    partial class FormMainAdministrator
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
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ресурсыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.услугиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнитьРесурсыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сформироватьЗаявкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.получитьОтчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewResources = new System.Windows.Forms.DataGridView();
            this.labelOrders = new System.Windows.Forms.Label();
            this.labelResources = new System.Windows.Forms.Label();
            this.buttonReady = new System.Windows.Forms.Button();
            this.buttonRenew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResources)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(12, 221);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.Size = new System.Drawing.Size(551, 169);
            this.dataGridViewOrders.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.пополнитьРесурсыToolStripMenuItem,
            this.получитьОтчетToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(766, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ресурсыToolStripMenuItem,
            this.услугиToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // ресурсыToolStripMenuItem
            // 
            this.ресурсыToolStripMenuItem.Name = "ресурсыToolStripMenuItem";
            this.ресурсыToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.ресурсыToolStripMenuItem.Text = "Ресурсы";
            this.ресурсыToolStripMenuItem.Click += new System.EventHandler(this.ресурсыToolStripMenuItem_Click);
            // 
            // услугиToolStripMenuItem
            // 
            this.услугиToolStripMenuItem.Name = "услугиToolStripMenuItem";
            this.услугиToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.услугиToolStripMenuItem.Text = "Услуги";
            this.услугиToolStripMenuItem.Click += new System.EventHandler(this.услугиToolStripMenuItem_Click);
            // 
            // пополнитьРесурсыToolStripMenuItem
            // 
            this.пополнитьРесурсыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сформироватьЗаявкуToolStripMenuItem});
            this.пополнитьРесурсыToolStripMenuItem.Name = "пополнитьРесурсыToolStripMenuItem";
            this.пополнитьРесурсыToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.пополнитьРесурсыToolStripMenuItem.Text = "Пополнить ресурсы";
            // 
            // сформироватьЗаявкуToolStripMenuItem
            // 
            this.сформироватьЗаявкуToolStripMenuItem.Name = "сформироватьЗаявкуToolStripMenuItem";
            this.сформироватьЗаявкуToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.сформироватьЗаявкуToolStripMenuItem.Text = "Сформировать заявку";
            this.сформироватьЗаявкуToolStripMenuItem.Click += new System.EventHandler(this.сформироватьЗаявкуToolStripMenuItem_Click);
            // 
            // получитьОтчетToolStripMenuItem
            // 
            this.получитьОтчетToolStripMenuItem.Name = "получитьОтчетToolStripMenuItem";
            this.получитьОтчетToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.получитьОтчетToolStripMenuItem.Text = "Получить отчет";
            // 
            // dataGridViewResources
            // 
            this.dataGridViewResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResources.Location = new System.Drawing.Point(12, 53);
            this.dataGridViewResources.Name = "dataGridViewResources";
            this.dataGridViewResources.Size = new System.Drawing.Size(739, 144);
            this.dataGridViewResources.TabIndex = 2;
            // 
            // labelOrders
            // 
            this.labelOrders.AutoSize = true;
            this.labelOrders.Location = new System.Drawing.Point(38, 200);
            this.labelOrders.Name = "labelOrders";
            this.labelOrders.Size = new System.Drawing.Size(46, 13);
            this.labelOrders.TabIndex = 3;
            this.labelOrders.Text = "Заказы";
            // 
            // labelResources
            // 
            this.labelResources.AutoSize = true;
            this.labelResources.Location = new System.Drawing.Point(38, 37);
            this.labelResources.Name = "labelResources";
            this.labelResources.Size = new System.Drawing.Size(51, 13);
            this.labelResources.TabIndex = 4;
            this.labelResources.Text = "Ресурсы";
            // 
            // buttonReady
            // 
            this.buttonReady.Location = new System.Drawing.Point(607, 318);
            this.buttonReady.Name = "buttonReady";
            this.buttonReady.Size = new System.Drawing.Size(119, 23);
            this.buttonReady.TabIndex = 6;
            this.buttonReady.Text = "Заказ готов";
            this.buttonReady.UseVisualStyleBackColor = true;
            this.buttonReady.Click += new System.EventHandler(this.buttonReady_Click);
            // 
            // buttonRenew
            // 
            this.buttonRenew.Location = new System.Drawing.Point(607, 359);
            this.buttonRenew.Name = "buttonRenew";
            this.buttonRenew.Size = new System.Drawing.Size(119, 23);
            this.buttonRenew.TabIndex = 8;
            this.buttonRenew.Text = "Обновить список";
            this.buttonRenew.UseVisualStyleBackColor = true;
            this.buttonRenew.Click += new System.EventHandler(this.buttonRenew_Click);
            // 
            // FormMainAdministrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 402);
            this.Controls.Add(this.buttonRenew);
            this.Controls.Add(this.buttonReady);
            this.Controls.Add(this.labelResources);
            this.Controls.Add(this.labelOrders);
            this.Controls.Add(this.dataGridViewResources);
            this.Controls.Add(this.dataGridViewOrders);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMainAdministrator";
            this.Text = "FormMainAdministrator";
            this.Load += new System.EventHandler(this.FormMainAdministrator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResources)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ресурсыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem услугиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem получитьОтчетToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewResources;
        private System.Windows.Forms.Label labelOrders;
        private System.Windows.Forms.Label labelResources;
        private System.Windows.Forms.ToolStripMenuItem пополнитьРесурсыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сформироватьЗаявкуToolStripMenuItem;
        private System.Windows.Forms.Button buttonReady;
        private System.Windows.Forms.Button buttonRenew;
    }
}