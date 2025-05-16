using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class ContractForm : Form
    {
        public DateTime ContractDate { get; set; }
        public decimal PrepaymentPercent { get; set; }
        public decimal PrepaymentAmount { get; set; }
        public string PrepaymentStatus { get; set; }
        public int ClientID { get; set; }
        public decimal TotalAmount { get; set; }

        public ContractForm(List<Client> clients)
        {
            InitializeComponent();

            dtpContractDate.DataBindings.Add("Value", this, "ContractDate");
            txtPredpro.DataBindings.Add("Text", this, "PrepaymentPercent");
            txtPricepred.DataBindings.Add("Text", this, "PrepaymentAmount");
            cmbStatus.DataBindings.Add("Text", this, "PrepaymentStatus");
            txtTotalAmount.DataBindings.Add("Text", this, "TotalAmount");




            cmbStatus.Items.AddRange(new string[] { "Оплачено", "Не оплачено" });


            cmbClients.DataSource = clients;
            cmbClients.ValueMember = "Id";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (cmbClients.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            if (!decimal.TryParse(txtPredpro.Text, out decimal prepaymentPercent))
            {
                MessageBox.Show("Введите корректный процент предоплаты!");
                return;
            }

            if (!decimal.TryParse(txtPricepred.Text, out decimal prepaymentAmount))
            {
                MessageBox.Show("Введите корректную сумму предоплаты!");
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbStatus.Text) ||
                (cmbStatus.Text != "Оплачено" && cmbStatus.Text != "Не оплачено"))
            {
                MessageBox.Show("Выберите корректный статус предоплаты!");
                return;
            }
            if (!decimal.TryParse(txtTotalAmount.Text, out decimal totalAmount))
            {
                MessageBox.Show("Введите корректную общую сумму!");
                return;
            }

            TotalAmount = totalAmount;

            ContractDate = dtpContractDate.Value;
            PrepaymentPercent = prepaymentPercent;
            PrepaymentAmount = prepaymentAmount;
            PrepaymentStatus = cmbStatus.Text;
            ClientID = (int)cmbClients.SelectedValue;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

}
