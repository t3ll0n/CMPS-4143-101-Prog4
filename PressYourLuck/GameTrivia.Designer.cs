namespace Program4_PressYourLuck
{
    partial class GameTrivia
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
            this.Questions = new System.Windows.Forms.RichTextBox();
            this.Trivia = new System.Windows.Forms.Button();
            this.Answers = new System.Windows.Forms.TextBox();
            this.Submit = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.Result = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Questions
            // 
            this.Questions.Location = new System.Drawing.Point(60, 56);
            this.Questions.Name = "Questions";
            this.Questions.ReadOnly = true;
            this.Questions.Size = new System.Drawing.Size(260, 71);
            this.Questions.TabIndex = 0;
            this.Questions.Text = "";
            // 
            // Trivia
            // 
            this.Trivia.Location = new System.Drawing.Point(49, 149);
            this.Trivia.Name = "Trivia";
            this.Trivia.Size = new System.Drawing.Size(83, 20);
            this.Trivia.TabIndex = 1;
            this.Trivia.TabStop = false;
            this.Trivia.Text = "Begin Trivia";
            this.Trivia.UseVisualStyleBackColor = true;
            this.Trivia.Click += new System.EventHandler(this.Trivia_Click);
            // 
            // Answers
            // 
            this.Answers.Location = new System.Drawing.Point(177, 149);
            this.Answers.Name = "Answers";
            this.Answers.Size = new System.Drawing.Size(182, 20);
            this.Answers.TabIndex = 2;
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(34, 189);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(123, 60);
            this.Submit.TabIndex = 3;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // next
            // 
            this.next.Location = new System.Drawing.Point(221, 226);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(88, 23);
            this.next.TabIndex = 4;
            this.next.Text = "Next Question";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // Result
            // 
            this.Result.Font = new System.Drawing.Font("Stencil", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Result.ForeColor = System.Drawing.Color.LawnGreen;
            this.Result.Location = new System.Drawing.Point(60, 8);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(260, 45);
            this.Result.TabIndex = 6;
            // 
            // GameTrivia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(407, 261);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.next);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.Answers);
            this.Controls.Add(this.Trivia);
            this.Controls.Add(this.Questions);
            this.Name = "GameTrivia";
            this.Text = "GameQuery";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Questions;
        private System.Windows.Forms.Button Trivia;
        private System.Windows.Forms.TextBox Answers;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Label Result;

    }
}