namespace AvailablePC
{
    partial class FunctionWindow
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label_Response = new System.Windows.Forms.Label();
            this.button_PaperCollect = new System.Windows.Forms.Button();
            this.button_Register = new System.Windows.Forms.Button();
            this.button_TeacherManage = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label_Response);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button_PaperCollect);
            this.splitContainer1.Panel2.Controls.Add(this.button_Register);
            this.splitContainer1.Panel2.Controls.Add(this.button_TeacherManage);
            this.splitContainer1.Size = new System.Drawing.Size(445, 454);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(0, 24);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(163, 439);
            this.listBox1.TabIndex = 7;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // label_Response
            // 
            this.label_Response.AutoSize = true;
            this.label_Response.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_Response.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Response.Location = new System.Drawing.Point(0, 0);
            this.label_Response.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Response.Name = "label_Response";
            this.label_Response.Size = new System.Drawing.Size(130, 24);
            this.label_Response.TabIndex = 6;
            this.label_Response.Text = "待响应请求";
            // 
            // button_PaperCollect
            // 
            this.button_PaperCollect.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_PaperCollect.Location = new System.Drawing.Point(0, 300);
            this.button_PaperCollect.Margin = new System.Windows.Forms.Padding(4);
            this.button_PaperCollect.Name = "button_PaperCollect";
            this.button_PaperCollect.Size = new System.Drawing.Size(277, 150);
            this.button_PaperCollect.TabIndex = 4;
            this.button_PaperCollect.Text = "试卷归档";
            this.button_PaperCollect.UseVisualStyleBackColor = true;
            this.button_PaperCollect.Click += new System.EventHandler(this.button_PaperCollect_Click);
            // 
            // button_Register
            // 
            this.button_Register.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Register.Location = new System.Drawing.Point(0, 150);
            this.button_Register.Margin = new System.Windows.Forms.Padding(4);
            this.button_Register.Name = "button_Register";
            this.button_Register.Size = new System.Drawing.Size(277, 150);
            this.button_Register.TabIndex = 3;
            this.button_Register.Text = "统一账号注册";
            this.button_Register.UseVisualStyleBackColor = true;
            this.button_Register.Click += new System.EventHandler(this.button_Register_Click);
            // 
            // button_TeacherManage
            // 
            this.button_TeacherManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_TeacherManage.Location = new System.Drawing.Point(0, 0);
            this.button_TeacherManage.Margin = new System.Windows.Forms.Padding(4);
            this.button_TeacherManage.Name = "button_TeacherManage";
            this.button_TeacherManage.Size = new System.Drawing.Size(277, 150);
            this.button_TeacherManage.TabIndex = 2;
            this.button_TeacherManage.Text = "手工信息管理";
            this.button_TeacherManage.UseVisualStyleBackColor = true;
            this.button_TeacherManage.Click += new System.EventHandler(this.button_ManualManage_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "选择需要归档的扫描信息";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // FunctionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 454);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FunctionWindow";
            this.Text = "FunctionWindow";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_Register;
        private System.Windows.Forms.Button button_TeacherManage;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label_Response;
        private System.Windows.Forms.Button button_PaperCollect;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}