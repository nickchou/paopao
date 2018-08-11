namespace PaoPao
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblCopyMsg = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.numDownMs = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numDownThread = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.lbMsg = new System.Windows.Forms.Label();
            this.txtIds = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbRes = new System.Windows.Forms.ListBox();
            this.btnGO = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numDownMs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDownThread)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCopyMsg
            // 
            this.lblCopyMsg.AutoSize = true;
            this.lblCopyMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCopyMsg.Location = new System.Drawing.Point(525, 448);
            this.lblCopyMsg.Name = "lblCopyMsg";
            this.lblCopyMsg.Size = new System.Drawing.Size(53, 12);
            this.lblCopyMsg.TabIndex = 39;
            this.lblCopyMsg.Text = "复制结果";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(690, 443);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(99, 23);
            this.btnOpenFile.TabIndex = 38;
            this.btnOpenFile.Text = "打开日志文件";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // numDownMs
            // 
            this.numDownMs.Location = new System.Drawing.Point(724, 12);
            this.numDownMs.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numDownMs.Name = "numDownMs";
            this.numDownMs.Size = new System.Drawing.Size(64, 21);
            this.numDownMs.TabIndex = 37;
            this.numDownMs.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(658, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "Sleep(ms)：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(524, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "线程数量：";
            // 
            // numDownThread
            // 
            this.numDownThread.Location = new System.Drawing.Point(589, 12);
            this.numDownThread.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numDownThread.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDownThread.Name = "numDownThread";
            this.numDownThread.Size = new System.Drawing.Size(56, 21);
            this.numDownThread.TabIndex = 34;
            this.numDownThread.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "URL：";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(47, 12);
            this.txtURL.Multiline = true;
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(452, 55);
            this.txtURL.TabIndex = 32;
            this.txtURL.Text = "http://www.baidu.com?id={0}&name={2}&age={1}&class={3}";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(590, 443);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(94, 23);
            this.btnCopy.TabIndex = 31;
            this.btnCopy.Text = "复制执行结果";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // lbMsg
            // 
            this.lbMsg.AutoSize = true;
            this.lbMsg.Location = new System.Drawing.Point(247, 448);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(35, 12);
            this.lbMsg.TabIndex = 30;
            this.lbMsg.Text = "lbMsg";
            // 
            // txtIds
            // 
            this.txtIds.AllowDrop = true;
            this.txtIds.Location = new System.Drawing.Point(12, 98);
            this.txtIds.MaxLength = 2147483647;
            this.txtIds.Multiline = true;
            this.txtIds.Name = "txtIds";
            this.txtIds.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIds.Size = new System.Drawing.Size(217, 340);
            this.txtIds.TabIndex = 29;
            this.txtIds.Text = "1,2,3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "执行结果：";
            // 
            // lbRes
            // 
            this.lbRes.AllowDrop = true;
            this.lbRes.FormattingEnabled = true;
            this.lbRes.HorizontalScrollbar = true;
            this.lbRes.ItemHeight = 12;
            this.lbRes.Location = new System.Drawing.Point(249, 98);
            this.lbRes.Name = "lbRes";
            this.lbRes.Size = new System.Drawing.Size(543, 340);
            this.lbRes.TabIndex = 27;
            // 
            // btnGO
            // 
            this.btnGO.Location = new System.Drawing.Point(526, 44);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(75, 23);
            this.btnGO.TabIndex = 26;
            this.btnGO.Text = "开始";
            this.btnGO.UseVisualStyleBackColor = true;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "参数（一行一批次，多参数用\",\"分割）";
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(620, 44);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 40;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(712, 44);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 41;
            this.btnAbort.Text = "终止";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 471);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lblCopyMsg);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.numDownMs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numDownThread);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.lbMsg);
            this.Controls.Add(this.txtIds);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbRes);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "跑跑数据 v1.2";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDownMs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDownThread)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCopyMsg;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.NumericUpDown numDownMs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numDownThread;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label lbMsg;
        private System.Windows.Forms.TextBox txtIds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbRes;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnAbort;
    }
}

