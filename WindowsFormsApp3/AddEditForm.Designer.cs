namespace WindowsFormsApp3
{
    partial class AddEditForm
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblStreet = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(284, 105);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(123, 22);
            this.txtName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(284, 83);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(140, 16);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Название компании";
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(287, 153);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(119, 16);
            this.lblContact.TabIndex = 2;
            this.lblContact.Text = "Контактное лицо";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(293, 215);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(67, 16);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Телефон";
            // 
            // lblStreet
            // 
            this.lblStreet.AutoSize = true;
            this.lblStreet.Location = new System.Drawing.Point(293, 273);
            this.lblStreet.Name = "lblStreet";
            this.lblStreet.Size = new System.Drawing.Size(47, 16);
            this.lblStreet.TabIndex = 4;
            this.lblStreet.Text = "Адрес";
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(284, 181);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(123, 22);
            this.txtContact.TabIndex = 5;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(287, 234);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(120, 22);
            this.txtPhone.TabIndex = 6;
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(287, 292);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(120, 22);
            this.txtStreet.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(284, 336);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(184, 43);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(157, 355);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "button1";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // AddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtStreet);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.lblStreet);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Name = "AddEditForm";
            this.Text = "AddEditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblStreet;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}