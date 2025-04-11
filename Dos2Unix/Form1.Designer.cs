namespace Dos2Unix
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.listView1 = new System.Windows.Forms.ListView();
			this.c0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.c1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.c2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.c3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button_do = new System.Windows.Forms.Button();
			this.button_browser = new System.Windows.Forms.Button();
			this.button_help = new System.Windows.Forms.Button();
			this.button_nolist = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.textBox_status = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// folderBrowserDialog1
			// 
			resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
			// 
			// listView1
			// 
			resources.ApplyResources(this.listView1, "listView1");
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c0,
            this.c1,
            this.c2,
            this.c3});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Name = "listView1";
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			// 
			// c0
			// 
			resources.ApplyResources(this.c0, "c0");
			// 
			// c1
			// 
			resources.ApplyResources(this.c1, "c1");
			// 
			// c2
			// 
			resources.ApplyResources(this.c2, "c2");
			// 
			// c3
			// 
			resources.ApplyResources(this.c3, "c3");
			// 
			// button_do
			// 
			resources.ApplyResources(this.button_do, "button_do");
			this.button_do.Name = "button_do";
			this.button_do.UseVisualStyleBackColor = true;
			this.button_do.Click += new System.EventHandler(this.button_do_Click);
			// 
			// button_browser
			// 
			resources.ApplyResources(this.button_browser, "button_browser");
			this.button_browser.Name = "button_browser";
			this.button_browser.UseVisualStyleBackColor = true;
			this.button_browser.Click += new System.EventHandler(this.button_browser_Click);
			// 
			// button_help
			// 
			resources.ApplyResources(this.button_help, "button_help");
			this.button_help.Name = "button_help";
			this.button_help.UseVisualStyleBackColor = true;
			this.button_help.Click += new System.EventHandler(this.button_help_Click);
			// 
			// button_nolist
			// 
			resources.ApplyResources(this.button_nolist, "button_nolist");
			this.button_nolist.Name = "button_nolist";
			this.button_nolist.UseVisualStyleBackColor = true;
			this.button_nolist.Click += new System.EventHandler(this.button_nolist_Click);
			// 
			// button_cancel
			// 
			resources.ApplyResources(this.button_cancel, "button_cancel");
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// textBox_status
			// 
			resources.ApplyResources(this.textBox_status, "textBox_status");
			this.textBox_status.BackColor = System.Drawing.SystemColors.Control;
			this.textBox_status.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox_status.Name = "textBox_status";
			this.textBox_status.ReadOnly = true;
			this.textBox_status.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 500;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.textBox_status);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_nolist);
			this.Controls.Add(this.button_help);
			this.Controls.Add(this.button_browser);
			this.Controls.Add(this.button_do);
			this.Controls.Add(this.listView1);
			this.Name = "Form1";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader c0;
        private System.Windows.Forms.ColumnHeader c1;
        private System.Windows.Forms.ColumnHeader c2;
        private System.Windows.Forms.ColumnHeader c3;
		private System.Windows.Forms.Button button_do;
        private System.Windows.Forms.Button button_browser;
        private System.Windows.Forms.Button button_help;
		private System.Windows.Forms.Button button_nolist;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.TextBox textBox_status;
		private System.Windows.Forms.Timer timer1;
	}
}

