using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{

    public partial class AddEditForm : Form
    {
   


        public string ClientName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }

        public AddEditForm()
        {
            InitializeCustomComponents();
            SetupDataBindings();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Редактирование клиента";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ClientSize = new Size(400, 250);
            this.Padding = new Padding(10);

            lblName = new Label { Text = "Название компании:", AutoSize = true, Location = new Point(10, 10) };
            txtName = new TextBox { Location = new Point(150, 7), Width = 230, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };

            lblContact = new Label { Text = "Контактное лицо:", AutoSize = true, Location = new Point(10, 40) };
            txtContact = new TextBox { Location = new Point(150, 37), Width = 230, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };

            lblPhone = new Label { Text = "Телефон:", AutoSize = true, Location = new Point(10, 70) };
            txtPhone = new TextBox { Location = new Point(150, 67), Width = 230, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };

            lblStreet = new Label { Text = "Адрес:", AutoSize = true, Location = new Point(10, 100) };
            txtStreet = new TextBox { Location = new Point(150, 97), Width = 230, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };

            btnSave = new Button { Text = "Сохранить", Width = 90, DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Отмена", Width = 90, DialogResult = DialogResult.Cancel };

            btnCancel.Location = new Point(this.ClientSize.Width - btnCancel.Width - 10, this.ClientSize.Height - btnCancel.Height - 10);
            btnSave.Location = new Point(btnCancel.Left - btnSave.Width - 10, btnCancel.Top);

            this.Resize += (s, e) =>
            {
                btnCancel.Left = this.ClientSize.Width - btnCancel.Width - 10;
                btnCancel.Top = this.ClientSize.Height - btnCancel.Height - 10;
                btnSave.Left = btnCancel.Left - btnSave.Width - 10;
                btnSave.Top = btnCancel.Top;
            };

            this.Controls.AddRange(new Control[]
            {
            lblName, txtName,
            lblContact, txtContact,
            lblPhone, txtPhone,
            lblStreet, txtStreet,
            btnSave, btnCancel
            });

            foreach (var textBox in this.Controls.OfType<TextBox>())
            {
                textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private void SetupDataBindings()
        {
            txtName.DataBindings.Add("Text", this, "ClientName");
            txtContact.DataBindings.Add("Text", this, "ContactName");
            txtPhone.DataBindings.Add("Text", this, "Phone");
            txtStreet.DataBindings.Add("Text", this, "Street");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название компании!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}