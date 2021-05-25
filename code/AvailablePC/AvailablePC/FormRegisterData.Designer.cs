namespace AvailablePC
{
    partial class FormRegisterData
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataSetExcel = new System.Data.DataSet();
            this.buttonChooseExcel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.DataSource = this.dataSetExcel;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1067, 562);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataSetExcel
            // 
            this.dataSetExcel.DataSetName = "NewDataSet";
            // 
            // buttonChooseExcel
            // 
            this.buttonChooseExcel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonChooseExcel.Font = new System.Drawing.Font("宋体", 18F);
            this.buttonChooseExcel.Location = new System.Drawing.Point(0, 500);
            this.buttonChooseExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonChooseExcel.Name = "buttonChooseExcel";
            this.buttonChooseExcel.Size = new System.Drawing.Size(1067, 62);
            this.buttonChooseExcel.TabIndex = 1;
            this.buttonChooseExcel.Text = "选择Excel源文件";
            this.buttonChooseExcel.UseVisualStyleBackColor = true;
            this.buttonChooseExcel.Click += new System.EventHandler(this.buttonChooseExcel_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            this.openFileDialog.InitialDirectory = "Environment.GetFolderPath(Environment.SpecialFolder.Desktop)";
            // 
            // FormRegisterData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.buttonChooseExcel);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormRegisterData";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetExcel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Data.DataSet dataSetExcel;
        private System.Windows.Forms.Button buttonChooseExcel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}