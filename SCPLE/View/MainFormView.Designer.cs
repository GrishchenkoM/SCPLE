namespace SCPLE
{
    partial class MainFormView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormView));
            this.ListPath_btn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Operation_gbx = new System.Windows.Forms.GroupBox();
            this.validation_rb = new System.Windows.Forms.RadioButton();
            this.complVerification_rb = new System.Windows.Forms.RadioButton();
            this.createSpec_rb = new System.Windows.Forms.RadioButton();
            this.SpecPath_btn = new System.Windows.Forms.Button();
            this.Specification_txBx = new System.Windows.Forms.TextBox();
            this.NextForm_btn = new System.Windows.Forms.Button();
            this.List_txBx = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.Operation_gbx.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListPath_btn
            // 
            resources.ApplyResources(this.ListPath_btn, "ListPath_btn");
            this.ListPath_btn.Name = "ListPath_btn";
            this.ListPath_btn.UseVisualStyleBackColor = true;
            this.ListPath_btn.Click += new System.EventHandler(this._listFilePathButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // Operation_gbx
            // 
            this.Operation_gbx.Controls.Add(this.validation_rb);
            this.Operation_gbx.Controls.Add(this.complVerification_rb);
            this.Operation_gbx.Controls.Add(this.createSpec_rb);
            resources.ApplyResources(this.Operation_gbx, "Operation_gbx");
            this.Operation_gbx.Name = "Operation_gbx";
            this.Operation_gbx.TabStop = false;
            // 
            // validation_rb
            // 
            resources.ApplyResources(this.validation_rb, "validation_rb");
            this.validation_rb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.validation_rb.Name = "validation_rb";
            this.validation_rb.UseVisualStyleBackColor = true;
            this.validation_rb.CheckedChanged += new System.EventHandler(this.validation_rb_CheckedChanged);
            // 
            // complVerification_rb
            // 
            resources.ApplyResources(this.complVerification_rb, "complVerification_rb");
            this.complVerification_rb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.complVerification_rb.Name = "complVerification_rb";
            this.complVerification_rb.UseVisualStyleBackColor = true;
            this.complVerification_rb.CheckedChanged += new System.EventHandler(this.complVerification_rb_CheckedChanged);
            // 
            // createSpec_rb
            // 
            resources.ApplyResources(this.createSpec_rb, "createSpec_rb");
            this.createSpec_rb.Checked = true;
            this.createSpec_rb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.createSpec_rb.Name = "createSpec_rb";
            this.createSpec_rb.TabStop = true;
            this.createSpec_rb.UseVisualStyleBackColor = true;
            this.createSpec_rb.CheckedChanged += new System.EventHandler(this.createSpec_rb_CheckedChanged);
            // 
            // SpecPath_btn
            // 
            resources.ApplyResources(this.SpecPath_btn, "SpecPath_btn");
            this.SpecPath_btn.Name = "SpecPath_btn";
            this.SpecPath_btn.UseVisualStyleBackColor = true;
            this.SpecPath_btn.Click += new System.EventHandler(this._specificationFilePathButton_Click);
            // 
            // Specification_txBx
            // 
            resources.ApplyResources(this.Specification_txBx, "Specification_txBx");
            this.Specification_txBx.Name = "Specification_txBx";
            this.Specification_txBx.Enter += new System.EventHandler(this.Specification_txBx_Enter);
            this.Specification_txBx.Leave += new System.EventHandler(this.Specification_txBx_Leave);
            // 
            // NextForm_btn
            // 
            this.NextForm_btn.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.NextForm_btn, "NextForm_btn");
            this.NextForm_btn.Name = "NextForm_btn";
            this.NextForm_btn.UseVisualStyleBackColor = true;
            this.NextForm_btn.Click += new System.EventHandler(this._NextFormButton_Click);
            // 
            // List_txBx
            // 
            resources.ApplyResources(this.List_txBx, "List_txBx");
            this.List_txBx.Name = "List_txBx";
            this.List_txBx.Enter += new System.EventHandler(this.List_txBx_Enter);
            this.List_txBx.Leave += new System.EventHandler(this.List_txBx_Leave);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            resources.ApplyResources(this.менюToolStripMenuItem, "менюToolStripMenuItem");
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            resources.ApplyResources(this.настройкиToolStripMenuItem, "настройкиToolStripMenuItem");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
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
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // MainFormView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Specification_txBx);
            this.Controls.Add(this.NextForm_btn);
            this.Controls.Add(this.SpecPath_btn);
            this.Controls.Add(this.Operation_gbx);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.List_txBx);
            this.Controls.Add(this.ListPath_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainFormView";
            this.Opacity = 0.95D;
            this.Load += new System.EventHandler(this.MainFormView_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.Operation_gbx.ResumeLayout(false);
            this.Operation_gbx.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ListPath_btn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox Operation_gbx;
        private System.Windows.Forms.RadioButton validation_rb;
        private System.Windows.Forms.RadioButton complVerification_rb;
        private System.Windows.Forms.RadioButton createSpec_rb;
        private System.Windows.Forms.Button SpecPath_btn;
        public System.Windows.Forms.TextBox Specification_txBx;
        public System.Windows.Forms.TextBox List_txBx;
        private System.Windows.Forms.Button NextForm_btn;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
    }
}

