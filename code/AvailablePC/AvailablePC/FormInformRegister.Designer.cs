namespace AvailablePC
{
    partial class FormInformRegister
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
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.textBox_roomNumber = new System.Windows.Forms.TextBox();
            this.textBox_courseNumber = new System.Windows.Forms.TextBox();
            this.label_roomNumber = new System.Windows.Forms.Label();
            this.label_StudentInfo = new System.Windows.Forms.Label();
            this.label_courseNumber = new System.Windows.Forms.Label();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.label_TestDate = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.label_excelChoosen = new System.Windows.Forms.Label();
            this.labelBuilding = new System.Windows.Forms.Label();
            this.textBox_building = new System.Windows.Forms.TextBox();
            this.label_type = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label_courseIndex = new System.Windows.Forms.Label();
            this.textBox_courseIndex = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(127, 261);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(326, 25);
            this.dateTimePicker.TabIndex = 0;
            // 
            // textBox_roomNumber
            // 
            this.textBox_roomNumber.Location = new System.Drawing.Point(127, 21);
            this.textBox_roomNumber.Name = "textBox_roomNumber";
            this.textBox_roomNumber.Size = new System.Drawing.Size(326, 25);
            this.textBox_roomNumber.TabIndex = 1;
            // 
            // textBox_courseNumber
            // 
            this.textBox_courseNumber.Location = new System.Drawing.Point(127, 153);
            this.textBox_courseNumber.Name = "textBox_courseNumber";
            this.textBox_courseNumber.Size = new System.Drawing.Size(326, 25);
            this.textBox_courseNumber.TabIndex = 2;
            // 
            // label_roomNumber
            // 
            this.label_roomNumber.AutoSize = true;
            this.label_roomNumber.Location = new System.Drawing.Point(39, 24);
            this.label_roomNumber.Name = "label_roomNumber";
            this.label_roomNumber.Size = new System.Drawing.Size(82, 15);
            this.label_roomNumber.TabIndex = 3;
            this.label_roomNumber.Text = "考场房间号";
            // 
            // label_StudentInfo
            // 
            this.label_StudentInfo.AutoSize = true;
            this.label_StudentInfo.Location = new System.Drawing.Point(39, 112);
            this.label_StudentInfo.Name = "label_StudentInfo";
            this.label_StudentInfo.Size = new System.Drawing.Size(67, 15);
            this.label_StudentInfo.TabIndex = 4;
            this.label_StudentInfo.Text = "考生信息";
            // 
            // label_courseNumber
            // 
            this.label_courseNumber.AutoSize = true;
            this.label_courseNumber.Location = new System.Drawing.Point(39, 156);
            this.label_courseNumber.Name = "label_courseNumber";
            this.label_courseNumber.Size = new System.Drawing.Size(52, 15);
            this.label_courseNumber.TabIndex = 6;
            this.label_courseNumber.Text = "课程号";
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(127, 112);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(100, 23);
            this.buttonChoose.TabIndex = 7;
            this.buttonChoose.Text = "选择Excel";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // label_TestDate
            // 
            this.label_TestDate.AutoSize = true;
            this.label_TestDate.Location = new System.Drawing.Point(39, 268);
            this.label_TestDate.Name = "label_TestDate";
            this.label_TestDate.Size = new System.Drawing.Size(67, 15);
            this.label_TestDate.TabIndex = 8;
            this.label_TestDate.Text = "考试时间";
            // 
            // button_OK
            // 
            this.button_OK.Font = new System.Drawing.Font("宋体", 18F);
            this.button_OK.Location = new System.Drawing.Point(42, 316);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(411, 63);
            this.button_OK.TabIndex = 9;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label_excelChoosen
            // 
            this.label_excelChoosen.AutoSize = true;
            this.label_excelChoosen.Location = new System.Drawing.Point(218, 116);
            this.label_excelChoosen.Name = "label_excelChoosen";
            this.label_excelChoosen.Size = new System.Drawing.Size(0, 15);
            this.label_excelChoosen.TabIndex = 10;
            // 
            // labelBuilding
            // 
            this.labelBuilding.AutoSize = true;
            this.labelBuilding.Location = new System.Drawing.Point(39, 68);
            this.labelBuilding.Name = "labelBuilding";
            this.labelBuilding.Size = new System.Drawing.Size(82, 15);
            this.labelBuilding.TabIndex = 12;
            this.labelBuilding.Text = "考场教学楼";
            // 
            // textBox_building
            // 
            this.textBox_building.Location = new System.Drawing.Point(127, 63);
            this.textBox_building.Name = "textBox_building";
            this.textBox_building.Size = new System.Drawing.Size(326, 25);
            this.textBox_building.TabIndex = 11;
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Location = new System.Drawing.Point(39, 229);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(67, 15);
            this.label_type.TabIndex = 15;
            this.label_type.Text = "考试类型";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "正常",
            "补缓考"});
            this.comboBox1.Location = new System.Drawing.Point(127, 223);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(326, 23);
            this.comboBox1.TabIndex = 16;
            // 
            // label_courseIndex
            // 
            this.label_courseIndex.AutoSize = true;
            this.label_courseIndex.Location = new System.Drawing.Point(39, 190);
            this.label_courseIndex.Name = "label_courseIndex";
            this.label_courseIndex.Size = new System.Drawing.Size(52, 15);
            this.label_courseIndex.TabIndex = 17;
            this.label_courseIndex.Text = "课序号";
            // 
            // textBox_courseIndex
            // 
            this.textBox_courseIndex.Location = new System.Drawing.Point(127, 187);
            this.textBox_courseIndex.Name = "textBox_courseIndex";
            this.textBox_courseIndex.Size = new System.Drawing.Size(326, 25);
            this.textBox_courseIndex.TabIndex = 18;
            // 
            // FormInformRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 411);
            this.Controls.Add(this.textBox_courseIndex);
            this.Controls.Add(this.label_courseIndex);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label_type);
            this.Controls.Add(this.labelBuilding);
            this.Controls.Add(this.textBox_building);
            this.Controls.Add(this.label_excelChoosen);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label_TestDate);
            this.Controls.Add(this.buttonChoose);
            this.Controls.Add(this.label_courseNumber);
            this.Controls.Add(this.label_StudentInfo);
            this.Controls.Add(this.label_roomNumber);
            this.Controls.Add(this.textBox_courseNumber);
            this.Controls.Add(this.textBox_roomNumber);
            this.Controls.Add(this.dateTimePicker);
            this.Name = "FormInformRegister";
            this.Text = " ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.TextBox textBox_roomNumber;
        private System.Windows.Forms.TextBox textBox_courseNumber;
        private System.Windows.Forms.Label label_roomNumber;
        private System.Windows.Forms.Label label_StudentInfo;
        private System.Windows.Forms.Label label_courseNumber;
        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Label label_TestDate;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label_excelChoosen;
        private System.Windows.Forms.Label labelBuilding;
        private System.Windows.Forms.TextBox textBox_building;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label_courseIndex;
        private System.Windows.Forms.TextBox textBox_courseIndex;
    }
}