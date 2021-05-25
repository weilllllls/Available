namespace AvailablePC
{
    partial class FormManualManage
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_paper = new System.Windows.Forms.Button();
            this.button_teacher = new System.Windows.Forms.Button();
            this.button_preview = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_paper
            // 
            this.button_paper.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_paper.Location = new System.Drawing.Point(0, 0);
            this.button_paper.Margin = new System.Windows.Forms.Padding(4);
            this.button_paper.Name = "button_paper";
            this.button_paper.Size = new System.Drawing.Size(382, 75);
            this.button_paper.TabIndex = 4;
            this.button_paper.Text = "试卷信息管理";
            this.button_paper.UseVisualStyleBackColor = true;
            this.button_paper.Click += new System.EventHandler(this.button_paper_Click);
            // 
            // button_teacher
            // 
            this.button_teacher.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_teacher.Location = new System.Drawing.Point(0, 75);
            this.button_teacher.Margin = new System.Windows.Forms.Padding(4);
            this.button_teacher.Name = "button_teacher";
            this.button_teacher.Size = new System.Drawing.Size(382, 75);
            this.button_teacher.TabIndex = 5;
            this.button_teacher.Text = "教师信息管理";
            this.button_teacher.UseVisualStyleBackColor = true;
            this.button_teacher.Click += new System.EventHandler(this.button_teacher_Click);
            // 
            // button_preview
            // 
            this.button_preview.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_preview.Location = new System.Drawing.Point(0, 150);
            this.button_preview.Margin = new System.Windows.Forms.Padding(4);
            this.button_preview.Name = "button_preview";
            this.button_preview.Size = new System.Drawing.Size(382, 75);
            this.button_preview.TabIndex = 6;
            this.button_preview.Text = "返回上一级";
            this.button_preview.UseVisualStyleBackColor = true;
            this.button_preview.Click += new System.EventHandler(this.button_preview_Click);
            // 
            // FormManualManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 225);
            this.Controls.Add(this.button_preview);
            this.Controls.Add(this.button_teacher);
            this.Controls.Add(this.button_paper);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormManualManage";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_paper;
        private System.Windows.Forms.Button button_teacher;
        private System.Windows.Forms.Button button_preview;
    }
}

