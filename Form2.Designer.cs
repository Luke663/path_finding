
namespace PathFinding
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.start_button = new System.Windows.Forms.Button();
            this.finish_button = new System.Windows.Forms.Button();
            this.wall_button = new System.Windows.Forms.Button();
            this.investigated_button = new System.Windows.Forms.Button();
            this.open_button = new System.Windows.Forms.Button();
            this.path_button = new System.Windows.Forms.Button();
            this.finish_label = new System.Windows.Forms.Label();
            this.start_label = new System.Windows.Forms.Label();
            this.wall_label = new System.Windows.Forms.Label();
            this.investigated_label = new System.Windows.Forms.Label();
            this.opened_label = new System.Windows.Forms.Label();
            this.path_label = new System.Windows.Forms.Label();
            this.key_label = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // start_button
            // 
            this.start_button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.start_button.Enabled = false;
            this.start_button.Location = new System.Drawing.Point(31, 300);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(33, 23);
            this.start_button.TabIndex = 0;
            this.start_button.Text = "S";
            this.start_button.UseVisualStyleBackColor = false;
            // 
            // finish_button
            // 
            this.finish_button.BackColor = System.Drawing.Color.IndianRed;
            this.finish_button.Enabled = false;
            this.finish_button.Location = new System.Drawing.Point(31, 320);
            this.finish_button.Name = "finish_button";
            this.finish_button.Size = new System.Drawing.Size(33, 23);
            this.finish_button.TabIndex = 1;
            this.finish_button.Text = "F";
            this.finish_button.UseVisualStyleBackColor = false;
            // 
            // wall_button
            // 
            this.wall_button.BackColor = System.Drawing.Color.DarkGray;
            this.wall_button.Enabled = false;
            this.wall_button.Location = new System.Drawing.Point(31, 340);
            this.wall_button.Name = "wall_button";
            this.wall_button.Size = new System.Drawing.Size(33, 23);
            this.wall_button.TabIndex = 2;
            this.wall_button.UseVisualStyleBackColor = false;
            // 
            // investigated_button
            // 
            this.investigated_button.BackColor = System.Drawing.Color.LightSkyBlue;
            this.investigated_button.Enabled = false;
            this.investigated_button.Location = new System.Drawing.Point(31, 360);
            this.investigated_button.Name = "investigated_button";
            this.investigated_button.Size = new System.Drawing.Size(33, 23);
            this.investigated_button.TabIndex = 3;
            this.investigated_button.UseVisualStyleBackColor = false;
            // 
            // open_button
            // 
            this.open_button.BackColor = System.Drawing.Color.PowderBlue;
            this.open_button.Enabled = false;
            this.open_button.Location = new System.Drawing.Point(31, 380);
            this.open_button.Name = "open_button";
            this.open_button.Size = new System.Drawing.Size(33, 23);
            this.open_button.TabIndex = 4;
            this.open_button.UseVisualStyleBackColor = false;
            // 
            // path_button
            // 
            this.path_button.BackColor = System.Drawing.Color.Gold;
            this.path_button.Enabled = false;
            this.path_button.Location = new System.Drawing.Point(31, 400);
            this.path_button.Name = "path_button";
            this.path_button.Size = new System.Drawing.Size(33, 23);
            this.path_button.TabIndex = 7;
            this.path_button.UseVisualStyleBackColor = false;
            // 
            // finish_label
            // 
            this.finish_label.AutoSize = true;
            this.finish_label.Location = new System.Drawing.Point(70, 325);
            this.finish_label.Name = "finish_label";
            this.finish_label.Size = new System.Drawing.Size(265, 13);
            this.finish_label.TabIndex = 13;
            this.finish_label.Text = "-> The finish point or goal the algorithm will try to reach.";
            // 
            // start_label
            // 
            this.start_label.AutoSize = true;
            this.start_label.Location = new System.Drawing.Point(70, 305);
            this.start_label.Name = "start_label";
            this.start_label.Size = new System.Drawing.Size(211, 13);
            this.start_label.TabIndex = 14;
            this.start_label.Text = "-> The start point the algorithm will run from.";
            // 
            // wall_label
            // 
            this.wall_label.AutoSize = true;
            this.wall_label.Location = new System.Drawing.Point(70, 345);
            this.wall_label.Name = "wall_label";
            this.wall_label.Size = new System.Drawing.Size(372, 13);
            this.wall_label.TabIndex = 15;
            this.wall_label.Text = "-> Wall, the algorithm will not be able to create a path through these grid cells" +
    ".";
            // 
            // investigated_label
            // 
            this.investigated_label.AutoSize = true;
            this.investigated_label.Location = new System.Drawing.Point(70, 365);
            this.investigated_label.Name = "investigated_label";
            this.investigated_label.Size = new System.Drawing.Size(378, 13);
            this.investigated_label.TabIndex = 16;
            this.investigated_label.Text = "-> Investigated cell, these cells have been investigated during the path search.";
            // 
            // opened_label
            // 
            this.opened_label.AutoSize = true;
            this.opened_label.Location = new System.Drawing.Point(70, 385);
            this.opened_label.Name = "opened_label";
            this.opened_label.Size = new System.Drawing.Size(535, 13);
            this.opened_label.TabIndex = 17;
            this.opened_label.Text = "-> Opened, these cells have been marked as relevant but not investigated as a mor" +
    "e appropriate cell was found.";
            // 
            // path_label
            // 
            this.path_label.AutoSize = true;
            this.path_label.Location = new System.Drawing.Point(70, 405);
            this.path_label.Name = "path_label";
            this.path_label.Size = new System.Drawing.Size(427, 13);
            this.path_label.TabIndex = 18;
            this.path_label.Text = "-> Path, these cells demonstrate the path (shortest route) the chosen algorithm h" +
    "as found.";
            // 
            // key_label
            // 
            this.key_label.AutoSize = true;
            this.key_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.key_label.Location = new System.Drawing.Point(69, 276);
            this.key_label.Name = "key_label";
            this.key_label.Size = new System.Drawing.Size(43, 20);
            this.key_label.TabIndex = 19;
            this.key_label.Text = "Key:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(29, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(577, 240);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(637, 434);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.key_label);
            this.Controls.Add(this.path_label);
            this.Controls.Add(this.opened_label);
            this.Controls.Add(this.investigated_label);
            this.Controls.Add(this.wall_label);
            this.Controls.Add(this.start_label);
            this.Controls.Add(this.finish_label);
            this.Controls.Add(this.path_button);
            this.Controls.Add(this.open_button);
            this.Controls.Add(this.investigated_button);
            this.Controls.Add(this.wall_button);
            this.Controls.Add(this.finish_button);
            this.Controls.Add(this.start_button);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Help";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button finish_button;
        private System.Windows.Forms.Button wall_button;
        private System.Windows.Forms.Button investigated_button;
        private System.Windows.Forms.Button open_button;
        private System.Windows.Forms.Button path_button;
        private System.Windows.Forms.Label finish_label;
        private System.Windows.Forms.Label start_label;
        private System.Windows.Forms.Label wall_label;
        private System.Windows.Forms.Label investigated_label;
        private System.Windows.Forms.Label opened_label;
        private System.Windows.Forms.Label path_label;
        private System.Windows.Forms.Label key_label;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}