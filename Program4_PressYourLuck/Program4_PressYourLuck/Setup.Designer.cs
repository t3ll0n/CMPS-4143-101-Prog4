namespace Program4_PressYourLuck
{
    partial class Setup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.choosefile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numplayer = new System.Windows.Forms.ComboBox();
            this.okbutton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.choosefile);
            this.groupBox1.Location = new System.Drawing.Point(40, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Setup";
            // 
            // choosefile
            // 
            this.choosefile.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choosefile.Location = new System.Drawing.Point(59, 39);
            this.choosefile.Name = "choosefile";
            this.choosefile.Size = new System.Drawing.Size(87, 25);
            this.choosefile.TabIndex = 1;
            this.choosefile.Text = "Choose File";
            this.choosefile.UseVisualStyleBackColor = true;
            this.choosefile.Click += new System.EventHandler(this.choosefile_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numplayer);
            this.groupBox2.Location = new System.Drawing.Point(40, 166);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 61);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Players";
            // 
            // numplayer
            // 
            this.numplayer.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numplayer.FormattingEnabled = true;
            this.numplayer.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.numplayer.Location = new System.Drawing.Point(31, 19);
            this.numplayer.Name = "numplayer";
            this.numplayer.Size = new System.Drawing.Size(151, 23);
            this.numplayer.TabIndex = 2;
            this.numplayer.SelectedValueChanged += new System.EventHandler(this.numplayer_SelectedValueChanged);
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(111, 247);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 2;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 282);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Setup";
            this.Text = "Setup";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button choosefile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox numplayer;
        private System.Windows.Forms.Button okbutton;
    }
}