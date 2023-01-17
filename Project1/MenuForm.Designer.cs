using System;

namespace FishGame
{
    partial class MenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.playSoloButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.enterSensorAmount = new System.Windows.Forms.ComboBox();
            this.enterHiddenNeuronAmount = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.runSimulationButton = new System.Windows.Forms.Button();
            this.enterFishAmount = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // playSoloButton
            // 
            this.playSoloButton.Location = new System.Drawing.Point(471, 224);
            this.playSoloButton.Margin = new System.Windows.Forms.Padding(7);
            this.playSoloButton.Name = "playSoloButton";
            this.playSoloButton.Size = new System.Drawing.Size(204, 65);
            this.playSoloButton.TabIndex = 0;
            this.playSoloButton.Text = "Play Solo";
            this.playSoloButton.UseVisualStyleBackColor = true;
            this.playSoloButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(471, 29);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(204, 159);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // enterSensorAmount
            // 
            this.enterSensorAmount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enterSensorAmount.FormattingEnabled = true;
            this.enterSensorAmount.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.enterSensorAmount.Location = new System.Drawing.Point(471, 470);
            this.enterSensorAmount.Margin = new System.Windows.Forms.Padding(7);
            this.enterSensorAmount.Name = "enterSensorAmount";
            this.enterSensorAmount.Size = new System.Drawing.Size(230, 49);
            this.enterSensorAmount.TabIndex = 3;
            // 
            // enterHiddenNeuronAmount
            // 
            this.enterHiddenNeuronAmount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enterHiddenNeuronAmount.FormattingEnabled = true;
            this.enterHiddenNeuronAmount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.enterHiddenNeuronAmount.Location = new System.Drawing.Point(850, 470);
            this.enterHiddenNeuronAmount.Margin = new System.Windows.Forms.Padding(7);
            this.enterHiddenNeuronAmount.Name = "enterHiddenNeuronAmount";
            this.enterHiddenNeuronAmount.Size = new System.Drawing.Size(232, 49);
            this.enterHiddenNeuronAmount.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 422);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 41);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fish Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 422);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 41);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sensor Amount";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(797, 422);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(338, 41);
            this.label3.TabIndex = 7;
            this.label3.Text = "Hidden Neuron Amount";
            // 
            // runSimulationButton
            // 
            this.runSimulationButton.Location = new System.Drawing.Point(447, 569);
            this.runSimulationButton.Margin = new System.Windows.Forms.Padding(7);
            this.runSimulationButton.Name = "runSimulationButton";
            this.runSimulationButton.Size = new System.Drawing.Size(289, 55);
            this.runSimulationButton.TabIndex = 8;
            this.runSimulationButton.Text = "Run simulation";
            this.runSimulationButton.UseVisualStyleBackColor = true;
            this.runSimulationButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // enterFishAmount
            // 
            this.enterFishAmount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enterFishAmount.FormattingEnabled = true;
            this.enterFishAmount.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60"});
            this.enterFishAmount.Location = new System.Drawing.Point(95, 470);
            this.enterFishAmount.Margin = new System.Windows.Forms.Padding(7);
            this.enterFishAmount.Name = "enterFishAmount";
            this.enterFishAmount.Size = new System.Drawing.Size(230, 49);
            this.enterFishAmount.TabIndex = 9;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 654);
            this.Controls.Add(this.enterFishAmount);
            this.Controls.Add(this.runSimulationButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.enterHiddenNeuronAmount);
            this.Controls.Add(this.enterSensorAmount);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.playSoloButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7);
            this.MaximizeBox = false;
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fish Game Menu";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playSoloButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox enterSensorAmount;
        private System.Windows.Forms.ComboBox enterHiddenNeuronAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button runSimulationButton;
        private System.Windows.Forms.ComboBox enterFishAmount;
    }
}