namespace AvailablePC
{
    partial class FormQueryTeacher
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
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label_Name = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.textBox_Id = new System.Windows.Forms.TextBox();
            this.label_Id = new System.Windows.Forms.Label();
            this.button_New = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(151, 91);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(150, 25);
            this.textBox_Name.TabIndex = 49;
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(57, 96);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(52, 15);
            this.label_Name.TabIndex = 50;
            this.label_Name.Text = "教师名";
            // 
            // button_OK
            // 
            this.button_OK.Font = new System.Drawing.Font("宋体", 18F);
            this.button_OK.Location = new System.Drawing.Point(60, 146);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(241, 63);
            this.button_OK.TabIndex = 51;
            this.button_OK.Text = "查询";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // textBox_Id
            // 
            this.textBox_Id.Location = new System.Drawing.Point(151, 37);
            this.textBox_Id.Name = "textBox_Id";
            this.textBox_Id.Size = new System.Drawing.Size(150, 25);
            this.textBox_Id.TabIndex = 52;
            // 
            // label_Id
            // 
            this.label_Id.AutoSize = true;
            this.label_Id.Location = new System.Drawing.Point(57, 42);
            this.label_Id.Name = "label_Id";
            this.label_Id.Size = new System.Drawing.Size(67, 15);
            this.label_Id.TabIndex = 53;
            this.label_Id.Text = "教师工号";
            // 
            // button_New
            // 
            this.button_New.Font = new System.Drawing.Font("宋体", 18F);
            this.button_New.Location = new System.Drawing.Point(60, 227);
            this.button_New.Name = "button_New";
            this.button_New.Size = new System.Drawing.Size(241, 63);
            this.button_New.TabIndex = 54;
            this.button_New.Text = "新增";
            this.button_New.UseVisualStyleBackColor = true;
            this.button_New.Click += new System.EventHandler(this.button_New_Click);
            // 
            // FormQueryTeacher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 319);
            this.Controls.Add(this.button_New);
            this.Controls.Add(this.label_Id);
            this.Controls.Add(this.textBox_Id);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label_Name);
            this.Controls.Add(this.textBox_Name);
            this.Name = "FormQueryTeacher";
            this.Text = "FormQueryTeacher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.TextBox textBox_Id;
        private System.Windows.Forms.Label label_Id;
        private System.Windows.Forms.Button button_New;
    }
}