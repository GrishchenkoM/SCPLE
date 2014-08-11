namespace SCPLE.View
{
    partial class CreateSpecificationView
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
            this.SpecDesignation_gbx = new System.Windows.Forms.GroupBox();
            this.DesignDoc_maskedTB = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ListOfitems_chkBx = new System.Windows.Forms.CheckBox();
            this.AssemblyDrawing_chkBx = new System.Windows.Forms.CheckBox();
            this.ElectricalCircuit_chkBx = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DesignDoc_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DesignPcb_maskedTB = new System.Windows.Forms.MaskedTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Pcb_chkBx = new System.Windows.Forms.CheckBox();
            this.CertifyingSheet_chkBx = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DesignPcb_TB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ElementsOfSMDMounting_chkBx = new System.Windows.Forms.CheckBox();
            this.BorrowedItems_chkBx = new System.Windows.Forms.CheckBox();
            this.FileDoc_chkBx = new System.Windows.Forms.CheckBox();
            this.FileXls_chkBx = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Start_btn = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.SpecDesignation_gbx.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SpecDesignation_gbx
            // 
            this.SpecDesignation_gbx.Controls.Add(this.DesignDoc_maskedTB);
            this.SpecDesignation_gbx.Controls.Add(this.groupBox1);
            this.SpecDesignation_gbx.Controls.Add(this.label3);
            this.SpecDesignation_gbx.Controls.Add(this.DesignDoc_TB);
            this.SpecDesignation_gbx.Controls.Add(this.label1);
            this.SpecDesignation_gbx.Location = new System.Drawing.Point(13, 13);
            this.SpecDesignation_gbx.Name = "SpecDesignation_gbx";
            this.SpecDesignation_gbx.Size = new System.Drawing.Size(325, 100);
            this.SpecDesignation_gbx.TabIndex = 0;
            this.SpecDesignation_gbx.TabStop = false;
            this.SpecDesignation_gbx.Text = "Обозначение документации";
            // 
            // DesignDoc_maskedTB
            // 
            this.DesignDoc_maskedTB.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.DesignDoc_maskedTB.Location = new System.Drawing.Point(62, 39);
            this.DesignDoc_maskedTB.Mask = "999999";
            this.DesignDoc_maskedTB.Name = "DesignDoc_maskedTB";
            this.DesignDoc_maskedTB.PromptChar = ' ';
            this.DesignDoc_maskedTB.Size = new System.Drawing.Size(70, 33);
            this.DesignDoc_maskedTB.TabIndex = 7;
            this.DesignDoc_maskedTB.Text = "999999";
            this.DesignDoc_maskedTB.Click += new System.EventHandler(this.DesignDoc_maskedTB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ListOfitems_chkBx);
            this.groupBox1.Controls.Add(this.AssemblyDrawing_chkBx);
            this.groupBox1.Controls.Add(this.ElectricalCircuit_chkBx);
            this.groupBox1.Location = new System.Drawing.Point(197, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 83);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Другие документы";
            // 
            // ListOfitems_chkBx
            // 
            this.ListOfitems_chkBx.AutoSize = true;
            this.ListOfitems_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ListOfitems_chkBx.Location = new System.Drawing.Point(25, 56);
            this.ListOfitems_chkBx.Name = "ListOfitems_chkBx";
            this.ListOfitems_chkBx.Size = new System.Drawing.Size(59, 25);
            this.ListOfitems_chkBx.TabIndex = 1;
            this.ListOfitems_chkBx.Text = "ПЭЗ";
            this.ListOfitems_chkBx.UseVisualStyleBackColor = true;
            // 
            // AssemblyDrawing_chkBx
            // 
            this.AssemblyDrawing_chkBx.AutoSize = true;
            this.AssemblyDrawing_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AssemblyDrawing_chkBx.Location = new System.Drawing.Point(25, 16);
            this.AssemblyDrawing_chkBx.Name = "AssemblyDrawing_chkBx";
            this.AssemblyDrawing_chkBx.Size = new System.Drawing.Size(48, 25);
            this.AssemblyDrawing_chkBx.TabIndex = 1;
            this.AssemblyDrawing_chkBx.Text = "СБ";
            this.AssemblyDrawing_chkBx.UseVisualStyleBackColor = true;
            // 
            // ElectricalCircuit_chkBx
            // 
            this.ElectricalCircuit_chkBx.AutoSize = true;
            this.ElectricalCircuit_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ElectricalCircuit_chkBx.Location = new System.Drawing.Point(25, 36);
            this.ElectricalCircuit_chkBx.Name = "ElectricalCircuit_chkBx";
            this.ElectricalCircuit_chkBx.Size = new System.Drawing.Size(48, 25);
            this.ElectricalCircuit_chkBx.TabIndex = 1;
            this.ElectricalCircuit_chkBx.Text = "ЭЗ";
            this.ElectricalCircuit_chkBx.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(129, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = ".";
            // 
            // DesignDoc_TB
            // 
            this.DesignDoc_TB.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DesignDoc_TB.Location = new System.Drawing.Point(142, 39);
            this.DesignDoc_TB.Name = "DesignDoc_TB";
            this.DesignDoc_TB.Size = new System.Drawing.Size(39, 33);
            this.DesignDoc_TB.TabIndex = 1;
            this.DesignDoc_TB.Text = "888";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "ААОТ.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DesignPcb_maskedTB);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.DesignPcb_TB);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(13, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Обозначение платы";
            // 
            // DesignPcb_maskedTB
            // 
            this.DesignPcb_maskedTB.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.DesignPcb_maskedTB.Location = new System.Drawing.Point(62, 36);
            this.DesignPcb_maskedTB.Mask = "999999";
            this.DesignPcb_maskedTB.Name = "DesignPcb_maskedTB";
            this.DesignPcb_maskedTB.PromptChar = ' ';
            this.DesignPcb_maskedTB.Size = new System.Drawing.Size(70, 33);
            this.DesignPcb_maskedTB.TabIndex = 7;
            this.DesignPcb_maskedTB.Text = "999999";
            this.DesignPcb_maskedTB.Click += new System.EventHandler(this.DesignPcb_maskedTB_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Pcb_chkBx);
            this.groupBox3.Controls.Add(this.CertifyingSheet_chkBx);
            this.groupBox3.Location = new System.Drawing.Point(197, 11);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(122, 83);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Другие документы";
            // 
            // Pcb_chkBx
            // 
            this.Pcb_chkBx.AutoSize = true;
            this.Pcb_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Pcb_chkBx.Location = new System.Drawing.Point(25, 25);
            this.Pcb_chkBx.Name = "Pcb_chkBx";
            this.Pcb_chkBx.Size = new System.Drawing.Size(71, 25);
            this.Pcb_chkBx.TabIndex = 1;
            this.Pcb_chkBx.Text = "Плата";
            this.Pcb_chkBx.UseVisualStyleBackColor = true;
            // 
            // CertifyingSheet_chkBx
            // 
            this.CertifyingSheet_chkBx.AutoSize = true;
            this.CertifyingSheet_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CertifyingSheet_chkBx.Location = new System.Drawing.Point(25, 45);
            this.CertifyingSheet_chkBx.Name = "CertifyingSheet_chkBx";
            this.CertifyingSheet_chkBx.Size = new System.Drawing.Size(49, 25);
            this.CertifyingSheet_chkBx.TabIndex = 1;
            this.CertifyingSheet_chkBx.Text = "УД";
            this.CertifyingSheet_chkBx.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(129, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = ".";
            // 
            // DesignPcb_TB
            // 
            this.DesignPcb_TB.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DesignPcb_TB.Location = new System.Drawing.Point(142, 36);
            this.DesignPcb_TB.Name = "DesignPcb_TB";
            this.DesignPcb_TB.Size = new System.Drawing.Size(39, 33);
            this.DesignPcb_TB.TabIndex = 1;
            this.DesignPcb_TB.Text = "888";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 23);
            this.label6.TabIndex = 1;
            this.label6.Text = "ААОТ.";
            // 
            // ElementsOfSMDMounting_chkBx
            // 
            this.ElementsOfSMDMounting_chkBx.AutoSize = true;
            this.ElementsOfSMDMounting_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ElementsOfSMDMounting_chkBx.Location = new System.Drawing.Point(13, 225);
            this.ElementsOfSMDMounting_chkBx.Name = "ElementsOfSMDMounting_chkBx";
            this.ElementsOfSMDMounting_chkBx.Size = new System.Drawing.Size(207, 25);
            this.ElementsOfSMDMounting_chkBx.TabIndex = 1;
            this.ElementsOfSMDMounting_chkBx.Text = "Элементы SMD монтажа";
            this.ElementsOfSMDMounting_chkBx.UseVisualStyleBackColor = true;
            // 
            // BorrowedItems_chkBx
            // 
            this.BorrowedItems_chkBx.AutoSize = true;
            this.BorrowedItems_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BorrowedItems_chkBx.Location = new System.Drawing.Point(13, 253);
            this.BorrowedItems_chkBx.Name = "BorrowedItems_chkBx";
            this.BorrowedItems_chkBx.Size = new System.Drawing.Size(212, 25);
            this.BorrowedItems_chkBx.TabIndex = 1;
            this.BorrowedItems_chkBx.Text = "Заимствованные изделия";
            this.BorrowedItems_chkBx.UseVisualStyleBackColor = true;
            // 
            // FileDoc_chkBx
            // 
            this.FileDoc_chkBx.AutoSize = true;
            this.FileDoc_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FileDoc_chkBx.Location = new System.Drawing.Point(13, 67);
            this.FileDoc_chkBx.Name = "FileDoc_chkBx";
            this.FileDoc_chkBx.Size = new System.Drawing.Size(65, 25);
            this.FileDoc_chkBx.TabIndex = 1;
            this.FileDoc_chkBx.Text = ".DOC";
            this.FileDoc_chkBx.UseVisualStyleBackColor = true;
            // 
            // FileXls_chkBx
            // 
            this.FileXls_chkBx.AutoSize = true;
            this.FileXls_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FileXls_chkBx.Location = new System.Drawing.Point(13, 132);
            this.FileXls_chkBx.Name = "FileXls_chkBx";
            this.FileXls_chkBx.Size = new System.Drawing.Size(58, 25);
            this.FileXls_chkBx.TabIndex = 1;
            this.FileXls_chkBx.Text = ".XLS";
            this.FileXls_chkBx.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.FileDoc_chkBx);
            this.groupBox4.Controls.Add(this.FileXls_chkBx);
            this.groupBox4.Location = new System.Drawing.Point(345, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(94, 206);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Создание файла";
            // 
            // Start_btn
            // 
            this.Start_btn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Start_btn.Location = new System.Drawing.Point(272, 244);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(79, 23);
            this.Start_btn.TabIndex = 6;
            this.Start_btn.Text = "Пуск!";
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Cancel.Location = new System.Drawing.Point(358, 244);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(79, 23);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 290);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(451, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // CreateSpecificationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(451, 312);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Start_btn);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BorrowedItems_chkBx);
            this.Controls.Add(this.ElementsOfSMDMounting_chkBx);
            this.Controls.Add(this.SpecDesignation_gbx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(461, 344);
            this.MinimumSize = new System.Drawing.Size(461, 344);
            this.Name = "CreateSpecificationView";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spec-Creator:  Creation of specification";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateSpecificationView_FormClosing);
            this.SpecDesignation_gbx.ResumeLayout(false);
            this.SpecDesignation_gbx.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox SpecDesignation_gbx;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ListOfitems_chkBx;
        private System.Windows.Forms.CheckBox AssemblyDrawing_chkBx;
        private System.Windows.Forms.CheckBox ElectricalCircuit_chkBx;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox DesignDoc_TB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox Pcb_chkBx;
        private System.Windows.Forms.CheckBox CertifyingSheet_chkBx;
        public System.Windows.Forms.TextBox DesignPcb_TB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ElementsOfSMDMounting_chkBx;
        private System.Windows.Forms.CheckBox BorrowedItems_chkBx;
        private System.Windows.Forms.CheckBox FileDoc_chkBx;
        private System.Windows.Forms.CheckBox FileXls_chkBx;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Button Cancel;
        public System.Windows.Forms.MaskedTextBox DesignDoc_maskedTB;
        public System.Windows.Forms.MaskedTextBox DesignPcb_maskedTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

    }
}