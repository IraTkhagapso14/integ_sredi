using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection con;
        private string connString = "Host=127.0.0.1;Username=postgres;Password=postpass;Database=projectpo";

        private string currentTable;
        private DataGridView currentGridView;
        private int currentIdColumnIndex;
        public Form1()
        {
            InitializeComponent();
            InitializeDatabaseConnection();

            tabControl1.Dock = DockStyle.Fill;

            SetupTableLayoutPanel();
            ConfigureAllDataGridViews();
            LoadAllData();

           
            tabControl1.SelectedIndexChanged += (s, e) => {
                UpdateCurrentContext();
                TabControl_SelectedIndexChanged(s, e);
            };
        }

        private void ConfigureAllDataGridViews()
        {
            DataGridView[] gridViews = { dataGridView1, dataGridView2, dataGridView3,
                               dataGridView4, dataGridView5, dataGridView6, dataGridView7 };

            foreach (var dgv in gridViews)
            {
                ConfigureDataGridView(dgv);
            }
        }

        private void ConfigureDataGridView(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Dock = DockStyle.Fill;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Margin = new Padding(0);
            dgv.Visible = true;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = SystemColors.Window;

            dgv.CellContentClick -= DataGridView_CellContentClick;
            
            dgv.CellContentClick += DataGridView_CellContentClick;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            var dgv = sender as DataGridView;
            if (dgv != null && e.RowIndex >= 0)
            {
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = tabControl1.SelectedIndex;
            if (selectedIndex == 7)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
            else
            {
                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }

        private void InitializeDatabaseConnection()
        {
            con = new NpgsqlConnection(connString);
            con.Open();
        }

        private void ConfigureTabControl()
        {
            tabControl1.SelectedIndexChanged += (s, e) => UpdateCurrentContext();
            UpdateCurrentContext();
        }

        private void UpdateCurrentContext()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: 
                    currentTable = "Clients";
                    currentGridView = dataGridView1;
                    currentIdColumnIndex = 0;
                    break;
                case 1:
                    currentTable = "Products";
                    currentGridView = dataGridView2;
                    currentIdColumnIndex = 0;
                    break;
                case 2: 
                    currentTable = "Contracts";
                    currentGridView = dataGridView3;
                    currentIdColumnIndex = 0;
                    break;
                case 3: 
                    currentTable = "ContractDetails";
                    currentGridView = dataGridView4;
                    currentIdColumnIndex = 0;
                    break;
                case 4: 
                    currentTable = "Invoices";
                    currentGridView = dataGridView5;
                    currentIdColumnIndex = 0;
                    break;
                case 5: 
                    currentTable = "InvoiceDetails";
                    currentGridView = dataGridView6;
                    currentIdColumnIndex = 0;
                    break;
                case 6: 
                    currentTable = "Payments";
                    currentGridView = dataGridView7;
                    currentIdColumnIndex = 0;
                    break;
                default:
                    currentTable = null;
                    currentGridView = null;
                    break;
            }
        }

        private void LoadAllData()
        {
            loadClients();
            loadProducts();
            loadContracts();
            loadContractDetails();
            loadInvoices();
            loadInvoiceDetails();
            loadPayments();
        }

        #region Data Loading Methods
        private void loadClients()
        {
            dataGridView1.DataSource = LoadData("Clients");
        }

        private void loadProducts()
        {
            dataGridView2.DataSource = LoadData("Products");
        }

        private void loadContracts()
        {
            dataGridView3.DataSource = LoadData("Contracts");
        }

        private void loadContractDetails()
        {
            dataGridView4.DataSource = LoadData("ContractDetails");
        }

        private void loadInvoices()
        {
            dataGridView5.DataSource = LoadData("Invoices");
        }

        private void loadInvoiceDetails()
        {
            dataGridView6.DataSource = LoadData("InvoiceDetails");
        }

        private void loadPayments()
        {
            dataGridView7.DataSource = LoadData("Payments");
        }

        private DataTable LoadData(string tableName)
        {
            var dt = new DataTable();
            using (var adap = new NpgsqlDataAdapter($"SELECT * FROM {tableName}", con))
            {
                adap.Fill(dt);
            }
            return dt;
        }
        #endregion

        #region CRUD Operations
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                switch (currentTable)
                {
                    case "Clients":
                        AddClient();
                        break;
                    case "Products":
                        AddProduct();
                        break;
                    case "Contracts":
                        AddContract();
                        break;
                    case "ContractDetails":
                        AddContractDetails();
                        break;
                    case "Invoices":
                        AddInvoices();
                        break;
                    case "InvoiceDetails":
                        AddInvoiceDetails();
                        break;
                    case "Payments":
                        AddPayments();
                        break;
                    
                    default:
                        MessageBox.Show("Добавление для этой таблицы не реализовано");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!ValidateSelection()) return;

            try
            {
                switch (currentTable)
                {
                    case "Clients":
                        EditClient();
                        break;
                    case "Products":
                        EditProduct();
                        break;
                    case "Contracts":
                        EditContract();
                        break;
                    case "ContractDetails":
                        EditContractDetails();
                        break;
                    case "Invoices":
                        EditInvoices();
                        break;
                    case "InvoiceDetails":
                        EditInvoiceDetails();
                        break;
                    case "Payments":
                        EditPayments();
                        break;
                    
                    default:
                        MessageBox.Show("Редактирование для этой таблицы не реализовано");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!ValidateSelection()) return;

            var id = GetSelectedId();
            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                using (var cmd = new NpgsqlCommand(
                    $"DELETE FROM {currentTable} WHERE {currentTable.Substring(0, currentTable.Length - 1)}ID = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                RefreshCurrentData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}\n{ex.InnerException?.Message}");
            }
        }
        #endregion

        #region Specific Entity Handlers
        private void AddClient()
        {
            using (var form = new AddEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    using (var cmd = new NpgsqlCommand(
                        "INSERT INTO Clients (ClientName, ContactName, Phone, Street) VALUES (@name, @contact, @phone, @street)", con))
                    {
                        cmd.Parameters.AddWithValue("@name", form.ClientName);
                        cmd.Parameters.AddWithValue("@contact", (object)form.ContactName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@phone", (object)form.Phone ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@street", (object)form.Street ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                    loadClients();
                }
            }
        }

        private void EditClient()
        {
            var id = GetSelectedId();
            using (var cmd = new NpgsqlCommand($"SELECT * FROM Clients WHERE ClientID = {id}", con))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    using (var form = new AddEditForm())
                    {
                        form.ClientName = reader["ClientName"].ToString();
                        form.ContactName = reader["ContactName"]?.ToString();
                        form.Phone = reader["Phone"]?.ToString();
                        form.Street = reader["Street"]?.ToString();

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            reader.Close();
                            using (var updateCmd = new NpgsqlCommand(
                                "UPDATE Clients SET ClientName=@name, ContactName=@contact, " +
                                "Phone=@phone, Street=@street WHERE ClientID=@id", con))
                            {
                                updateCmd.Parameters.AddWithValue("@name", form.ClientName);
                                updateCmd.Parameters.AddWithValue("@contact", (object)form.ContactName ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@phone", (object)form.Phone ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@street", (object)form.Street ?? DBNull.Value);
                                updateCmd.Parameters.AddWithValue("@id", id);
                                updateCmd.ExecuteNonQuery();
                            }
                            loadClients();
                        }
                    }
                }
            }
        }

        private void AddProduct()
        {
            using (var form = new ProductForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    using (var cmd = new NpgsqlCommand(
                        "INSERT INTO Products (ProductTitle, Price) VALUES (@title, @price)", con))
                    {
                        cmd.Parameters.AddWithValue("@title", (object)form.ProductTitle ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@price", (object)form.Price?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                    loadProducts();
                }
            }
        }
        private void EditProduct()
        {
            var id = GetSelectedId();
            if (id <= 0)
            {
                MessageBox.Show("Выберите товар для редактирования.");
                return;
            }

            using (var cmd = new NpgsqlCommand("SELECT * FROM Products WHERE ProductID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        using (var form = new ProductForm())
                        {
                            form.ProductTitle = reader["ProductTitle"]?.ToString();
                            form.Price = reader["Price"] is DBNull ? 0 : Convert.ToDecimal(reader["Price"]);

                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                reader.Close();

                                using (var updateCmd = new NpgsqlCommand(
                                    "UPDATE Products SET ProductTitle=@title, Price=@price WHERE ProductID=@id", con))
                                {
                                    updateCmd.Parameters.AddWithValue("@title", (object)form.ProductTitle ?? DBNull.Value);
                                    updateCmd.Parameters.AddWithValue("@price", (object)form.Price ?? DBNull.Value);
                                    updateCmd.Parameters.AddWithValue("@id", id);

                                    updateCmd.ExecuteNonQuery();
                                }

                                loadProducts();
                            }
                        }
                    }
                }
            }
        }

        private void AddContract()
        {
            // Загрузка клиентов из БД
            List<Client> clients = LoadClientsFromDatabase();

            using (var form = new ContractForm(clients)) // передаём клиентов
            {
                form.ContractDate = DateTime.Now;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    using (var cmd = new NpgsqlCommand(
                        "INSERT INTO Contracts (ClientID, ContractDate, PrepaymentPercent, PrepaymentAmount, PrepaymentStatus, TotalAmount) " +
                        "VALUES (@client, @date, @percent, @amount, @status, @total)", con))
                    {
                        cmd.Parameters.AddWithValue("@client", form.ClientID);
                        cmd.Parameters.AddWithValue("@date", form.ContractDate);
                        cmd.Parameters.AddWithValue("@percent", form.PrepaymentPercent);
                        cmd.Parameters.AddWithValue("@amount", form.PrepaymentAmount);
                        cmd.Parameters.AddWithValue("@status", form.PrepaymentStatus);
                        cmd.Parameters.AddWithValue("@total", form.TotalAmount);

                        cmd.ExecuteNonQuery();
                    }

                    loadContracts();
                }
            }
        }

        private List<Client> LoadClientsFromDatabase()
        {
            var clients = new List<Client>();
            using (var cmd = new NpgsqlCommand("SELECT ClientID FROM Clients", con))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    clients.Add(new Client
                    {
                        Id = reader.GetInt32(0) // Загружаем только ClientID
                    });
                }
            }
            return clients;
        }

        private void EditContract()
        {
            var id = GetSelectedId();
            if (id <= 0)
            {
                MessageBox.Show("Выберите контракт для редактирования.");
                return;
            }

            // Загрузка клиентов из базы данных
            List<Client> clients = LoadClientsFromDatabase();

            using (var cmd = new NpgsqlCommand("SELECT * FROM Contracts WHERE ContractID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        using (var form = new ContractForm(clients))
                        {
                            form.ContractDate = reader["ContractDate"] is DBNull
                                ? DateTime.Now
                                : Convert.ToDateTime(reader["ContractDate"]);

                            form.PrepaymentPercent = reader["PrepaymentPercent"] is DBNull
                                ? 0
                                : Convert.ToDecimal(reader["PrepaymentPercent"]);

                            form.PrepaymentAmount = reader["PrepaymentAmount"] is DBNull
                                ? 0
                                : Convert.ToDecimal(reader["PrepaymentAmount"]);

                            form.PrepaymentStatus = reader["PrepaymentStatus"]?.ToString();

                            form.TotalAmount = reader["TotalAmount"] is DBNull
                                ? 0
                                : Convert.ToDecimal(reader["TotalAmount"]);

                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                reader.Close();

                                using (var updateCmd = new NpgsqlCommand(
                                    "UPDATE Contracts SET ClientID=@client, ContractDate=@date, PrepaymentPercent=@percent, " +
                                    "PrepaymentAmount=@amount, PrepaymentStatus=@status, TotalAmount=@total WHERE ContractID=@id", con))
                                {
                                    updateCmd.Parameters.AddWithValue("@client", form.ClientID);
                                    updateCmd.Parameters.AddWithValue("@date", form.ContractDate);
                                    updateCmd.Parameters.AddWithValue("@percent", form.PrepaymentPercent);
                                    updateCmd.Parameters.AddWithValue("@amount", form.PrepaymentAmount);
                                    updateCmd.Parameters.AddWithValue("@status", form.PrepaymentStatus ?? (object)DBNull.Value);
                                    updateCmd.Parameters.AddWithValue("@total", form.TotalAmount);
                                    updateCmd.Parameters.AddWithValue("@id", id);

                                    updateCmd.ExecuteNonQuery();
                                }

                                loadContracts();
                            }
                        }
                    }
                }
            }
        }

        private void AddContractDetails()
        {
            using (var form = new ContractDetail())
            {
                // Если форма вернула ОК, продолжаем
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string query = @"INSERT INTO ContractDetails 
                             (ContractID, ProductID, Quantity, Price, Amount) 
                             VALUES (@contractId, @productId, @quantity, @price, @amount)";

                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@contractId", form.ContractID);
                        cmd.Parameters.AddWithValue("@productId", form.ProductID);
                        cmd.Parameters.AddWithValue("@quantity", form.Quantity);
                        cmd.Parameters.AddWithValue("@price", form.Price);
                        cmd.Parameters.AddWithValue("@amount", form.Amount);

                        cmd.ExecuteNonQuery();
                    }

                    loadContractDetails(); // Перезагрузка данных
                }
            }
        }

        private void EditContractDetails()
        {
            var id = GetSelectedId(); // Возвращает ContractDetailID
            if (id <= 0)
            {
                MessageBox.Show("Выберите запись для редактирования.");
                return;
            }

            using (var cmd = new NpgsqlCommand("SELECT * FROM ContractDetails WHERE ContractDetailID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        using (var form = new ContractDetail())
                        {
                            // Передаём данные из базы в форму
                            form.ContractID = reader["ContractID"] is DBNull ? 0 : Convert.ToInt32(reader["ContractID"]);
                            form.ProductID = reader["ProductID"] is DBNull ? 0 : Convert.ToInt32(reader["ProductID"]);
                            form.Quantity = reader["Quantity"] is DBNull ? 0 : Convert.ToInt32(reader["Quantity"]);
                            form.Price = reader["Price"] is DBNull ? 0 : Convert.ToDecimal(reader["Price"]);
                            form.Amount = reader["Amount"] is DBNull ? 0 : Convert.ToDecimal(reader["Amount"]);

                            reader.Close(); // обязательно закрываем, прежде чем выполнять другой запрос

                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                string updateQuery = @"UPDATE ContractDetails 
                                               SET ContractID = @contractId, ProductID = @productId, 
                                                   Quantity = @quantity, Price = @price, Amount = @amount 
                                               WHERE ContractDetailID = @id";

                                using (var updateCmd = new NpgsqlCommand(updateQuery, con))
                                {
                                    updateCmd.Parameters.AddWithValue("@contractId", form.ContractID);
                                    updateCmd.Parameters.AddWithValue("@productId", form.ProductID);
                                    updateCmd.Parameters.AddWithValue("@quantity", form.Quantity);
                                    updateCmd.Parameters.AddWithValue("@price", form.Price);
                                    updateCmd.Parameters.AddWithValue("@amount", form.Amount);
                                    updateCmd.Parameters.AddWithValue("@id", id);

                                    updateCmd.ExecuteNonQuery();
                                }

                                loadContractDetails(); // Обновляем отображение
                            }
                        }
                    }
                }
            }
        }
        private void AddInvoices()
        {
            using (var form = new InvoicesForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string query = "INSERT INTO Invoices (ContractID, InvoiceDate, TotalAmount) " +
                                   "VALUES (@contractId, @invoiceDate, @totalAmount)";

                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@contractId", form.ContractID);
                        cmd.Parameters.AddWithValue("@invoiceDate", form.InvoiceDate);
                        cmd.Parameters.AddWithValue("@totalAmount", form.TotalAmount);

                        cmd.ExecuteNonQuery();
                    }

                    loadInvoices();
                }
            }
        }


        private void EditInvoices()
        {
            var id = GetSelectedId(); // Метод должен возвращать выбранный InvoiceID
            if (id <= 0)
            {
                MessageBox.Show("Выберите запись для редактирования.");
                return;
            }

            using (var cmd = new NpgsqlCommand("SELECT * FROM Invoices WHERE InvoiceID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        using (var form = new InvoicesForm())
                        {
                            form.ContractID = reader["ContractID"] is DBNull
                                ? 0
                                : Convert.ToInt32(reader["ContractID"]);

                            form.InvoiceDate = reader["InvoiceDate"] is DBNull
                                ? DateTime.MinValue
                                : Convert.ToDateTime(reader["InvoiceDate"]);

                            form.TotalAmount = reader["TotalAmount"] is DBNull
                                ? 0
                                : Convert.ToDecimal(reader["TotalAmount"]);

                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                reader.Close();

                                using (var updateCmd = new NpgsqlCommand(
                                    "UPDATE Invoices SET ContractID = @contractId, InvoiceDate = @invoiceDate, " +
                                    "TotalAmount = @totalAmount WHERE InvoiceID = @id", con))
                                {
                                    updateCmd.Parameters.AddWithValue("@contractId", form.ContractID);
                                    updateCmd.Parameters.AddWithValue("@invoiceDate", form.InvoiceDate);
                                    updateCmd.Parameters.AddWithValue("@totalAmount", form.TotalAmount);
                                    updateCmd.Parameters.AddWithValue("@id", id);

                                    updateCmd.ExecuteNonQuery();
                                }

                                loadInvoices(); // Обновление таблицы счетов
                            }
                        }
                    }
                }
            }
        }

        private void AddInvoiceDetails()
        {
            using (var form = new InvoiceDetailsForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string query = "INSERT INTO InvoiceDetails (InvoiceID, ProductID, Quantity, Price, Amount) " +
                                   "VALUES (@invoiceId, @productId, @quantity, @price, @amount)";

                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@invoiceId", form.InvoiceID);
                        cmd.Parameters.AddWithValue("@productId", form.ProductID);
                        cmd.Parameters.AddWithValue("@quantity", form.Quantity);
                        cmd.Parameters.AddWithValue("@price", form.Price);
                        cmd.Parameters.AddWithValue("@amount", form.Amount);

                        cmd.ExecuteNonQuery();
                    }

                    loadInvoiceDetails(); // Загрузка данных после вставки, аналогично loadInvoices()
                }
            }
        }
        private void EditInvoiceDetails()
        {
            var id = GetSelectedId(); // Метод должен возвращать выбранный InvoiceDetailID
            if (id <= 0)
            {
                MessageBox.Show("Выберите запись для редактирования.");
                return;
            }

            using (var cmd = new NpgsqlCommand("SELECT * FROM InvoiceDetails WHERE InvoiceDetailID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        using (var form = new InvoiceDetailsForm())
                        {
                            form.InvoiceID = reader["InvoiceID"] is DBNull
                                ? 0
                                : Convert.ToInt32(reader["InvoiceID"]);

                            form.ProductID = reader["ProductID"] is DBNull
                                ? 0
                                : Convert.ToInt32(reader["ProductID"]);

                            form.Quantity = reader["Quantity"] is DBNull
                                ? 0
                                : Convert.ToInt32(reader["Quantity"]);

                            form.Price = reader["Price"] is DBNull
                                ? 0
                                : Convert.ToDecimal(reader["Price"]);

                            form.Amount = reader["Amount"] is DBNull
                                ? 0
                                : Convert.ToDecimal(reader["Amount"]);

                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                reader.Close(); // Закрываем reader перед выполнением следующего запроса

                                using (var updateCmd = new NpgsqlCommand(
                                    "UPDATE InvoiceDetails SET InvoiceID = @invoiceId, ProductID = @productId, " +
                                    "Quantity = @quantity, Price = @price, Amount = @amount WHERE InvoiceDetailID = @id", con))
                                {
                                    updateCmd.Parameters.AddWithValue("@invoiceId", form.InvoiceID);
                                    updateCmd.Parameters.AddWithValue("@productId", form.ProductID);
                                    updateCmd.Parameters.AddWithValue("@quantity", form.Quantity);
                                    updateCmd.Parameters.AddWithValue("@price", form.Price);
                                    updateCmd.Parameters.AddWithValue("@amount", form.Amount);
                                    updateCmd.Parameters.AddWithValue("@id", id);

                                    updateCmd.ExecuteNonQuery();
                                }

                                loadInvoiceDetails(); // Обновление отображения
                            }
                        }
                    }
                }
            }
        }
        private void AddPayments()
        {
            using (var form = new PaymentsForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string query = "INSERT INTO Payments (ContractID, PaymentDate, Amount, PaymentType, IsPrePayment) " +
                                   "VALUES (@contractId, @paymentDate, @amount, @paymentType, @isPrePayment)";

                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@contractId", form.ContractID);
                        cmd.Parameters.AddWithValue("@paymentDate", form.PaymentDate);
                        cmd.Parameters.AddWithValue("@amount", form.Amount);
                        cmd.Parameters.AddWithValue("@paymentType", form.PaymentType);
                        cmd.Parameters.AddWithValue("@isPrePayment", form.IsPrePayment);

                        cmd.ExecuteNonQuery();
                    }

                    loadPayments(); // Обновление таблицы платежей (сделай такой метод, если его ещё нет)
                }
            }
        }

        private void EditPayments()
        {
            var id = GetSelectedId(); // Метод должен возвращать выбранный PaymentID
            if (id <= 0)
            {
                MessageBox.Show("Выберите платёж для редактирования.");
                return;
            }

            using (var cmd = new NpgsqlCommand("SELECT * FROM Payments WHERE PaymentID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        using (var form = new PaymentsForm())
                        {
                            form.ContractID = reader["ContractID"] is DBNull ? 0 : Convert.ToInt32(reader["ContractID"]);
                            form.Amount = reader["Amount"] is DBNull ? 0 : Convert.ToDecimal(reader["Amount"]);
                            form.PaymentType = reader["PaymentType"]?.ToString() ?? "";
                            form.IsPrePayment = reader["IsPrePayment"] is DBNull ? false : Convert.ToBoolean(reader["IsPrePayment"]);

                            // Обрабатываем дату, чтобы она точно была в допустимом диапазоне
                            if (reader["PaymentDate"] != DBNull.Value)
                            {
                                DateTime date = Convert.ToDateTime(reader["PaymentDate"]);
                                // Проверяем, что дата в допустимом диапазоне
                                if (date < form.PickerMinDate || date > form.PickerMaxDate)
                                    form.PaymentDate = DateTime.Today;
                                else
                                    form.PaymentDate = date;
                            }
                            else
                            {
                                form.PaymentDate = DateTime.Today;
                            }

                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                reader.Close();

                                using (var updateCmd = new NpgsqlCommand(
                                    "UPDATE Payments SET ContractID = @contractId, PaymentDate = @paymentDate, " +
                                    "Amount = @amount, PaymentType = @paymentType, IsPrePayment = @isPrePayment " +
                                    "WHERE PaymentID = @id", con))
                                {
                                    updateCmd.Parameters.AddWithValue("@contractId", form.ContractID);
                                    updateCmd.Parameters.AddWithValue("@paymentDate", form.PaymentDate);
                                    updateCmd.Parameters.AddWithValue("@amount", form.Amount);
                                    updateCmd.Parameters.AddWithValue("@paymentType", form.PaymentType);
                                    updateCmd.Parameters.AddWithValue("@isPrePayment", form.IsPrePayment);
                                    updateCmd.Parameters.AddWithValue("@id", id);

                                    updateCmd.ExecuteNonQuery();
                                }

                                loadPayments(); // Обновление таблицы платежей
                            }
                        }
                    }
                }
            }
        }


  

        #endregion

        #region Helpers
        private bool ValidateSelection()
        {
            if (currentGridView?.CurrentRow == null)
            {
                MessageBox.Show("Выберите запись!");
                return false;
            }
            return true;
        }

        private int GetSelectedId()
        {
            return Convert.ToInt32(currentGridView.CurrentRow.Cells[currentIdColumnIndex].Value);
        }

        private void RefreshCurrentData()
        {
            switch (currentTable)
            {
                case "Clients": loadClients(); break;
                case "Products": loadProducts(); break;
                case "Contracts": loadContracts(); break;
                case "ContractDetails": loadContractDetails(); break;
                case "Invoices": loadInvoices(); break;
                case "InvoiceDetails": loadInvoiceDetails(); break;
                case "Payments": loadPayments(); break;
            }
        }
        #endregion
        private void FillDataGridView(DataGridView dgv, string query, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dgv.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }


        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            string query1 = @"
    SELECT 
        cl.ClientName,
        p.ProductTitle, 
        cd.Quantity, 
        cd.Amount
    FROM 
        ContractDetails cd
    JOIN 
        Contracts c ON c.ContractID = cd.ContractID
    JOIN 
        Products p ON p.ProductID = cd.ProductID
    JOIN 
        Clients cl ON cl.ClientID = c.ClientID
    LEFT JOIN 
        InvoiceDetails id ON id.ProductID = p.ProductID AND id.InvoiceID IN (
            SELECT InvoiceID FROM Invoices WHERE ContractID = c.ContractID
        )
    WHERE 
        id.InvoiceDetailID IS NULL
        AND c.ContractDate BETWEEN @StartDate AND @EndDate;
    ";

            string query2 = @"
