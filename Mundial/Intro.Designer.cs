namespace Mundial
{
    partial class Intro
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
            this.selecionarTipoButton = new System.Windows.Forms.Button();
            this.tiposComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selecionarTipoButton
            // 
            this.selecionarTipoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(117)))), ((int)(((byte)(191)))));
            this.selecionarTipoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selecionarTipoButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selecionarTipoButton.ForeColor = System.Drawing.Color.White;
            this.selecionarTipoButton.Location = new System.Drawing.Point(167, 192);
            this.selecionarTipoButton.Name = "selecionarTipoButton";
            this.selecionarTipoButton.Size = new System.Drawing.Size(144, 28);
            this.selecionarTipoButton.TabIndex = 2;
            this.selecionarTipoButton.Text = "CONTINUAR";
            this.selecionarTipoButton.UseVisualStyleBackColor = false;
            this.selecionarTipoButton.Click += new System.EventHandler(this.selecionarTipoButton_Click);
            // 
            // tiposComboBox
            // 
            this.tiposComboBox.FormattingEnabled = true;
            this.tiposComboBox.Location = new System.Drawing.Point(154, 100);
            this.tiposComboBox.Name = "tiposComboBox";
            this.tiposComboBox.Size = new System.Drawing.Size(168, 21);
            this.tiposComboBox.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(142, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(200, 16);
            this.label11.TabIndex = 54;
            this.label11.Text = "Seleccione un tipo de polla";
            // 
            // Intro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 294);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tiposComboBox);
            this.Controls.Add(this.selecionarTipoButton);
            this.Name = "Intro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Intro";
            this.Load += new System.EventHandler(this.Intro_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selecionarTipoButton;
        private System.Windows.Forms.ComboBox tiposComboBox;
        private System.Windows.Forms.Label label11;
    }
}