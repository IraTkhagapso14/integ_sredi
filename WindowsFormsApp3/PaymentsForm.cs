using System;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class PaymentsForm : Form
    {
        public int ContractID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }
        public bool IsPrePayment { get; set; }
        public DateTime PickerMinDate => dtpPaymentDate.MinDate;
        public DateTime PickerMaxDate => dtpPaymentDate.MaxDate;

        public PaymentsForm()
        {
            InitializeComponent();

            PaymentDate = DateTime.Today;

            txtContractID.DataBindings.Add("Text", this, "ContractID");
            dtpPaymentDate.DataBindings.Add("Value", this, "PaymentDate");
            txtAmount.DataBindings.Add("Text", this, "Amount");
            cmbPaymentType.DataBindings.Add("Text", this, "PaymentType");
            chkIsPrePayment.DataBindings.Add("Checked", this, "IsPrePayment");

            cmbPaymentType.Items.AddRange(new string[] { "Наличные", "Безналичный" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtContractID.Text, out int contractId))
            {
                MessageBox.Show("Введите корректный ContractID!");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Введите корректную сумму!");
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbPaymentType.Text) ||
                (cmbPaymentType.Text != "Наличные" && cmbPaymentType.Text != "Безналичный"))
            {
                MessageBox.Show("Выберите тип оплаты: 'Наличные' или 'Безналичный'");
                return;
            }

            ContractID = contractId;
            PaymentDate = dtpPaymentDate.Value;
            Amount = amount;
            PaymentType = cmbPaymentType.Text;
            IsPrePayment = chkIsPrePayment.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
