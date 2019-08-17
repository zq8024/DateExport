namespace DataExport.Demo
{
    partial class DataExportDemo
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
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectTemplate = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtExportFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExportFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrderId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGenerateData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTemplate
            // 
            this.txtTemplate.Location = new System.Drawing.Point(111, 12);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.ReadOnly = true;
            this.txtTemplate.Size = new System.Drawing.Size(288, 21);
            this.txtTemplate.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "Template:";
            // 
            // btnSelectTemplate
            // 
            this.btnSelectTemplate.Location = new System.Drawing.Point(405, 10);
            this.btnSelectTemplate.Name = "btnSelectTemplate";
            this.btnSelectTemplate.Size = new System.Drawing.Size(112, 23);
            this.btnSelectTemplate.TabIndex = 24;
            this.btnSelectTemplate.Text = "Select Template";
            this.btnSelectTemplate.UseVisualStyleBackColor = true;
            this.btnSelectTemplate.Click += new System.EventHandler(this.btnSelectTemplate_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(275, 70);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(112, 23);
            this.btnExport.TabIndex = 27;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtExportFileName
            // 
            this.txtExportFileName.Location = new System.Drawing.Point(111, 99);
            this.txtExportFileName.Name = "txtExportFileName";
            this.txtExportFileName.ReadOnly = true;
            this.txtExportFileName.Size = new System.Drawing.Size(288, 21);
            this.txtExportFileName.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "Export File Name:";
            // 
            // txtExportFilePath
            // 
            this.txtExportFilePath.Location = new System.Drawing.Point(111, 126);
            this.txtExportFilePath.Name = "txtExportFilePath";
            this.txtExportFilePath.ReadOnly = true;
            this.txtExportFilePath.Size = new System.Drawing.Size(288, 21);
            this.txtExportFilePath.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-2, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 31;
            this.label2.Text = "Export File Path:";
            // 
            // txtOrderId
            // 
            this.txtOrderId.Location = new System.Drawing.Point(111, 41);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.Size = new System.Drawing.Size(288, 21);
            this.txtOrderId.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "Order ID:";
            // 
            // btnGenerateData
            // 
            this.btnGenerateData.Location = new System.Drawing.Point(128, 70);
            this.btnGenerateData.Name = "btnGenerateData";
            this.btnGenerateData.Size = new System.Drawing.Size(112, 23);
            this.btnGenerateData.TabIndex = 34;
            this.btnGenerateData.Text = "Generate Data";
            this.btnGenerateData.UseVisualStyleBackColor = true;
            this.btnGenerateData.Click += new System.EventHandler(this.btnGenerateData_Click);
            // 
            // DataExportDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 162);
            this.Controls.Add(this.btnGenerateData);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOrderId);
            this.Controls.Add(this.txtExportFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtExportFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.txtTemplate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSelectTemplate);
            this.Name = "DataExportDemo";
            this.Text = "Data Export Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTemplate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectTemplate;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtExportFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExportFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrderId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGenerateData;
    }
}

