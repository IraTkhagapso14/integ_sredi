using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp3
{
    public partial class ProductForm : Form
    {
        public string ProductTitle { get; set; }
        public decimal Price { get; set; }

        public ProductForm()
        {
            InitializeComponent();
            txtTitle.DataBindings.Add("Text", this, "ProductTitle");
            txtPrice.DataBindings.Add("Text", this, "Price");

        }


        private void ProductTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название продукта!");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Введите корректную цену!");
                return;
            }

            ProductTitle = txtTitle.Text;
            Price = price;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
