namespace AvailablePC
{
    partial class FormQuery
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
            this.label_SutudentScore = new System.Windows.Forms.Label();
            this.textBox_StudentNumber = new System.Windows.Forms.TextBox();
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
            this.dateTimePicker_Begin = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown_StudentScore_Max = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_StudentScore_Min = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StudentScore_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StudentScore_Min)).BeginInit();
            this.SuspendLayout();
            // 
            // label_SutudentScore
            // 
            this.label_SutudentScore.AutoSize = true;
            this.label_SutudentScore.Location = new System.Drawing.Point(38, 116);
            this.label_SutudentScore.Name = "label_SutudentScore";
            this.label_SutudentScore.Size = new System.Drawing.Size(67, 15);
            this.label_SutudentScore.TabIndex = 54;
            this.label_SutudentScore.Text = "考生成绩";
            // 
            // textBox_StudentNumber
            // 
            this.textBox_StudentNumber.Location = new System.Drawing.Point(132, 71);
            this.textBox_StudentNumber.Name = "textBox_StudentNumber";
            this.textBox_StudentNumber.Size = new System.Drawing.Size(326, 25);
            this.textBox_StudentNumber.TabIndex = 53;
            // 
            // textBox_courseIndex
            // 
            this.textBox_courseIndex.Location = new System.Drawing.Point(388, 151);
            this.textBox_courseIndex.Name = "textBox_courseIndex";
            this.textBox_courseIndex.Size = new System.Drawing.Size(150, 25);
            this.textBox_courseIndex.TabIndex = 52;
            // 
            // label_courseIndex
            // 
            this.label_courseIndex.AutoSize = true;
            this.label_courseIndex.Location = new System.Drawing.Point(304, 156);
            this.label_courseIndex.Name = "label_courseIndex";
            this.label_courseIndex.Size = new System.Drawing.Size(52, 15);
            this.label_courseIndex.TabIndex = 51;
            this.label_courseIndex.Text = "课序号";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "正常",
            "补缓考"});
            this.comboBox1.Location = new System.Drawing.Point(132, 192);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 23);
            this.comboBox1.TabIndex = 50;
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Location = new System.Drawing.Point(38, 196);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(67, 15);
            this.label_type.TabIndex = 49;
            this.label_type.Text = "考试类型";
            // 
            // labelBuilding
            // 
            this.labelBuilding.AutoSize = true;
            this.labelBuilding.Location = new System.Drawing.Point(38, 36);
            this.labelBuilding.Name = "labelBuilding";
            this.labelBuilding.Size = new System.Drawing.Size(82, 15);
            this.labelBuilding.TabIndex = 48;
            this.labelBuilding.Text = "考场教学楼";
            // 
            // textBox_building
            // 
            this.textBox_building.Location = new System.Drawing.Point(132, 31);
            this.textBox_building.Name = "textBox_building";
            this.textBox_building.Size = new System.Drawing.Size(150, 25);
            this.textBox_building.TabIndex = 47;
            // 
            // button_OK
            // 
            this.button_OK.Font = new System.Drawing.Font("宋体", 18F);
            this.button_OK.Location = new System.Drawing.Point(41, 273);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(500, 63);
            this.button_OK.TabIndex = 46;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label_TestDate
            // 
            this.label_TestDate.AutoSize = true;
            this.label_TestDate.Location = new System.Drawing.Point(38, 236);
            this.label_TestDate.Name = "label_TestDate";
            this.label_TestDate.Size = new System.Drawing.Size(67, 15);
            this.label_TestDate.TabIndex = 45;
            this.label_TestDate.Text = "考试时间";
            // 
            // label_courseNumber
            // 
            this.label_courseNumber.AutoSize = true;
            this.label_courseNumber.Location = new System.Drawing.Point(38, 156);
            this.label_courseNumber.Name = "label_courseNumber";
            this.label_courseNumber.Size = new System.Drawing.Size(52, 15);
            this.label_courseNumber.TabIndex = 44;
            this.label_courseNumber.Text = "课程号";
            // 
            // label_StudentNumber
            // 
            this.label_StudentNumber.AutoSize = true;
            this.label_StudentNumber.Location = new System.Drawing.Point(38, 76);
            this.label_StudentNumber.Name = "label_StudentNumber";
            this.label_StudentNumber.Size = new System.Drawing.Size(67, 15);
            this.label_StudentNumber.TabIndex = 43;
            this.label_StudentNumber.Text = "考生学号";
            // 
            // label_roomNumber
            // 
            this.label_roomNumber.AutoSize = true;
            this.label_roomNumber.Location = new System.Drawing.Point(294, 36);
            this.label_roomNumber.Name = "label_roomNumber";
            this.label_roomNumber.Size = new System.Drawing.Size(82, 15);
            this.label_roomNumber.TabIndex = 42;
            this.label_roomNumber.Text = "考场房间号";
            // 
            // textBox_courseNumber
            // 
            this.textBox_courseNumber.Location = new System.Drawing.Point(132, 151);
            this.textBox_courseNumber.Name = "textBox_courseNumber";
            this.textBox_courseNumber.Size = new System.Drawing.Size(150, 25);
            this.textBox_courseNumber.TabIndex = 41;
            // 
            // textBox_roomNumber
            // 
            this.textBox_roomNumber.Location = new System.Drawing.Point(388, 31);
            this.textBox_roomNumber.Name = "textBox_roomNumber";
            this.textBox_roomNumber.Size = new System.Drawing.Size(150, 25);
            this.textBox_roomNumber.TabIndex = 40;
            // 
            // dateTimePicker_Begin
            // 
            this.dateTimePicker_Begin.Location = new System.Drawing.Point(183, 231);
            this.dateTimePicker_Begin.Name = "dateTimePicker_Begin";
            this.dateTimePicker_Begin.Size = new System.Drawing.Size(138, 25);
            this.dateTimePicker_Begin.TabIndex = 39;
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Location = new System.Drawing.Point(399, 231);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(138, 25);
            this.dateTimePicker_End.TabIndex = 56;
            // 
            // numericUpDown_StudentScore_Max
            // 
            this.numericUpDown_StudentScore_Max.Location = new System.Drawing.Point(417, 111);
            this.numericUpDown_StudentScore_Max.Name = "numericUpDown_StudentScore_Max";
            this.numericUpDown_StudentScore_Max.Size = new System.Drawing.Size(120, 25);
            this.numericUpDown_StudentScore_Max.TabIndex = 57;
            this.numericUpDown_StudentScore_Max.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 58;
            this.label1.Text = "起";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 15);
            this.label2.TabIndex = 59;
            this.label2.Text = "止";
            // 
            // numericUpDown_StudentScore_Min
            // 
            this.numericUpDown_StudentScore_Min.Location = new System.Drawing.Point(201, 111);
            this.numericUpDown_StudentScore_Min.Name = "numericUpDown_StudentScore_Min";
            this.numericUpDown_StudentScore_Min.Size = new System.Drawing.Size(120, 25);
            this.numericUpDown_StudentScore_Min.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(343, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 61;
            this.label3.Text = "不高于";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 62;
            this.label4.Text = "不低于";
            // 
            // FormQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 453);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown_StudentScore_Min);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_StudentScore_Max);
            this.Controls.Add(this.dateTimePicker_End);
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
            this.Controls.Add(this.dateTimePicker_Begin);
            this.Name = "FormQuery";
            this.Text = "FormQuery";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StudentScore_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StudentScore_Min)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_SutudentScore;
        private System.Windows.Forms.TextBox textBox_StudentNumber;
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
        private System.Windows.Forms.DateTimePicker dateTimePicker_Begin;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.NumericUpDown numericUpDown_StudentScore_Max;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_StudentScore_Min;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}