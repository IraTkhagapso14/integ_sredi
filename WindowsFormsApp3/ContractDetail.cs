using System;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class ContractDetail : Form
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public int ContractID { get; set; }
        public int ProductID { get; set; }

        public ContractDetail()
        {
            InitializeComponent();

            txtContractID.DataBindings.Add("Text", this, "ContractID");
            txtProductID.DataBindings.Add("Text", this, "ProductID");
            txtQuantity.DataBindings.Add("Text", this, "Quantity");
            txtPrice.DataBindings.Add("Text", this, "Price");
            txtAmount.DataBindings.Add("Text", this, "Amount");

            txtAmount.ReadOnly = true;

            txtQuantity.TextChanged += (s, e) => RecalculateAmount();
            txtPrice.TextChanged += (s, e) => RecalculateAmount();
        }

        private void RecalculateAmount()
        {
            if (int.TryParse(txtQuantity.Text, out int quantity) &&
                decimal.TryParse(txtPrice.Text, out decimal price))
            {
                Amount = quantity * price;
                txtAmount.Text = Amount.ToString("F2");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!int.TryParse(txtProductID.Text, out int productId))
            {
                MessageBox.Show("Введите корректный Product ID!");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) ||
                !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Введите корректные значения для количества и цены!");
                return;
            }

            ProductID = productId;
            Quantity = quantity;
            Price = price;
            Amount = quantity * price;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
