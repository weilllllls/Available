namespace AvailablePC
{
    partial class FormPaper
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
            this.textBox_courseIndex = new System.Windows.Forms.TextBox();
            this.label_courseIndex = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label_type = new System.Windows.Forms.Label();
            this.labelBuilding = new System.Windows.Forms.Label();
            this.textBox_building = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.label_TestDate = new System.Windows.Forms.Label();
            this.label_courseNumber = new System.Windows.Forms.Label();
            this.label_StudentNumber = new System.Windows.Forms.Label();
            this.label_roomNumber = new System.Windows.Forms.Label();
            this.textBox_courseNumber = new System.Windows.Forms.TextBox();
            this.textBox_roomNumber = new System.Windows.Forms.TextBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.textBox_StudentNumber = new System.Windows.Forms.TextBox();
            this.textBox_StudentScore = new System.Windows.Forms.TextBox();
            this.label_SutudentScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_courseIndex
            // 
            this.textBox_courseIndex.Location = new System.Drawing.Point(137, 231);
            this.textBox_courseIndex.Name = "textBox_courseIndex";
            this.textBox_courseIndex.Size = new System.Drawing.Size(326, 25);
            this.textBox_courseIndex.TabIndex = 34;
            // 
            // label_courseIndex
            // 
            this.label_courseIndex.AutoSize = true;
            this.label_courseIndex.Location = new System.Drawing.Point(49, 235);
            this.label_courseIndex.Name = "label_courseIndex";
            this.label_courseIndex.Size = new System.Drawing.Size(52, 15);
            this.label_courseIndex.TabIndex = 33;
            this.label_courseIndex.Text = "课序号";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "正常",
            "补缓考"});
            this.comboBox1.Location = new System.Drawing.Point(137, 271);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(326, 23);
            this.comboBox1.TabIndex = 32;
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Location = new System.Drawing.Point(49, 275);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(67, 15);
            this.label_type.TabIndex = 31;
            this.label_type.Text = "考试类型";
            // 
            // labelBuilding
            // 
            this.labelBuilding.AutoSize = true;
            this.labelBuilding.Location = new System.Drawing.Point(49, 75);
            this.labelBuilding.Name = "labelBuilding";
            this.labelBuilding.Size = new System.Drawing.Size(82, 15);
            this.labelBuilding.TabIndex = 30;
            this.labelBuilding.Text = "考场教学楼";
            // 
            // textBox_building
            // 
            this.textBox_building.Location = new System.Drawing.Point(137, 71);
            this.textBox_building.Name = "textBox_building";
            this.textBox_building.Size = new System.Drawing.Size(326, 25);
            this.textBox_building.TabIndex = 29;
            // 
            // button_OK
            // 
            this.button_OK.Font = new System.Drawing.Font("宋体", 18F);
            this.button_OK.Location = new System.Drawing.Point(52, 365);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(411, 63);
            this.button_OK.TabIndex = 27;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label_TestDate
            // 
            this.label_TestDate.AutoSize = true;
            this.label_TestDate.Location = new System.Drawing.Point(49, 315);
            this.label_TestDate.Name = "label_TestDate";
            this.label_TestDate.Size = new System.Drawing.Size(67, 15);
            this.label_TestDate.TabIndex = 26;
            this.label_TestDate.Text = "考试时间";
            // 
            // label_courseNumber
            // 
            this.label_courseNumber.AutoSize = true;
            this.label_courseNumber.Location = new System.Drawing.Point(49, 195);
            this.label_courseNumber.Name = "label_courseNumber";
            this.label_courseNumber.Size = new System.Drawing.Size(52, 15);
            this.label_courseNumber.TabIndex = 24;
            this.label_courseNumber.Text = "课程号";
            // 
            // label_StudentNumber
            // 
            this.label_StudentNumber.AutoSize = true;
            this.label_StudentNumber.Location = new System.Drawing.Point(49, 115);
            this.label_StudentNumber.Name = "label_StudentNumber";
            this.label_StudentNumber.Size = new System.Drawing.Size(67, 15);
            this.label_StudentNumber.TabIndex = 23;
            this.label_StudentNumber.Text = "考生学号";
            // 
            // label_roomNumber
            // 
            this.label_roomNumber.AutoSize = true;
            this.label_roomNumber.Location = new System.Drawing.Point(49, 35);
            this.label_roomNumber.Name = "label_roomNumber";
            this.label_roomNumber.Size = new System.Drawing.Size(82, 15);
            this.label_roomNumber.TabIndex = 22;
            this.label_roomNumber.Text = "考场房间号";
            // 
            // textBox_courseNumber
            // 
            this.textBox_courseNumber.Location = new System.Drawing.Point(137, 191);
            this.textBox_courseNumber.Name = "textBox_courseNumber";
            this.textBox_courseNumber.Size = new System.Drawing.Size(326, 25);
            this.textBox_courseNumber.TabIndex = 21;
            // 
            // textBox_roomNumber
            // 
            this.textBox_roomNumber.Location = new System.Drawing.Point(137, 31);
            this.textBox_roomNumber.Name = "textBox_roomNumber";
            this.textBox_roomNumber.Size = new System.Drawing.Size(326, 25);
            this.textBox_roomNumber.TabIndex = 20;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(137, 309);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(326, 25);
            this.dateTimePicker.TabIndex = 19;
            // 
            // textBox_StudentNumber
            // 
            this.textBox_StudentNumber.Location = new System.Drawing.Point(137, 111);
            this.textBox_StudentNumber.Name = "textBox_StudentNumber";
            this.textBox_StudentNumber.Size = new System.Drawing.Size(326, 25);
            this.textBox_StudentNumber.TabIndex = 35;
            // 
            // textBox_StudentScore
            // 
            this.textBox_StudentScore.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox_StudentScore.Location = new System.Drawing.Point(137, 151);
            this.textBox_StudentScore.Name = "textBox_StudentScore";
            this.textBox_StudentScore.Size = new System.Drawing.Size(326, 25);
            this.textBox_StudentScore.TabIndex = 38;
            // 
            // label_SutudentScore
            // 
            this.label_SutudentScore.AutoSize = true;
            this.label_SutudentScore.Location = new System.Drawing.Point(49, 155);
            this.label_SutudentScore.Name = "label_SutudentScore";
            this.label_SutudentScore.Size = new System.Drawing.Size(67, 15);
            this.label_SutudentScore.TabIndex = 36;
            this.label_SutudentScore.Text = "考生成绩";
            // 
            // FormPaper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 450);
            this.Controls.Add(this.textBox_StudentScore);
            this.Controls.Add(this.label_SutudentScore);
            this.Controls.Add(this.textBox_StudentNumber);
            this.Controls.Add(this.textBox_courseIndex);
            this.Controls.Add(this.label_courseIndex);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label_type);
            this.Controls.Add(this.labelBuilding);
            this.Controls.Add(this.textBox_building);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label_TestDate);
            this.Controls.Add(this.label_courseNumber);
            this.Controls.Add(this.label_StudentNumber);
            this.Controls.Add(this.label_roomNumber);
            this.Controls.Add(this.textBox_courseNumber);
            this.Controls.Add(this.textBox_roomNumber);
            this.Controls.Add(this.dateTimePicker);
            this.Name = "FormPaper";
            this.Text = "FormPaper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_courseIndex;
        private System.Windows.Forms.Label label_courseIndex;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.Label labelBuilding;
        private System.Windows.Forms.TextBox textBox_building;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label_TestDate;
        private System.Windows.Forms.Label label_courseNumber;
        private System.Windows.Forms.Label label_StudentNumber;
        private System.Windows.Forms.Label label_roomNumber;
        private System.Windows.Forms.TextBox textBox_courseNumber;
        private System.Windows.Forms.TextBox textBox_roomNumber;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.TextBox textBox_StudentNumber;
        private System.Windows.Forms.TextBox textBox_StudentScore;
        private System.Windows.Forms.Label label_SutudentScore;
    }
}