namespace DallasMicrofController
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиТермометраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOMПортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.разрешениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.битToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.битToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.битToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.информацияОТермометреToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.автозапускToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьВАвтозапускToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьИзАвтозапускаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиСервераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.имяСервераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.стартToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.запускатьАвтоматическиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.установитьСоединениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиТермометраToolStripMenuItem,
            this.настройкиСервераToolStripMenuItem,
            this.установитьСоединениеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(544, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиТермометраToolStripMenuItem
            // 
            this.настройкиТермометраToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cOMПортToolStripMenuItem,
            this.разрешениеToolStripMenuItem,
            this.информацияОТермометреToolStripMenuItem,
            this.toolStripSeparator2,
            this.автозапускToolStripMenuItem});
            this.настройкиТермометраToolStripMenuItem.Name = "настройкиТермометраToolStripMenuItem";
            this.настройкиТермометраToolStripMenuItem.Size = new System.Drawing.Size(149, 20);
            this.настройкиТермометраToolStripMenuItem.Text = "Настройки термометра";
            // 
            // cOMПортToolStripMenuItem
            // 
            this.cOMПортToolStripMenuItem.Name = "cOMПортToolStripMenuItem";
            this.cOMПортToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.cOMПортToolStripMenuItem.Text = "COM порт";
            // 
            // разрешениеToolStripMenuItem
            // 
            this.разрешениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.битToolStripMenuItem,
            this.битToolStripMenuItem1,
            this.битToolStripMenuItem2});
            this.разрешениеToolStripMenuItem.Name = "разрешениеToolStripMenuItem";
            this.разрешениеToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.разрешениеToolStripMenuItem.Text = "Разрешение";
            // 
            // битToolStripMenuItem
            // 
            this.битToolStripMenuItem.Name = "битToolStripMenuItem";
            this.битToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.битToolStripMenuItem.Text = "9 Бит";
            this.битToolStripMenuItem.Click += new System.EventHandler(this.SetResol);
            // 
            // битToolStripMenuItem1
            // 
            this.битToolStripMenuItem1.Name = "битToolStripMenuItem1";
            this.битToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
            this.битToolStripMenuItem1.Text = "10 Бит";
            this.битToolStripMenuItem1.Click += new System.EventHandler(this.SetResol);
            // 
            // битToolStripMenuItem2
            // 
            this.битToolStripMenuItem2.Name = "битToolStripMenuItem2";
            this.битToolStripMenuItem2.Size = new System.Drawing.Size(108, 22);
            this.битToolStripMenuItem2.Text = "11 Бит";
            this.битToolStripMenuItem2.Click += new System.EventHandler(this.SetResol);
            // 
            // информацияОТермометреToolStripMenuItem
            // 
            this.информацияОТермометреToolStripMenuItem.Name = "информацияОТермометреToolStripMenuItem";
            this.информацияОТермометреToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.информацияОТермометреToolStripMenuItem.Text = "Информация о термометре";
            this.информацияОТермометреToolStripMenuItem.Click += new System.EventHandler(this.информацияОТермометреToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
            // 
            // автозапускToolStripMenuItem
            // 
            this.автозапускToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьВАвтозапускToolStripMenuItem,
            this.удалитьИзАвтозапускаToolStripMenuItem});
            this.автозапускToolStripMenuItem.Name = "автозапускToolStripMenuItem";
            this.автозапускToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.автозапускToolStripMenuItem.Text = "Автозапуск";
            // 
            // добавитьВАвтозапускToolStripMenuItem
            // 
            this.добавитьВАвтозапускToolStripMenuItem.Name = "добавитьВАвтозапускToolStripMenuItem";
            this.добавитьВАвтозапускToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.добавитьВАвтозапускToolStripMenuItem.Text = "Добавить в автозапуск";
            this.добавитьВАвтозапускToolStripMenuItem.Click += new System.EventHandler(this.добавитьВАвтозапускToolStripMenuItem_Click);
            // 
            // удалитьИзАвтозапускаToolStripMenuItem
            // 
            this.удалитьИзАвтозапускаToolStripMenuItem.Name = "удалитьИзАвтозапускаToolStripMenuItem";
            this.удалитьИзАвтозапускаToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.удалитьИзАвтозапускаToolStripMenuItem.Text = "Удалить из автозапуска";
            this.удалитьИзАвтозапускаToolStripMenuItem.Click += new System.EventHandler(this.удалитьИзАвтозапускаToolStripMenuItem_Click);
            // 
            // настройкиСервераToolStripMenuItem
            // 
            this.настройкиСервераToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.имяСервераToolStripMenuItem,
            this.стартToolStripMenuItem,
            this.toolStripSeparator1,
            this.запускатьАвтоматическиToolStripMenuItem});
            this.настройкиСервераToolStripMenuItem.Name = "настройкиСервераToolStripMenuItem";
            this.настройкиСервераToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
            this.настройкиСервераToolStripMenuItem.Text = "Настройки сервера";
            // 
            // имяСервераToolStripMenuItem
            // 
            this.имяСервераToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.имяСервераToolStripMenuItem.Name = "имяСервераToolStripMenuItem";
            this.имяСервераToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.имяСервераToolStripMenuItem.Text = "Имя сервера";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // стартToolStripMenuItem
            // 
            this.стартToolStripMenuItem.Name = "стартToolStripMenuItem";
            this.стартToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.стартToolStripMenuItem.Text = "Старт";
            this.стартToolStripMenuItem.Click += new System.EventHandler(this.стартToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // запускатьАвтоматическиToolStripMenuItem
            // 
            this.запускатьАвтоматическиToolStripMenuItem.Name = "запускатьАвтоматическиToolStripMenuItem";
            this.запускатьАвтоматическиToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.запускатьАвтоматическиToolStripMenuItem.Text = "Запускать автоматически";
            this.запускатьАвтоматическиToolStripMenuItem.Click += new System.EventHandler(this.запускатьАвтоматическиToolStripMenuItem_Click);
            // 
            // установитьСоединениеToolStripMenuItem
            // 
            this.установитьСоединениеToolStripMenuItem.Name = "установитьСоединениеToolStripMenuItem";
            this.установитьСоединениеToolStripMenuItem.Size = new System.Drawing.Size(149, 20);
            this.установитьСоединениеToolStripMenuItem.Text = "Установить соединение";
            this.установитьСоединениеToolStripMenuItem.Click += new System.EventHandler(this.установитьСоединениеToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(544, 107);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Статус сервера:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Сервер не запущен";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Локальный IP:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(439, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "255.255.255.255";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "DallasMicrofController";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 151);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(560, 189);
            this.Name = "Form1";
            this.Text = "DallasMicrofController - Сервер";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиТермометраToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиСервераToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOMПортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem разрешениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem битToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem битToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem битToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem установитьСоединениеToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem информацияОТермометреToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem стартToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem имяСервераToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem запускатьАвтоматическиToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem автозапускToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьВАвтозапускToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьИзАвтозапускаToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

