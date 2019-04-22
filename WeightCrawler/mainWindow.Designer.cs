namespace WeightCrawler
{
    partial class WeightCrawler
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
            this.lbl_domainFile = new System.Windows.Forms.Label();
            this.tb_file = new System.Windows.Forms.TextBox();
            this.btn_selectFile = new System.Windows.Forms.Button();
            this.lv_domains = new System.Windows.Forms.ListView();
            this.col_domain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_weight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_domain = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lv_stats = new System.Windows.Forms.ListView();
            this.col_weightValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_weightCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_thread = new System.Windows.Forms.Label();
            this.tb_thread = new System.Windows.Forms.TextBox();
            this.combo_api = new System.Windows.Forms.ComboBox();
            this.lbl_api = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.progress_total = new System.Windows.Forms.ProgressBar();
            this.tb_count = new System.Windows.Forms.TextBox();
            this.btn_export = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rb_output = new System.Windows.Forms.RichTextBox();
            this.btn_import = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_domainFile
            // 
            this.lbl_domainFile.AutoSize = true;
            this.lbl_domainFile.Location = new System.Drawing.Point(12, 13);
            this.lbl_domainFile.Name = "lbl_domainFile";
            this.lbl_domainFile.Size = new System.Drawing.Size(53, 12);
            this.lbl_domainFile.TabIndex = 0;
            this.lbl_domainFile.Text = "导入文件";
            // 
            // tb_file
            // 
            this.tb_file.Location = new System.Drawing.Point(12, 28);
            this.tb_file.Name = "tb_file";
            this.tb_file.Size = new System.Drawing.Size(268, 21);
            this.tb_file.TabIndex = 1;
            // 
            // btn_selectFile
            // 
            this.btn_selectFile.Location = new System.Drawing.Point(283, 28);
            this.btn_selectFile.Name = "btn_selectFile";
            this.btn_selectFile.Size = new System.Drawing.Size(25, 22);
            this.btn_selectFile.TabIndex = 2;
            this.btn_selectFile.Text = "..";
            this.btn_selectFile.UseVisualStyleBackColor = true;
            this.btn_selectFile.Click += new System.EventHandler(this.Btn_selectFile_Click);
            // 
            // lv_domains
            // 
            this.lv_domains.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_domain,
            this.col_weight});
            this.lv_domains.GridLines = true;
            this.lv_domains.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_domains.Location = new System.Drawing.Point(12, 71);
            this.lv_domains.Name = "lv_domains";
            this.lv_domains.Size = new System.Drawing.Size(200, 330);
            this.lv_domains.TabIndex = 3;
            this.lv_domains.UseCompatibleStateImageBehavior = false;
            this.lv_domains.View = System.Windows.Forms.View.Details;
            // 
            // col_domain
            // 
            this.col_domain.Text = "域名";
            this.col_domain.Width = 120;
            // 
            // col_weight
            // 
            this.col_weight.Text = "权重";
            this.col_weight.Width = 50;
            // 
            // lbl_domain
            // 
            this.lbl_domain.AutoSize = true;
            this.lbl_domain.Location = new System.Drawing.Point(12, 56);
            this.lbl_domain.Name = "lbl_domain";
            this.lbl_domain.Size = new System.Drawing.Size(29, 12);
            this.lbl_domain.TabIndex = 4;
            this.lbl_domain.Text = "域名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "权重统计";
            // 
            // lv_stats
            // 
            this.lv_stats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_weightValue,
            this.col_weightCount});
            this.lv_stats.GridLines = true;
            this.lv_stats.Location = new System.Drawing.Point(229, 72);
            this.lv_stats.Name = "lv_stats";
            this.lv_stats.Scrollable = false;
            this.lv_stats.Size = new System.Drawing.Size(160, 329);
            this.lv_stats.TabIndex = 6;
            this.lv_stats.UseCompatibleStateImageBehavior = false;
            this.lv_stats.View = System.Windows.Forms.View.Details;
            // 
            // col_weightValue
            // 
            this.col_weightValue.Text = "权重";
            // 
            // col_weightCount
            // 
            this.col_weightCount.Text = "数量";
            this.col_weightCount.Width = 100;
            // 
            // lbl_thread
            // 
            this.lbl_thread.AutoSize = true;
            this.lbl_thread.Location = new System.Drawing.Point(393, 13);
            this.lbl_thread.Name = "lbl_thread";
            this.lbl_thread.Size = new System.Drawing.Size(41, 12);
            this.lbl_thread.TabIndex = 0;
            this.lbl_thread.Text = "线程：";
            // 
            // tb_thread
            // 
            this.tb_thread.Location = new System.Drawing.Point(395, 28);
            this.tb_thread.Name = "tb_thread";
            this.tb_thread.Size = new System.Drawing.Size(59, 21);
            this.tb_thread.TabIndex = 1;
            // 
            // combo_api
            // 
            this.combo_api.FormattingEnabled = true;
            this.combo_api.Items.AddRange(new object[] {
            "自动",
            "斗笠数据",
            "老李API"});
            this.combo_api.Location = new System.Drawing.Point(460, 29);
            this.combo_api.Name = "combo_api";
            this.combo_api.Size = new System.Drawing.Size(162, 20);
            this.combo_api.TabIndex = 4;
            // 
            // lbl_api
            // 
            this.lbl_api.AutoSize = true;
            this.lbl_api.Location = new System.Drawing.Point(458, 13);
            this.lbl_api.Name = "lbl_api";
            this.lbl_api.Size = new System.Drawing.Size(41, 12);
            this.lbl_api.TabIndex = 5;
            this.lbl_api.Text = "接口：";
            // 
            // btn_start
            // 
            this.btn_start.Enabled = false;
            this.btn_start.Location = new System.Drawing.Point(395, 71);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 7;
            this.btn_start.Text = "开始";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.Btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Enabled = false;
            this.btn_stop.Location = new System.Drawing.Point(476, 71);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 8;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.Btn_stop_Click);
            // 
            // progress_total
            // 
            this.progress_total.Location = new System.Drawing.Point(12, 407);
            this.progress_total.Name = "progress_total";
            this.progress_total.Size = new System.Drawing.Size(610, 23);
            this.progress_total.TabIndex = 9;
            // 
            // tb_count
            // 
            this.tb_count.Location = new System.Drawing.Point(557, 71);
            this.tb_count.Name = "tb_count";
            this.tb_count.ReadOnly = true;
            this.tb_count.Size = new System.Drawing.Size(65, 21);
            this.tb_count.TabIndex = 10;
            // 
            // btn_export
            // 
            this.btn_export.Enabled = false;
            this.btn_export.Location = new System.Drawing.Point(395, 100);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(75, 23);
            this.btn_export.TabIndex = 11;
            this.btn_export.Text = "导出结果";
            this.btn_export.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "输出";
            // 
            // rb_output
            // 
            this.rb_output.Location = new System.Drawing.Point(397, 146);
            this.rb_output.Name = "rb_output";
            this.rb_output.ReadOnly = true;
            this.rb_output.Size = new System.Drawing.Size(225, 255);
            this.rb_output.TabIndex = 14;
            this.rb_output.Text = "";
            // 
            // btn_import
            // 
            this.btn_import.Enabled = false;
            this.btn_import.Location = new System.Drawing.Point(314, 28);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(75, 23);
            this.btn_import.TabIndex = 15;
            this.btn_import.Text = "导入";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.Btn_import_Click);
            // 
            // WeightCrawler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 438);
            this.Controls.Add(this.btn_import);
            this.Controls.Add(this.rb_output);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_export);
            this.Controls.Add(this.tb_count);
            this.Controls.Add(this.progress_total);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.combo_api);
            this.Controls.Add(this.lbl_api);
            this.Controls.Add(this.lv_stats);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_thread);
            this.Controls.Add(this.lbl_domain);
            this.Controls.Add(this.lbl_thread);
            this.Controls.Add(this.lv_domains);
            this.Controls.Add(this.btn_selectFile);
            this.Controls.Add(this.tb_file);
            this.Controls.Add(this.lbl_domainFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WeightCrawler";
            this.Text = "WeightCrawler";
            this.Load += new System.EventHandler(this.WeightCrawler_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_domainFile;
        private System.Windows.Forms.TextBox tb_file;
        private System.Windows.Forms.Button btn_selectFile;
        private System.Windows.Forms.ListView lv_domains;
        private System.Windows.Forms.ColumnHeader col_domain;
        private System.Windows.Forms.ColumnHeader col_weight;
        private System.Windows.Forms.Label lbl_domain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lv_stats;
        private System.Windows.Forms.ColumnHeader col_weightValue;
        private System.Windows.Forms.ColumnHeader col_weightCount;
        private System.Windows.Forms.Label lbl_thread;
        private System.Windows.Forms.TextBox tb_thread;
        private System.Windows.Forms.ComboBox combo_api;
        private System.Windows.Forms.Label lbl_api;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.ProgressBar progress_total;
        private System.Windows.Forms.TextBox tb_count;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rb_output;
        private System.Windows.Forms.Button btn_import;
    }
}

