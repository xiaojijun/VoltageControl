namespace VoltageControl
{
    partial class MainWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_CleanLog = new System.Windows.Forms.Button();
            this.button_serialsetting = new System.Windows.Forms.Button();
            this.comboBox_checkbits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_stopbits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_databits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_baudrate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_com = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.terminator = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView_channel = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.channel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voltage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operation = new System.Windows.Forms.DataGridViewButtonColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于本软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectAllMemu = new System.Windows.Forms.ToolStripMenuItem();
            this.合并发送ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置发送间隔ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delay_time = new System.Windows.Forms.ToolStripTextBox();
            this.SetDelayTime = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_channel)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(924, 497);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Location = new System.Drawing.Point(548, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(376, 243);
            this.panel4.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.button_CleanLog);
            this.groupBox3.Controls.Add(this.button_serialsetting);
            this.groupBox3.Controls.Add(this.comboBox_checkbits);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.comboBox_stopbits);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.comboBox_databits);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.comboBox_baudrate);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.comboBox_com);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(367, 232);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "串口";
            // 
            // button_CleanLog
            // 
            this.button_CleanLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_CleanLog.Location = new System.Drawing.Point(292, 209);
            this.button_CleanLog.Name = "button_CleanLog";
            this.button_CleanLog.Size = new System.Drawing.Size(75, 23);
            this.button_CleanLog.TabIndex = 1;
            this.button_CleanLog.Text = "清空终端输出";
            this.button_CleanLog.UseVisualStyleBackColor = true;
            this.button_CleanLog.Click += new System.EventHandler(this.button_CleanLog_Click);
            // 
            // button_serialsetting
            // 
            this.button_serialsetting.BackgroundImage = global::VoltageControl.Properties.Resources.swith_grey;
            this.button_serialsetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_serialsetting.FlatAppearance.BorderSize = 0;
            this.button_serialsetting.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button_serialsetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button_serialsetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_serialsetting.Location = new System.Drawing.Point(35, 49);
            this.button_serialsetting.Name = "button_serialsetting";
            this.button_serialsetting.Size = new System.Drawing.Size(120, 121);
            this.button_serialsetting.TabIndex = 4;
            this.button_serialsetting.Text = "打开串口";
            this.button_serialsetting.UseVisualStyleBackColor = true;
            this.button_serialsetting.Click += new System.EventHandler(this.button_serialsetting_Click);
            // 
            // comboBox_checkbits
            // 
            this.comboBox_checkbits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_checkbits.FormattingEnabled = true;
            this.comboBox_checkbits.Location = new System.Drawing.Point(225, 150);
            this.comboBox_checkbits.Name = "comboBox_checkbits";
            this.comboBox_checkbits.Size = new System.Drawing.Size(97, 20);
            this.comboBox_checkbits.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "校验位";
            // 
            // comboBox_stopbits
            // 
            this.comboBox_stopbits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_stopbits.FormattingEnabled = true;
            this.comboBox_stopbits.Location = new System.Drawing.Point(225, 124);
            this.comboBox_stopbits.Name = "comboBox_stopbits";
            this.comboBox_stopbits.Size = new System.Drawing.Size(97, 20);
            this.comboBox_stopbits.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(178, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "停止位";
            // 
            // comboBox_databits
            // 
            this.comboBox_databits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_databits.FormattingEnabled = true;
            this.comboBox_databits.Location = new System.Drawing.Point(225, 98);
            this.comboBox_databits.Name = "comboBox_databits";
            this.comboBox_databits.Size = new System.Drawing.Size(97, 20);
            this.comboBox_databits.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据位";
            // 
            // comboBox_baudrate
            // 
            this.comboBox_baudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_baudrate.FormattingEnabled = true;
            this.comboBox_baudrate.Location = new System.Drawing.Point(225, 72);
            this.comboBox_baudrate.Name = "comboBox_baudrate";
            this.comboBox_baudrate.Size = new System.Drawing.Size(97, 20);
            this.comboBox_baudrate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率";
            // 
            // comboBox_com
            // 
            this.comboBox_com.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_com.FormattingEnabled = true;
            this.comboBox_com.Location = new System.Drawing.Point(225, 46);
            this.comboBox_com.Name = "comboBox_com";
            this.comboBox_com.Size = new System.Drawing.Size(97, 20);
            this.comboBox_com.TabIndex = 1;
            this.comboBox_com.DropDown += new System.EventHandler(this.RenewComList);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Location = new System.Drawing.Point(545, 249);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(379, 248);
            this.panel3.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.terminator);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(382, 251);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "终端输出";
            // 
            // terminator
            // 
            this.terminator.BackColor = System.Drawing.SystemColors.InfoText;
            this.terminator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.terminator.ForeColor = System.Drawing.SystemColors.Window;
            this.terminator.Location = new System.Drawing.Point(3, 17);
            this.terminator.Name = "terminator";
            this.terminator.Size = new System.Drawing.Size(376, 231);
            this.terminator.TabIndex = 0;
            this.terminator.Text = "";
            this.terminator.TextChanged += new System.EventHandler(this.terminator_TextChange);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dataGridView_channel);
            this.panel2.Controls.Add(this.menuStrip1);
            this.panel2.Location = new System.Drawing.Point(0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(545, 494);
            this.panel2.TabIndex = 4;
            // 
            // dataGridView_channel
            // 
            this.dataGridView_channel.AllowUserToAddRows = false;
            this.dataGridView_channel.AllowUserToDeleteRows = false;
            this.dataGridView_channel.AllowUserToResizeRows = false;
            this.dataGridView_channel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_channel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.channel,
            this.voltage,
            this.status,
            this.operation});
            this.dataGridView_channel.Location = new System.Drawing.Point(0, 27);
            this.dataGridView_channel.Name = "dataGridView_channel";
            this.dataGridView_channel.RowHeadersVisible = false;
            this.dataGridView_channel.RowTemplate.Height = 23;
            this.dataGridView_channel.Size = new System.Drawing.Size(548, 471);
            this.dataGridView_channel.TabIndex = 0;
            this.dataGridView_channel.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SendSingleChannel_button_click);
            this.dataGridView_channel.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellEndEdit_FormatNumber);
            this.dataGridView_channel.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.CellValidating);
            // 
            // Selected
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.NullValue = false;
            this.Selected.DefaultCellStyle = dataGridViewCellStyle1;
            this.Selected.Frozen = true;
            this.Selected.HeaderText = "选择";
            this.Selected.Name = "Selected";
            this.Selected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // channel
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.channel.DefaultCellStyle = dataGridViewCellStyle2;
            this.channel.Frozen = true;
            this.channel.HeaderText = "通道";
            this.channel.Name = "channel";
            this.channel.ReadOnly = true;
            // 
            // voltage
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.voltage.DefaultCellStyle = dataGridViewCellStyle3;
            this.voltage.Frozen = true;
            this.voltage.HeaderText = "电压(V)";
            this.voltage.Name = "voltage";
            this.voltage.ToolTipText = "请输入要发送的电压值";
            // 
            // status
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.status.DefaultCellStyle = dataGridViewCellStyle4;
            this.status.Frozen = true;
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // operation
            // 
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            this.operation.Name = "operation";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.SelectAllMemu,
            this.合并发送ToolStripMenuItem,
            this.重置列表ToolStripMenuItem,
            this.设置发送间隔ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(545, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于本软件ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 关于本软件ToolStripMenuItem
            // 
            this.关于本软件ToolStripMenuItem.Name = "关于本软件ToolStripMenuItem";
            this.关于本软件ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.关于本软件ToolStripMenuItem.Text = "关于本软件";
            this.关于本软件ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // SelectAllMemu
            // 
            this.SelectAllMemu.Name = "SelectAllMemu";
            this.SelectAllMemu.Size = new System.Drawing.Size(68, 21);
            this.SelectAllMemu.Text = "全部选中";
            this.SelectAllMemu.Click += new System.EventHandler(this.SelectAll_Click);
            // 
            // 合并发送ToolStripMenuItem
            // 
            this.合并发送ToolStripMenuItem.Name = "合并发送ToolStripMenuItem";
            this.合并发送ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.合并发送ToolStripMenuItem.Text = "发送选中";
            this.合并发送ToolStripMenuItem.Click += new System.EventHandler(this.SendMultiChannel_Click);
            // 
            // 重置列表ToolStripMenuItem
            // 
            this.重置列表ToolStripMenuItem.Name = "重置列表ToolStripMenuItem";
            this.重置列表ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.重置列表ToolStripMenuItem.Text = "重置列表";
            this.重置列表ToolStripMenuItem.Click += new System.EventHandler(this.ResetGridView_Click);
            // 
            // 设置发送间隔ToolStripMenuItem
            // 
            this.设置发送间隔ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delay_time,
            this.SetDelayTime});
            this.设置发送间隔ToolStripMenuItem.Name = "设置发送间隔ToolStripMenuItem";
            this.设置发送间隔ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.设置发送间隔ToolStripMenuItem.Text = "设置发送间隔";
            // 
            // delay_time
            // 
            this.delay_time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.delay_time.MaxLength = 5;
            this.delay_time.Name = "delay_time";
            this.delay_time.Size = new System.Drawing.Size(100, 23);
            this.delay_time.Text = "300";
            this.delay_time.ToolTipText = "毫秒";
            // 
            // SetDelayTime
            // 
            this.SetDelayTime.Name = "SetDelayTime";
            this.SetDelayTime.Size = new System.Drawing.Size(160, 22);
            this.SetDelayTime.Text = "设置（毫秒）";
            this.SetDelayTime.Click += new System.EventHandler(this.SetDelayTime_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 498);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "电压输出调节器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing_DeleteSource);
            this.Resize += new System.EventHandler(this.MainWindow_load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_channel)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_serialsetting;
        private System.Windows.Forms.ComboBox comboBox_checkbits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_stopbits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_databits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_baudrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_com;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView_channel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox terminator;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button_CleanLog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn channel;
        private System.Windows.Forms.DataGridViewTextBoxColumn voltage;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewButtonColumn operation;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 合并发送ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重置列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SelectAllMemu;
        private System.Windows.Forms.ToolStripMenuItem 关于本软件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置发送间隔ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox delay_time;
        private System.Windows.Forms.ToolStripMenuItem SetDelayTime;
    }
}

