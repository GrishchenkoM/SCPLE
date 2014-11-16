namespace Scple.View
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateSpecificationView));
            this.SpecDesignation_gbx = new System.Windows.Forms.GroupBox();
            this.DesignDoc_maskedTB = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ListOfitems_chkBx = new System.Windows.Forms.CheckBox();
            this.AssemblyDrawing_chkBx = new System.Windows.Forms.CheckBox();
            this.ElectricalCircuit_chkBx = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DesignDoc_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DesignationPcb_gBx = new System.Windows.Forms.GroupBox();
            this.DesignPcb_maskedTB = new System.Windows.Forms.MaskedTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Pcb_chkBx = new System.Windows.Forms.CheckBox();
            this.CertifyingSheet_chkBx = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DesignPcb_TB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ElementsOfSMDMounting_chkBx = new System.Windows.Forms.CheckBox();
            this.BorrowedItems_chkBx = new System.Windows.Forms.CheckBox();
            this.CreationFile_gBx = new System.Windows.Forms.GroupBox();
            this.FileXls_rb = new System.Windows.Forms.RadioButton();
            this.FileDoc_rb = new System.Windows.Forms.RadioButton();
            this.Start_btn = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gb_SourcePosition = new System.Windows.Forms.GroupBox();
            this.SourcePosition_maskedTB = new System.Windows.Forms.MaskedTextBox();
            this.XlsSettings_gBx = new System.Windows.Forms.GroupBox();
            this.RatingPlusName_chkBx = new System.Windows.Forms.CheckBox();
            this.FirstPage_chkBx = new System.Windows.Forms.CheckBox();
            this.Hat_chkBx = new System.Windows.Forms.CheckBox();
            this.SpecDesignation_gbx.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.DesignationPcb_gBx.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.CreationFile_gBx.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gb_SourcePosition.SuspendLayout();
            this.XlsSettings_gBx.SuspendLayout();
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
            this.toolTip1.SetToolTip(this.ListOfitems_chkBx, "Перечень элементов");
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
            this.toolTip1.SetToolTip(this.AssemblyDrawing_chkBx, "Сборочный чертеж");
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
            this.toolTip1.SetToolTip(this.ElectricalCircuit_chkBx, "Схема электрическая");
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
            // DesignationPcb_gBx
            // 
            this.DesignationPcb_gBx.Controls.Add(this.DesignPcb_maskedTB);
            this.DesignationPcb_gBx.Controls.Add(this.groupBox3);
            this.DesignationPcb_gBx.Controls.Add(this.label2);
            this.DesignationPcb_gBx.Controls.Add(this.DesignPcb_TB);
            this.DesignationPcb_gBx.Controls.Add(this.label6);
            this.DesignationPcb_gBx.Location = new System.Drawing.Point(13, 119);
            this.DesignationPcb_gBx.Name = "DesignationPcb_gBx";
            this.DesignationPcb_gBx.Size = new System.Drawing.Size(325, 82);
            this.DesignationPcb_gBx.TabIndex = 0;
            this.DesignationPcb_gBx.TabStop = false;
            this.DesignationPcb_gBx.Text = "Обозначение платы";
            // 
            // DesignPcb_maskedTB
            // 
            this.DesignPcb_maskedTB.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.DesignPcb_maskedTB.Location = new System.Drawing.Point(62, 27);
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
            this.groupBox3.Size = new System.Drawing.Size(122, 64);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Другие документы";
            // 
            // Pcb_chkBx
            // 
            this.Pcb_chkBx.AutoSize = true;
            this.Pcb_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Pcb_chkBx.Location = new System.Drawing.Point(25, 16);
            this.Pcb_chkBx.Name = "Pcb_chkBx";
            this.Pcb_chkBx.Size = new System.Drawing.Size(71, 25);
            this.Pcb_chkBx.TabIndex = 1;
            this.Pcb_chkBx.Text = "Плата";
            this.toolTip1.SetToolTip(this.Pcb_chkBx, "Плата. Данные конструкции");
            this.Pcb_chkBx.UseVisualStyleBackColor = true;
            // 
            // CertifyingSheet_chkBx
            // 
            this.CertifyingSheet_chkBx.AutoSize = true;
            this.CertifyingSheet_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CertifyingSheet_chkBx.Location = new System.Drawing.Point(25, 36);
            this.CertifyingSheet_chkBx.Name = "CertifyingSheet_chkBx";
            this.CertifyingSheet_chkBx.Size = new System.Drawing.Size(49, 25);
            this.CertifyingSheet_chkBx.TabIndex = 1;
            this.CertifyingSheet_chkBx.Text = "УД";
            this.toolTip1.SetToolTip(this.CertifyingSheet_chkBx, "Удостоверяющий лист");
            this.CertifyingSheet_chkBx.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(129, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = ".";
            // 
            // DesignPcb_TB
            // 
            this.DesignPcb_TB.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DesignPcb_TB.Location = new System.Drawing.Point(142, 27);
            this.DesignPcb_TB.Name = "DesignPcb_TB";
            this.DesignPcb_TB.Size = new System.Drawing.Size(39, 33);
            this.DesignPcb_TB.TabIndex = 1;
            this.DesignPcb_TB.Text = "888";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(6, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 23);
            this.label6.TabIndex = 1;
            this.label6.Text = "ААОТ.";
            // 
            // ElementsOfSMDMounting_chkBx
            // 
            this.ElementsOfSMDMounting_chkBx.AutoSize = true;
            this.ElementsOfSMDMounting_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ElementsOfSMDMounting_chkBx.Location = new System.Drawing.Point(13, 243);
            this.ElementsOfSMDMounting_chkBx.Name = "ElementsOfSMDMounting_chkBx";
            this.ElementsOfSMDMounting_chkBx.Size = new System.Drawing.Size(207, 25);
            this.ElementsOfSMDMounting_chkBx.TabIndex = 1;
            this.ElementsOfSMDMounting_chkBx.Text = "Элементы SMD монтажа";
            this.toolTip1.SetToolTip(this.ElementsOfSMDMounting_chkBx, "Определение элементов SMD монтажа");
            this.ElementsOfSMDMounting_chkBx.UseVisualStyleBackColor = true;
            // 
            // BorrowedItems_chkBx
            // 
            this.BorrowedItems_chkBx.AutoSize = true;
            this.BorrowedItems_chkBx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BorrowedItems_chkBx.Location = new System.Drawing.Point(13, 263);
            this.BorrowedItems_chkBx.Name = "BorrowedItems_chkBx";
            this.BorrowedItems_chkBx.Size = new System.Drawing.Size(212, 25);
            this.BorrowedItems_chkBx.TabIndex = 1;
            this.BorrowedItems_chkBx.Text = "Заимствованные изделия";
            this.toolTip1.SetToolTip(this.BorrowedItems_chkBx, "Наличие заимствованных изделий");
            this.BorrowedItems_chkBx.UseVisualStyleBackColor = true;
            // 
            // CreationFile_gBx
            // 
            this.CreationFile_gBx.Controls.Add(this.FileXls_rb);
            this.CreationFile_gBx.Controls.Add(this.FileDoc_rb);
            this.CreationFile_gBx.Location = new System.Drawing.Point(345, 13);
            this.CreationFile_gBx.Name = "CreationFile_gBx";
            this.CreationFile_gBx.Size = new System.Drawing.Size(94, 100);
            this.CreationFile_gBx.TabIndex = 2;
            this.CreationFile_gBx.TabStop = false;
            this.CreationFile_gBx.Text = "Создание файла";
            // 
            // FileXls_rb
            // 
            this.FileXls_rb.AutoSize = true;
            this.FileXls_rb.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FileXls_rb.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FileXls_rb.Location = new System.Drawing.Point(14, 60);
            this.FileXls_rb.Name = "FileXls_rb";
            this.FileXls_rb.Size = new System.Drawing.Size(57, 25);
            this.FileXls_rb.TabIndex = 8;
            this.FileXls_rb.TabStop = true;
            this.FileXls_rb.Text = ".XLS";
            this.FileXls_rb.UseVisualStyleBackColor = true;
            this.FileXls_rb.CheckedChanged += new System.EventHandler(this.FileXls_rb_CheckedChanged);
            // 
            // FileDoc_rb
            // 
            this.FileDoc_rb.AutoSize = true;
            this.FileDoc_rb.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FileDoc_rb.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FileDoc_rb.Location = new System.Drawing.Point(14, 35);
            this.FileDoc_rb.Name = "FileDoc_rb";
            this.FileDoc_rb.Size = new System.Drawing.Size(64, 25);
            this.FileDoc_rb.TabIndex = 8;
            this.FileDoc_rb.TabStop = true;
            this.FileDoc_rb.Text = ".DOC";
            this.FileDoc_rb.UseVisualStyleBackColor = true;
            this.FileDoc_rb.CheckedChanged += new System.EventHandler(this.FileDoc_rb_CheckedChanged);
            // 
            // Start_btn
            // 
            this.Start_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Start_btn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Start_btn.Location = new System.Drawing.Point(272, 259);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(79, 23);
            this.Start_btn.TabIndex = 6;
            this.Start_btn.Text = "Пуск!";
            this.toolTip1.SetToolTip(this.Start_btn, "Начать создание спецификации");
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // Cancel
            // 
            this.Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Cancel.Location = new System.Drawing.Point(358, 259);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(79, 23);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Отмена";
            this.toolTip1.SetToolTip(this.Cancel, "Вернуться на предыдущий шаг");
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
            // gb_SourcePosition
            // 
            this.gb_SourcePosition.Controls.Add(this.SourcePosition_maskedTB);
            this.gb_SourcePosition.Location = new System.Drawing.Point(345, 119);
            this.gb_SourcePosition.Name = "gb_SourcePosition";
            this.gb_SourcePosition.Size = new System.Drawing.Size(94, 82);
            this.gb_SourcePosition.TabIndex = 8;
            this.gb_SourcePosition.TabStop = false;
            this.gb_SourcePosition.Text = "Начальная позиция";
            this.toolTip1.SetToolTip(this.gb_SourcePosition, "Начальная позиция прочих изделий");
            // 
            // SourcePosition_maskedTB
            // 
            this.SourcePosition_maskedTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SourcePosition_maskedTB.Location = new System.Drawing.Point(30, 36);
            this.SourcePosition_maskedTB.Mask = "00";
            this.SourcePosition_maskedTB.Name = "SourcePosition_maskedTB";
            this.SourcePosition_maskedTB.Size = new System.Drawing.Size(32, 31);
            this.SourcePosition_maskedTB.TabIndex = 1;
            this.SourcePosition_maskedTB.Text = "20";
            this.toolTip1.SetToolTip(this.SourcePosition_maskedTB, "Начальная позиция прочих изделий");
            // 
            // XlsSettings_gBx
            // 
            this.XlsSettings_gBx.Controls.Add(this.RatingPlusName_chkBx);
            this.XlsSettings_gBx.Controls.Add(this.FirstPage_chkBx);
            this.XlsSettings_gBx.Controls.Add(this.Hat_chkBx);
            this.XlsSettings_gBx.Location = new System.Drawing.Point(13, 207);
            this.XlsSettings_gBx.Name = "XlsSettings_gBx";
            this.XlsSettings_gBx.Size = new System.Drawing.Size(426, 35);
            this.XlsSettings_gBx.TabIndex = 9;
            this.XlsSettings_gBx.TabStop = false;
            // 
            // RatingPlusName_chkBx
            // 
            this.RatingPlusName_chkBx.AutoSize = true;
            this.RatingPlusName_chkBx.Location = new System.Drawing.Point(229, 12);
            this.RatingPlusName_chkBx.Name = "RatingPlusName_chkBx";
            this.RatingPlusName_chkBx.Size = new System.Drawing.Size(191, 17);
            this.RatingPlusName_chkBx.TabIndex = 2;
            this.RatingPlusName_chkBx.Text = "Номинал + название элемента";
            this.RatingPlusName_chkBx.UseVisualStyleBackColor = true;
            // 
            // FirstPage_chkBx
            // 
            this.FirstPage_chkBx.AutoSize = true;
            this.FirstPage_chkBx.Location = new System.Drawing.Point(124, 12);
            this.FirstPage_chkBx.Name = "FirstPage_chkBx";
            this.FirstPage_chkBx.Size = new System.Drawing.Size(65, 17);
            this.FirstPage_chkBx.TabIndex = 1;
            this.FirstPage_chkBx.Text = "1й лист";
            this.FirstPage_chkBx.UseVisualStyleBackColor = true;
            // 
            // Hat_chkBx
            // 
            this.Hat_chkBx.AutoSize = true;
            this.Hat_chkBx.Location = new System.Drawing.Point(10, 12);
            this.Hat_chkBx.Name = "Hat_chkBx";
            this.Hat_chkBx.Size = new System.Drawing.Size(62, 17);
            this.Hat_chkBx.TabIndex = 0;
            this.Hat_chkBx.Text = "Шапка";
            this.Hat_chkBx.UseVisualStyleBackColor = true;
            // 
            // CreateSpecificationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(451, 312);
            this.Controls.Add(this.XlsSettings_gBx);
            this.Controls.Add(this.gb_SourcePosition);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Start_btn);
            this.Controls.Add(this.CreationFile_gBx);
            this.Controls.Add(this.DesignationPcb_gBx);
            this.Controls.Add(this.BorrowedItems_chkBx);
            this.Controls.Add(this.ElementsOfSMDMounting_chkBx);
            this.Controls.Add(this.SpecDesignation_gbx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(461, 344);
            this.MinimumSize = new System.Drawing.Size(461, 344);
            this.Name = "CreateSpecificationView";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spec-Creator:  Параметры";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateSpecificationView_FormClosing);
            this.SpecDesignation_gbx.ResumeLayout(false);
            this.SpecDesignation_gbx.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DesignationPcb_gBx.ResumeLayout(false);
            this.DesignationPcb_gBx.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.CreationFile_gBx.ResumeLayout(false);
            this.CreationFile_gBx.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gb_SourcePosition.ResumeLayout(false);
            this.gb_SourcePosition.PerformLayout();
            this.XlsSettings_gBx.ResumeLayout(false);
            this.XlsSettings_gBx.PerformLayout();
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
        private System.Windows.Forms.GroupBox DesignationPcb_gBx;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox Pcb_chkBx;
        private System.Windows.Forms.CheckBox CertifyingSheet_chkBx;
        public System.Windows.Forms.TextBox DesignPcb_TB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ElementsOfSMDMounting_chkBx;
        private System.Windows.Forms.CheckBox BorrowedItems_chkBx;
        private System.Windows.Forms.GroupBox CreationFile_gBx;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Button Cancel;
        public System.Windows.Forms.MaskedTextBox DesignDoc_maskedTB;
        public System.Windows.Forms.MaskedTextBox DesignPcb_maskedTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton FileDoc_rb;
        private System.Windows.Forms.RadioButton FileXls_rb;
        private System.Windows.Forms.GroupBox gb_SourcePosition;
        private System.Windows.Forms.MaskedTextBox SourcePosition_maskedTB;
        private System.Windows.Forms.GroupBox XlsSettings_gBx;
        private System.Windows.Forms.CheckBox RatingPlusName_chkBx;
        private System.Windows.Forms.CheckBox FirstPage_chkBx;
        private System.Windows.Forms.CheckBox Hat_chkBx;

    }
}