SELECT 
    c.ContractID,
    cl.ClientName,
    pay.PaymentDate,
    pay.Amount,
    pay.PaymentType,
    pay.IsPrePayment
FROM 
    Payments pay
JOIN 
    Contracts c ON c.ContractID = pay.ContractID
JOIN 
    Clients cl ON cl.ClientID = c.ClientID
WHERE 
    pay.IsPrePayment = TRUE
    AND pay.PaymentDate BETWEEN @StartDate AND @EndDate;
";

            FillDataGridView(dgvNotShipped, query1, startDate, endDate);
            FillDataGridView(dgvNotPaid, query2, startDate, endDate);
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportDataGridViewToExcel(dgvNotShipped); 
        }
        private void ExportDataGridViewToExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Excel.Application excelApp = new Excel.Application();
            excelApp.Workbooks.Add();
            Excel._Worksheet worksheet = (Excel._Worksheet)excelApp.ActiveSheet;

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString();
                }
            }

            worksheet.Columns.AutoFit();
            excelApp.Visible = true;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
    
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
    
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void SetupTableLayoutPanel()
        {
            tableLayoutPanel1.Dock = DockStyle.Bottom; 

            tableLayoutPanel1.ColumnCount = 3; 
            tableLayoutPanel1.RowCount = 1;    

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));

            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f)); 

            btnAdd.Dock = DockStyle.Fill; 
            btnEdit.Dock = DockStyle.Fill;
            btnDelete.Dock = DockStyle.Fill;

            tableLayoutPanel1.Controls.Add(btnAdd, 0, 0);
            tableLayoutPanel1.Controls.Add(btnEdit, 1, 0);
            tableLayoutPanel1.Controls.Add(btnDelete, 2, 0);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvNotShipped_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}