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
            this.настройкиСервераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.имяСервераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.стартToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.установитьСоединениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
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
            this.информацияОТермометреToolStripMenuItem});
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
            // настройкиСервераToolStripMenuItem
            // 
            this.настройкиСервераToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.имяСервераToolStripMenuItem,
            this.стартToolStripMenuItem});
            this.настройкиСервераToolStripMenuItem.Name = "настройкиСервераToolStripMenuItem";
            this.настройкиСервераToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
            this.настройкиСервераToolStripMenuItem.Text = "Настройки сервера";
            // 
            // имяСервераToolStripMenuItem
            // 
            this.имяСервераToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.имяСервераToolStripMenuItem.Name = "имяСервераToolStripMenuItem";
            this.имяСервераToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            this.стартToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.стартToolStripMenuItem.Text = "Старт";
            this.стартToolStripMenuItem.Click += new System.EventHandler(this.стартToolStripMenuItem_Click);
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
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(544, 107);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 131);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DallasMicrofController - Сервер";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}

