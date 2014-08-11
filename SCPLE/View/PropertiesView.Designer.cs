namespace SCPLE.View
{
    partial class PropertiesView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesView));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Main_tabPage = new System.Windows.Forms.TabPage();
            this.BorrowedItems_chkBx = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ListOfitems_cmbBx = new System.Windows.Forms.ComboBox();
            this.ElectricalCircuit_cmbBx = new System.Windows.Forms.ComboBox();
            this.AssemblyDrawing_cmbBx = new System.Windows.Forms.ComboBox();
            this.CertifyingSheet_cmbBx = new System.Windows.Forms.ComboBox();
            this.Pcb_cmbBx = new System.Windows.Forms.ComboBox();
            this.ListOfitems_chkBx = new System.Windows.Forms.CheckBox();
            this.Pcb_chkBx = new System.Windows.Forms.CheckBox();
            this.AssemblyDrawing_chkBx = new System.Windows.Forms.CheckBox();
            this.ElectricalCircuit_chkBx = new System.Windows.Forms.CheckBox();
            this.CertifyingSheet_chkBx = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.FileDoc_chkBx = new System.Windows.Forms.CheckBox();
            this.FileXls_chkBx = new System.Windows.Forms.CheckBox();
            this.ElementsOfSMDMounting_chkBx = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DesignPcb_maskedTB = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Template_tabPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SpecificationTemplate_txBx = new System.Windows.Forms.TextBox();
            this.ListPath_btn = new System.Windows.Forms.Button();
            this.Settings_tabPage = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.SettingsPath_btn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.Settings_txBx = new System.Windows.Forms.TextBox();
            this.Save_btn = new System.Windows.Forms.Button();
            this.Exit_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.Main_tabPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Template_tabPage.SuspendLayout();
            this.Settings_tabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Main_tabPage);
            this.tabControl1.Controls.Add(this.Template_tabPage);
            this.tabControl1.Controls.Add(this.Settings_tabPage);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // Main_tabPage
            // 
            this.Main_tabPage.Controls.Add(this.BorrowedItems_chkBx);
            this.Main_tabPage.Controls.Add(this.groupBox3);
            this.Main_tabPage.Controls.Add(this.groupBox4);
            this.Main_tabPage.Controls.Add(this.ElementsOfSMDMounting_chkBx);
            this.Main_tabPage.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.Main_tabPage, "Main_tabPage");
            this.Main_tabPage.Name = "Main_tabPage";
            this.Main_tabPage.UseVisualStyleBackColor = true;
            // 
            // BorrowedItems_chkBx
            // 
            resources.ApplyResources(this.BorrowedItems_chkBx, "BorrowedItems_chkBx");
            this.BorrowedItems_chkBx.Name = "BorrowedItems_chkBx";
            this.BorrowedItems_chkBx.UseVisualStyleBackColor = true;
            this.BorrowedItems_chkBx.CheckedChanged += new System.EventHandler(this.BorrowedItems_chkBx_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ListOfitems_cmbBx);
            this.groupBox3.Controls.Add(this.ElectricalCircuit_cmbBx);
            this.groupBox3.Controls.Add(this.AssemblyDrawing_cmbBx);
            this.groupBox3.Controls.Add(this.CertifyingSheet_cmbBx);
            this.groupBox3.Controls.Add(this.Pcb_cmbBx);
            this.groupBox3.Controls.Add(this.ListOfitems_chkBx);
            this.groupBox3.Controls.Add(this.Pcb_chkBx);
            this.groupBox3.Controls.Add(this.AssemblyDrawing_chkBx);
            this.groupBox3.Controls.Add(this.ElectricalCircuit_chkBx);
            this.groupBox3.Controls.Add(this.CertifyingSheet_chkBx);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // ListOfitems_cmbBx
            // 
            this.ListOfitems_cmbBx.FormattingEnabled = true;
            this.ListOfitems_cmbBx.Items.AddRange(new object[] {
            resources.GetString("ListOfitems_cmbBx.Items"),
            resources.GetString("ListOfitems_cmbBx.Items1"),
            resources.GetString("ListOfitems_cmbBx.Items2"),
            resources.GetString("ListOfitems_cmbBx.Items3")});
            resources.ApplyResources(this.ListOfitems_cmbBx, "ListOfitems_cmbBx");
            this.ListOfitems_cmbBx.Name = "ListOfitems_cmbBx";
            this.ListOfitems_cmbBx.Click += new System.EventHandler(this.ListOfitems_cmbBx_Click);
            // 
            // ElectricalCircuit_cmbBx
            // 
            this.ElectricalCircuit_cmbBx.FormattingEnabled = true;
            this.ElectricalCircuit_cmbBx.Items.AddRange(new object[] {
            resources.GetString("ElectricalCircuit_cmbBx.Items"),
            resources.GetString("ElectricalCircuit_cmbBx.Items1"),
            resources.GetString("ElectricalCircuit_cmbBx.Items2"),
            resources.GetString("ElectricalCircuit_cmbBx.Items3")});
            resources.ApplyResources(this.ElectricalCircuit_cmbBx, "ElectricalCircuit_cmbBx");
            this.ElectricalCircuit_cmbBx.Name = "ElectricalCircuit_cmbBx";
            this.ElectricalCircuit_cmbBx.Click += new System.EventHandler(this.ElectricalCircuit_cmbBx_Click);
            // 
            // AssemblyDrawing_cmbBx
            // 
            this.AssemblyDrawing_cmbBx.FormattingEnabled = true;
            this.AssemblyDrawing_cmbBx.Items.AddRange(new object[] {
            resources.GetString("AssemblyDrawing_cmbBx.Items"),
            resources.GetString("AssemblyDrawing_cmbBx.Items1"),
            resources.GetString("AssemblyDrawing_cmbBx.Items2"),
            resources.GetString("AssemblyDrawing_cmbBx.Items3")});
            resources.ApplyResources(this.AssemblyDrawing_cmbBx, "AssemblyDrawing_cmbBx");
            this.AssemblyDrawing_cmbBx.Name = "AssemblyDrawing_cmbBx";
            this.AssemblyDrawing_cmbBx.Click += new System.EventHandler(this.AssemblyDrawing_cmbBx_Click);
            // 
            // CertifyingSheet_cmbBx
            // 
            this.CertifyingSheet_cmbBx.FormattingEnabled = true;
            this.CertifyingSheet_cmbBx.Items.AddRange(new object[] {
            resources.GetString("CertifyingSheet_cmbBx.Items"),
            resources.GetString("CertifyingSheet_cmbBx.Items1"),
            resources.GetString("CertifyingSheet_cmbBx.Items2"),
            resources.GetString("CertifyingSheet_cmbBx.Items3")});
            resources.ApplyResources(this.CertifyingSheet_cmbBx, "CertifyingSheet_cmbBx");
            this.CertifyingSheet_cmbBx.Name = "CertifyingSheet_cmbBx";
            this.CertifyingSheet_cmbBx.Click += new System.EventHandler(this.CertifyingSheet_cmbBx_Click);
            // 
            // Pcb_cmbBx
            // 
            this.Pcb_cmbBx.FormattingEnabled = true;
            this.Pcb_cmbBx.Items.AddRange(new object[] {
            resources.GetString("Pcb_cmbBx.Items"),
            resources.GetString("Pcb_cmbBx.Items1"),
            resources.GetString("Pcb_cmbBx.Items2"),
            resources.GetString("Pcb_cmbBx.Items3")});
            resources.ApplyResources(this.Pcb_cmbBx, "Pcb_cmbBx");
            this.Pcb_cmbBx.Name = "Pcb_cmbBx";
            this.Pcb_cmbBx.Click += new System.EventHandler(this.Pcb_cmbBx_Click);
            // 
            // ListOfitems_chkBx
            // 
            resources.ApplyResources(this.ListOfitems_chkBx, "ListOfitems_chkBx");
            this.ListOfitems_chkBx.Name = "ListOfitems_chkBx";
            this.ListOfitems_chkBx.UseVisualStyleBackColor = true;
            this.ListOfitems_chkBx.CheckedChanged += new System.EventHandler(this.ListOfitems_chkBx_CheckedChanged);
            // 
            // Pcb_chkBx
            // 
            resources.ApplyResources(this.Pcb_chkBx, "Pcb_chkBx");
            this.Pcb_chkBx.Name = "Pcb_chkBx";
            this.Pcb_chkBx.UseVisualStyleBackColor = true;
            this.Pcb_chkBx.CheckedChanged += new System.EventHandler(this.Pcb_chkBx_CheckedChanged);
            // 
            // AssemblyDrawing_chkBx
            // 
            resources.ApplyResources(this.AssemblyDrawing_chkBx, "AssemblyDrawing_chkBx");
            this.AssemblyDrawing_chkBx.Name = "AssemblyDrawing_chkBx";
            this.AssemblyDrawing_chkBx.UseVisualStyleBackColor = true;
            this.AssemblyDrawing_chkBx.CheckedChanged += new System.EventHandler(this.AssemblyDrawing_chkBx_CheckedChanged);
            // 
            // ElectricalCircuit_chkBx
            // 
            resources.ApplyResources(this.ElectricalCircuit_chkBx, "ElectricalCircuit_chkBx");
            this.ElectricalCircuit_chkBx.Name = "ElectricalCircuit_chkBx";
            this.ElectricalCircuit_chkBx.UseVisualStyleBackColor = true;
            this.ElectricalCircuit_chkBx.CheckedChanged += new System.EventHandler(this.ElectricalCircuit_chkBx_CheckedChanged);
            // 
            // CertifyingSheet_chkBx
            // 
            resources.ApplyResources(this.CertifyingSheet_chkBx, "CertifyingSheet_chkBx");
            this.CertifyingSheet_chkBx.Name = "CertifyingSheet_chkBx";
            this.CertifyingSheet_chkBx.UseVisualStyleBackColor = true;
            this.CertifyingSheet_chkBx.CheckedChanged += new System.EventHandler(this.CertifyingSheet_chkBx_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.FileDoc_chkBx);
            this.groupBox4.Controls.Add(this.FileXls_chkBx);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // FileDoc_chkBx
            // 
            resources.ApplyResources(this.FileDoc_chkBx, "FileDoc_chkBx");
            this.FileDoc_chkBx.Name = "FileDoc_chkBx";
            this.FileDoc_chkBx.UseVisualStyleBackColor = true;
            this.FileDoc_chkBx.CheckedChanged += new System.EventHandler(this.FileDoc_chkBx_CheckedChanged);
            // 
            // FileXls_chkBx
            // 
            resources.ApplyResources(this.FileXls_chkBx, "FileXls_chkBx");
            this.FileXls_chkBx.Name = "FileXls_chkBx";
            this.FileXls_chkBx.UseVisualStyleBackColor = true;
            this.FileXls_chkBx.CheckedChanged += new System.EventHandler(this.FileXls_chkBx_CheckedChanged);
            // 
            // ElementsOfSMDMounting_chkBx
            // 
            resources.ApplyResources(this.ElementsOfSMDMounting_chkBx, "ElementsOfSMDMounting_chkBx");
            this.ElementsOfSMDMounting_chkBx.Name = "ElementsOfSMDMounting_chkBx";
            this.ElementsOfSMDMounting_chkBx.UseVisualStyleBackColor = true;
            this.ElementsOfSMDMounting_chkBx.CheckedChanged += new System.EventHandler(this.ElementsOfSMDMounting_chkBx_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DesignPcb_maskedTB);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // DesignPcb_maskedTB
            // 
            resources.ApplyResources(this.DesignPcb_maskedTB, "DesignPcb_maskedTB");
            this.DesignPcb_maskedTB.Name = "DesignPcb_maskedTB";
            this.DesignPcb_maskedTB.TextChanged += new System.EventHandler(this.DesignPcb_maskedTB_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // Template_tabPage
            // 
            this.Template_tabPage.Controls.Add(this.label4);
            this.Template_tabPage.Controls.Add(this.label3);
            this.Template_tabPage.Controls.Add(this.SpecificationTemplate_txBx);
            this.Template_tabPage.Controls.Add(this.ListPath_btn);
            resources.ApplyResources(this.Template_tabPage, "Template_tabPage");
            this.Template_tabPage.Name = "Template_tabPage";
            this.Template_tabPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // SpecificationTemplate_txBx
            // 
            resources.ApplyResources(this.SpecificationTemplate_txBx, "SpecificationTemplate_txBx");
            this.SpecificationTemplate_txBx.Name = "SpecificationTemplate_txBx";
            // 
            // ListPath_btn
            // 
            resources.ApplyResources(this.ListPath_btn, "ListPath_btn");
            this.ListPath_btn.Name = "ListPath_btn";
            this.ListPath_btn.UseVisualStyleBackColor = true;
            this.ListPath_btn.Click += new System.EventHandler(this.SpecificationTemplatePath_btn_Click);
            // 
            // Settings_tabPage
            // 
            this.Settings_tabPage.Controls.Add(this.label5);
            this.Settings_tabPage.Controls.Add(this.SettingsPath_btn);
            this.Settings_tabPage.Controls.Add(this.label7);
            this.Settings_tabPage.Controls.Add(this.Settings_txBx);
            resources.ApplyResources(this.Settings_tabPage, "Settings_tabPage");
            this.Settings_tabPage.Name = "Settings_tabPage";
            this.Settings_tabPage.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // SettingsPath_btn
            // 
            resources.ApplyResources(this.SettingsPath_btn, "SettingsPath_btn");
            this.SettingsPath_btn.Name = "SettingsPath_btn";
            this.SettingsPath_btn.UseVisualStyleBackColor = true;
            this.SettingsPath_btn.Click += new System.EventHandler(this.SettingsPath_btn_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // Settings_txBx
            // 
            resources.ApplyResources(this.Settings_txBx, "Settings_txBx");
            this.Settings_txBx.Name = "Settings_txBx";
            // 
            // Save_btn
            // 
            resources.ApplyResources(this.Save_btn, "Save_btn");
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // Exit_btn
            // 
            this.Exit_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.Exit_btn, "Exit_btn");
            this.Exit_btn.Name = "Exit_btn";
            this.Exit_btn.UseVisualStyleBackColor = true;
            this.Exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // PropertiesView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Exit_btn;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Exit_btn);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "PropertiesView";
            this.Opacity = 0.95D;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PropertiesView_FormClosing);
            this.Load += new System.EventHandler(this.PropertiesView_Load);
            this.tabControl1.ResumeLayout(false);
            this.Main_tabPage.ResumeLayout(false);
            this.Main_tabPage.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Template_tabPage.ResumeLayout(false);
            this.Template_tabPage.PerformLayout();
            this.Settings_tabPage.ResumeLayout(false);
            this.Settings_tabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Main_tabPage;
        private System.Windows.Forms.TabPage Template_tabPage;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Button Exit_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.MaskedTextBox DesignPcb_maskedTB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox Pcb_chkBx;
        private System.Windows.Forms.CheckBox CertifyingSheet_chkBx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ListOfitems_chkBx;
        private System.Windows.Forms.CheckBox AssemblyDrawing_chkBx;
        private System.Windows.Forms.CheckBox ElectricalCircuit_chkBx;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox FileDoc_chkBx;
        private System.Windows.Forms.CheckBox FileXls_chkBx;
        private System.Windows.Forms.CheckBox BorrowedItems_chkBx;
        private System.Windows.Forms.CheckBox ElementsOfSMDMounting_chkBx;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox SpecificationTemplate_txBx;
        private System.Windows.Forms.Button ListPath_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ListOfitems_cmbBx;
        private System.Windows.Forms.ComboBox ElectricalCircuit_cmbBx;
        private System.Windows.Forms.ComboBox AssemblyDrawing_cmbBx;
        private System.Windows.Forms.ComboBox CertifyingSheet_cmbBx;
        private System.Windows.Forms.ComboBox Pcb_cmbBx;
        private System.Windows.Forms.TabPage Settings_tabPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SettingsPath_btn;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox Settings_txBx;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}