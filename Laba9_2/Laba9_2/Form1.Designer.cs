namespace Laba9_2
{
    partial class Form1
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
            this.listBoxCities = new System.Windows.Forms.ListBox();
            this.buttonLoadWeather = new System.Windows.Forms.Button();
            this.labelWeather = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxCities
            // 
            this.listBoxCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCities.FormattingEnabled = true;
            this.listBoxCities.Location = new System.Drawing.Point(0, 23);
            this.listBoxCities.Name = "listBoxCities";
            this.listBoxCities.Size = new System.Drawing.Size(300, 200);
            this.listBoxCities.TabIndex = 0;
            // 
            // buttonLoadWeather
            // 
            this.buttonLoadWeather.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonLoadWeather.Location = new System.Drawing.Point(0, 223);
            this.buttonLoadWeather.Name = "buttonLoadWeather";
            this.buttonLoadWeather.Size = new System.Drawing.Size(300, 23);
            this.buttonLoadWeather.TabIndex = 1;
            this.buttonLoadWeather.Text = "Load Weather";
            this.buttonLoadWeather.UseVisualStyleBackColor = true;
            this.buttonLoadWeather.Click += new System.EventHandler(this.buttonLoadWeather_Click);
            // 
            // labelWeather
            // 
            this.labelWeather.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelWeather.Location = new System.Drawing.Point(0, 0);
            this.labelWeather.Name = "labelWeather";
            this.labelWeather.Size = new System.Drawing.Size(300, 23);
            this.labelWeather.TabIndex = 2;
            this.labelWeather.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 246);
            this.Controls.Add(this.listBoxCities);
            this.Controls.Add(this.labelWeather);
            this.Controls.Add(this.buttonLoadWeather);
            this.Name = "Form1";
            this.Text = "Weather App";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxCities;
        private System.Windows.Forms.Button buttonLoadWeather;
        private System.Windows.Forms.Label labelWeather;
    }
}