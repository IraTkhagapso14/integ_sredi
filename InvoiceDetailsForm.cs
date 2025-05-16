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
    public partial class InvoiceDetailsForm : Form
    {
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        public InvoiceDetailsForm()
        {
            InitializeComponent();

            txtInvoiceID.DataBindings.Add("Text", this, "InvoiceID");
            txtProductID.DataBindings.Add("Text", this, "ProductID");
            txtQuantity.DataBindings.Add("Text", this, "Quantity");
            txtPrice.DataBindings.Add("Text", this, "Price");
            txtAmount.DataBindings.Add("Text", this, "Amount");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtInvoiceID.Text, out int invoiceId))
            {
                MessageBox.Show("Введите корректный InvoiceID!");
                return;
            }

            if (!int.TryParse(txtProductID.Text, out int productId))
            {
                MessageBox.Show("Введите корректный ProductID!");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Введите корректное количество!");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Введите корректную цену!");
                return;
            }

            decimal amount = quantity * price;
            InvoiceID = invoiceId;
            ProductID = productId;
            Quantity = quantity;
            Price = price;
            Amount = amount;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}