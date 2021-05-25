namespace AvailablePC
{
    partial class FormTestList
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnStudentNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTestRoom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCourseNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCourseIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnStudentNumber,
            this.columnHeaderScore,
            this.columnHeaderTestRoom,
            this.columnHeaderTime,
            this.columnHeaderCourseNumber,
            this.columnHeaderCourseIndex,
            this.columnHeaderType,
            this.columnHeaderURL});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(800, 450);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.active);
            // 
            // columnStudentNumber
            // 
            this.columnStudentNumber.Text = "学号";
            this.columnStudentNumber.Width = 165;
            // 
            // columnHeaderScore
            // 
            this.columnHeaderScore.Text = "成绩";
            // 
            // columnHeaderTestRoom
            // 
            this.columnHeaderTestRoom.Text = "考试地点";
            this.columnHeaderTestRoom.Width = 121;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "考试时间";
            this.columnHeaderTime.Width = 133;
            // 
            // columnHeaderCourseNumber
            // 
            this.columnHeaderCourseNumber.Text = "课程号";
            this.columnHeaderCourseNumber.Width = 118;
            // 
            // columnHeaderCourseIndex
            // 
            this.columnHeaderCourseIndex.Text = "课序号";
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "考试类型";
            this.columnHeaderType.Width = 95;
            // 
            // columnHeaderURL
            // 
            this.columnHeaderURL.Text = "试卷路径";
            this.columnHeaderURL.Width = 291;
            // 
            // FormTestList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Name = "FormTestList";
            this.Text = "FormList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnStudentNumber;
        private System.Windows.Forms.ColumnHeader columnHeaderScore;
        private System.Windows.Forms.ColumnHeader columnHeaderTestRoom;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.ColumnHeader columnHeaderCourseNumber;
        private System.Windows.Forms.ColumnHeader columnHeaderCourseIndex;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderURL;
    }
}