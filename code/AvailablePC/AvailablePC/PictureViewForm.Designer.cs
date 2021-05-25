

namespace AvailablePC
{
    partial class PictureViewForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_Preview = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Next = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Bigger = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Smaller = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Preview,
            this.ToolStripMenuItem_Next,
            this.ToolStripMenuItem_Bigger,
            this.ToolStripMenuItem_Smaller});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_Preview
            // 
            this.ToolStripMenuItem_Preview.Name = "ToolStripMenuItem_Preview";
            this.ToolStripMenuItem_Preview.Size = new System.Drawing.Size(68, 24);
            this.ToolStripMenuItem_Preview.Text = "上一页";
            this.ToolStripMenuItem_Preview.Click += new System.EventHandler(this.ToolStripMenuItem_Preview_Click);
            // 
            // ToolStripMenuItem_Next
            // 
            this.ToolStripMenuItem_Next.Name = "ToolStripMenuItem_Next";
            this.ToolStripMenuItem_Next.Size = new System.Drawing.Size(68, 24);
            this.ToolStripMenuItem_Next.Text = "下一页";
            this.ToolStripMenuItem_Next.Click += new System.EventHandler(this.ToolStripMenuItem_Next_Click);
            // 
            // ToolStripMenuItem_Bigger
            // 
            this.ToolStripMenuItem_Bigger.Name = "ToolStripMenuItem_Bigger";
            this.ToolStripMenuItem_Bigger.Size = new System.Drawing.Size(53, 24);
            this.ToolStripMenuItem_Bigger.Text = "放大";
            this.ToolStripMenuItem_Bigger.Click += new System.EventHandler(this.ToolStripMenuItem_Bigger_Click);
            // 
            // ToolStripMenuItem_Smaller
            // 
            this.ToolStripMenuItem_Smaller.Name = "ToolStripMenuItem_Smaller";
            this.ToolStripMenuItem_Smaller.Size = new System.Drawing.Size(53, 24);
            this.ToolStripMenuItem_Smaller.Text = "缩小";
            this.ToolStripMenuItem_Smaller.Click += new System.EventHandler(this.ToolStripMenuItem_Smaller_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonYes);
            this.splitContainer1.Panel1.Controls.Add(this.buttonNo);
            this.splitContainer1.Panel1.Controls.Add(this.textBox);
            this.splitContainer1.Panel1MinSize = 5;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 482);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 2;
            // 
            // buttonYes
            // 
            this.buttonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonYes.Location = new System.Drawing.Point(623, 40);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(75, 30);
            this.buttonYes.TabIndex = 2;
            this.buttonYes.Text = "确定";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // buttonNo
            // 
            this.buttonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNo.Location = new System.Drawing.Point(713, 40);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(75, 30);
            this.buttonNo.TabIndex = 1;
            this.buttonNo.Text = "否决";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("宋体", 20F);
            this.textBox.Location = new System.Drawing.Point(0, 30);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(617, 50);
            this.textBox.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 378);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox1_PreviewKeyDown);
            // 
            // PictureViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PictureViewForm";
            this.Text = "PictureViewForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.PictureViewForm_Scroll);
            this.SizeChanged += new System.EventHandler(this.PictureViewForm_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /*private O2S.Components.PDFView4NET.PDFPageView pdfPageView1;
        private O2S.Components.PDFView4NET.PDFDocument pdfDocument1;*/
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Preview;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Next;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Bigger;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Smaller;
        private System.Windows.Forms.SplitContainer splitContainer1;
        //private System.Windows.Forms.TextBox richTextBox1;
        //private System.Windows.Forms.TextBox textBox;
        
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}