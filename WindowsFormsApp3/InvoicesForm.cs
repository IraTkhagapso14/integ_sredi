using System;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class InvoicesForm : Form
    {
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int ContractID { get; set; }

        public InvoicesForm()
        {
            InitializeComponent();
            txtInvAmount.DataBindings.Add("Text", this, "TotalAmount");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!TryParseDate(txtInvDate.Text, out DateTime invoiceDate))
            {
                MessageBox.Show("Введите корректную дату в любом формате (например, 2025-04-19 или 20250419)!");
                return;
            }

            if (!decimal.TryParse(txtInvAmount.Text, out decimal totalAmount))
            {
                MessageBox.Show("Введите корректную сумму!");
                return;
            }

            InvoiceDate = invoiceDate;
            TotalAmount = totalAmount;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool TryParseDate(string dateString, out DateTime date)
        {
            string[] formats = { "yyyyMMdd", "yyyy-MM-dd", "dd-MM-yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };

            return DateTime.TryParseExact(dateString, formats, null, System.Globalization.DateTimeStyles.None, out date);
        }
    }
}
