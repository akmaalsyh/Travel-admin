using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient; // Changed from MySql.Data.MySqlClient
using System.Text.RegularExpressions; // Still needed for time validation if you keep it
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
// If you need to support .xls (Excel 97-2003) files, you'll also need:
// using NPOI.HSSF.UserModel; 

namespace Travel
{
    public partial class mobil : Form
    {
        // Connection string - Using SQL Server
        // Make sure to adjust "AKMAL" and "Travel" according to your SQL Server setup
        // Example for local SQL Server with Windows Authentication (recommended if possible):
        static string connectionString = "Data Source=AKMAL;Initial Catalog=Travel;Integrated Security=True;";
        // Alternative for SQL Server Authentication (if you need a username/password):
        // static string connectionString = "Server=AKMAL;Database=Travel;User ID=YourUsername;Password=YourPassword;";

        // Anda perlu memastikan kontrol-kontrol ini dideklarasikan di mobil.Designer.cs
        // private System.Windows.Forms.TextBox ttujuan;
        // private System.Windows.Forms.DateTimePicker ttanggal;
        // private System.Windows.Forms.TextBox twaktu;
        // private System.Windows.Forms.TextBox tharga;
        // private System.Windows.Forms.TextBox tkapasitas;
        // private System.Windows.Forms.TextBox tmerk;
        // private System.Windows.Forms.TextBox tmodel;
        // private System.Windows.Forms.TextBox tplat;
        // private System.Windows.Forms.ComboBox tstatus;
        // private System.Windows.Forms.DataGridView dataGridView1;
        // private System.Windows.Forms.Button btnTambah;
        // private System.Windows.Forms.Button btnUpdate;
        // private System.Windows.Forms.Button btnDelete;
        // private System.Windows.Forms.Button btnRefresh;
        // private System.Windows.Forms.Button btnImport;


        public mobil()
        {
            InitializeComponent();
        }

        private void mobil_Load(object sender, EventArgs e)
        {
            // Pastikan ComboBox tstatus diisi dengan item yang sesuai
            // Ini bisa dilakukan di desainer atau di sini secara programatis
            if (tstatus.Items.Count == 0)
            {
                tstatus.Items.AddRange(new string[] { "Tersedia", "Tidak Tersedia" });
            }
            LoadData();
            ClearForm(); // Panggil ClearForm setelah tstatus diinisialisasi
        }

        private void ClearForm()
        {
            ttujuan.Clear();
            ttanggal.Value = DateTime.Today;
            twaktu.Text = "";
            tharga.Text = "";
            tkapasitas.Text = "";
            tmerk.Text = "";
            tmodel.Text = "";
            tplat.Text = "";
            // Pastikan tstatus memiliki item sebelum mencoba mengatur SelectedIndex
            if (tstatus.Items.Count > 0)
            {
                tstatus.SelectedIndex = 0;
            }
            else
            {
                tstatus.SelectedIndex = -1; // Atau biarkan -1 jika ComboBox kosong
            }
            ttujuan.Focus();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString)) // Changed to SqlConnection
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM jadwal";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn); // Changed to SqlDataAdapter
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load data: " + ex.Message);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(ttujuan.Text))
            {
                MessageBox.Show("Tujuan wajib diisi!");
                ttujuan.Focus();
                return false;
            }
            // Validasi format waktu (misal: HH:mm atau HH:mm:ss)
            if (string.IsNullOrWhiteSpace(twaktu.Text) || !TimeSpan.TryParse(twaktu.Text.Trim(), out _))
            {
                MessageBox.Show("Format waktu tidak valid! Gunakan format seperti HH:mm atau HH:mm:ss.");
                twaktu.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tharga.Text) || !decimal.TryParse(tharga.Text, out _))
            {
                MessageBox.Show("Harga wajib diisi dan harus angka desimal!");
                tharga.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tkapasitas.Text) || !int.TryParse(tkapasitas.Text, out _))
            {
                MessageBox.Show("Kapasitas wajib diisi dan harus angka!");
                tkapasitas.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tmerk.Text))
            {
                MessageBox.Show("Merk mobil wajib diisi!");
                tmerk.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tmodel.Text))
            {
                MessageBox.Show("Model mobil wajib diisi!");
                tmodel.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tplat.Text))
            {
                MessageBox.Show("Plat nomor wajib diisi!");
                tplat.Focus();
                return false;
            }
            if (tstatus.SelectedIndex < 0)
            {
                MessageBox.Show("Status wajib dipilih!");
                tstatus.Focus();
                return false;
            }
            return true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            using (SqlConnection conn = new SqlConnection(connectionString)) // Changed to SqlConnection
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO jadwal 
                                     (tujuan, tanggal, waktu, harga, kapasitas, merk_mobil, model_mobil, plat_nomor, status)
                                     VALUES (@tujuan, @tanggal, @waktu, @harga, @kapasitas, @merk, @model, @plat, @status)";
                    SqlCommand cmd = new SqlCommand(query, conn); // Changed to SqlCommand
                    cmd.Parameters.AddWithValue("@tujuan", ttujuan.Text.Trim());
                    cmd.Parameters.AddWithValue("@tanggal", ttanggal.Value.Date); // .Date untuk hanya tanggal
                    cmd.Parameters.AddWithValue("@waktu", TimeSpan.Parse(twaktu.Text.Trim())); // SQL Server TIME bisa menerima TimeSpan
                    cmd.Parameters.AddWithValue("@harga", decimal.Parse(tharga.Text.Trim()));
                    cmd.Parameters.AddWithValue("@kapasitas", int.Parse(tkapasitas.Text.Trim()));
                    cmd.Parameters.AddWithValue("@merk", tmerk.Text.Trim());
                    cmd.Parameters.AddWithValue("@model", tmodel.Text.Trim());
                    cmd.Parameters.AddWithValue("@plat", tplat.Text.Trim());
                    cmd.Parameters.AddWithValue("@status", tstatus.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data jadwal berhasil ditambah!");
                    ClearForm();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menambah data: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan diupdate!");
                return;
            }
            if (!ValidateInput())
                return;

            using (SqlConnection conn = new SqlConnection(connectionString)) // Changed to SqlConnection
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE jadwal SET 
                                     tujuan=@tujuan, tanggal=@tanggal, waktu=@waktu, harga=@harga, kapasitas=@kapasitas, 
                                     merk_mobil=@merk, model_mobil=@model, plat_nomor=@plat, status=@status
                                     WHERE id_jadwal=@id";
                    SqlCommand cmd = new SqlCommand(query, conn); // Changed to SqlCommand
                    cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id_jadwal"].Value);
                    cmd.Parameters.AddWithValue("@tujuan", ttujuan.Text.Trim());
                    cmd.Parameters.AddWithValue("@tanggal", ttanggal.Value.Date);
                    cmd.Parameters.AddWithValue("@waktu", TimeSpan.Parse(twaktu.Text.Trim())); // SQL Server TIME bisa menerima TimeSpan
                    cmd.Parameters.AddWithValue("@harga", decimal.Parse(tharga.Text.Trim()));
                    cmd.Parameters.AddWithValue("@kapasitas", int.Parse(tkapasitas.Text.Trim()));
                    cmd.Parameters.AddWithValue("@merk", tmerk.Text.Trim());
                    cmd.Parameters.AddWithValue("@model", tmodel.Text.Trim());
                    cmd.Parameters.AddWithValue("@plat", tplat.Text.Trim());
                    cmd.Parameters.AddWithValue("@status", tstatus.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data jadwal berhasil diupdate!");
                    ClearForm();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal update data: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus!");
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString)) // Changed to SqlConnection
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM jadwal WHERE id_jadwal = @id";
                        SqlCommand cmd = new SqlCommand(query, conn); // Changed to SqlCommand
                        cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id_jadwal"].Value);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data jadwal berhasil dihapus!");
                        ClearForm();
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal hapus data: " + ex.Message);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            MessageBox.Show("Data refreshed successfully!");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ttujuan.Text = row.Cells["tujuan"].Value?.ToString();

                // Set ttanggal.Value if possible, else fallback to today
                if (row.Cells["tanggal"].Value != null && DateTime.TryParse(row.Cells["tanggal"].Value.ToString(), out DateTime tgl))
                    ttanggal.Value = tgl;
                else
                    ttanggal.Value = DateTime.Today;

                // Handle 'waktu' column - SQL Server 'time' type might return TimeSpan
                if (row.Cells["waktu"].Value is TimeSpan timeSpanValue)
                {
                    twaktu.Text = timeSpanValue.ToString(@"hh\:mm"); // Format as HH:mm or HH:mm:ss if needed
                }
                else
                {
                    // Fallback if not a TimeSpan, try to parse or just set as string
                    twaktu.Text = row.Cells["waktu"].Value?.ToString();
                }

                tharga.Text = row.Cells["harga"].Value?.ToString();
                tkapasitas.Text = row.Cells["kapasitas"].Value?.ToString();
                tmerk.Text = row.Cells["merk_mobil"].Value?.ToString();
                tmodel.Text = row.Cells["model_mobil"].Value?.ToString();
                tplat.Text = row.Cells["plat_nomor"].Value?.ToString();

                // Handle 'status' ComboBox selection
                string statusValue = row.Cells["status"].Value?.ToString();
                if (statusValue != null)
                {
                    int index = tstatus.FindStringExact(statusValue);
                    if (index != -1)
                    {
                        tstatus.SelectedIndex = index;
                    }
                    else
                    {
                        MessageBox.Show($"Status '{statusValue}' from database is not found in the status dropdown list. Please check your data or dropdown items.");
                        tstatus.SelectedIndex = -1; // Clear selection if not found
                    }
                }
                else
                {
                    tstatus.SelectedIndex = -1; // Clear selection if status is null
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Pilih File Excel";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        PreviewData(ofd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error membuka file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ini adalah bagian dari metode PreviewData di mobil.cs
        private void PreviewData(string filePath)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("tujuan", typeof(string));
                dt.Columns.Add("tanggal", typeof(DateTime));
                dt.Columns.Add("waktu", typeof(string)); // PASTIKAN INI TETAP STRING
                dt.Columns.Add("harga", typeof(decimal));
                dt.Columns.Add("kapasitas", typeof(int));
                dt.Columns.Add("merk_mobil", typeof(string));
                dt.Columns.Add("model_mobil", typeof(string));
                dt.Columns.Add("plat_nomor", typeof(string));
                dt.Columns.Add("status", typeof(string));

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    IWorkbook workbook;
                    string fileExtension = Path.GetExtension(filePath).ToLower();

                    if (fileExtension == ".xlsx")
                    {
                        workbook = new XSSFWorkbook(fs);
                    }
                    else if (fileExtension == ".xls")
                    {
                        throw new Exception("Format file .xls tidak didukung. Harap gunakan .xlsx atau instal paket NPOI.HSSF.");
                    }
                    else
                    {
                        throw new Exception("Format file tidak didukung. Gunakan .xlsx atau .xls.");
                    }

                    ISheet sheet = workbook.GetSheetAt(0);

                    for (int row = 1; row <= sheet.LastRowNum; row++)
                    {
                        IRow excelRow = sheet.GetRow(row);
                        if (excelRow != null)
                        {
                            DataRow dataRow = dt.NewRow();

                            // --- Pembacaan data yang disarankan ---
                            // Tujuan
                            dataRow["tujuan"] = excelRow.GetCell(0)?.ToString() ?? string.Empty;

                            // Tanggal
                            ICell dateCell = excelRow.GetCell(1);
                            if (dateCell != null && dateCell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(dateCell))
                            {
                                dataRow["tanggal"] = dateCell.DateCellValue;
                            }
                            else if (DateTime.TryParse(dateCell?.ToString(), out DateTime tglExcel))
                            {
                                dataRow["tanggal"] = tglExcel;
                            }
                            else
                            {
                                dataRow["tanggal"] = DBNull.Value; // Atau nilai default yang sesuai
                            }

                            // Waktu (INI BAGIAN UTAMA YANG PERLU DIPERHATIKAN)
                            ICell timeCell = excelRow.GetCell(2);
                            if (timeCell != null)
                            {
                                if (timeCell.CellType == CellType.Numeric)
                                {
                                    // Ini akan mengembalikan nilai double, seperti 0.604166
                                    double numericValue = timeCell.NumericCellValue;
                                    // NPOI memiliki DateUtil.Get='ExcelDate' atau konversi langsung
                                    // Konversi numeric value Excel (fraksi hari) menjadi TimeSpan
                                    // Jika format cell adalah waktu, DateUtil.GetJavaDate() bisa mengembalikan DateTime
                                    // Kemudian ambil bagian waktunya.
                                    try
                                    {
                                        // Ambil nilai waktu sebagai DateTime dan format ke string HH:mm:ss
                                        DateTime timeValue = DateTime.FromOADate(numericValue);
                                        dataRow["waktu"] = timeValue.ToString("HH:mm:ss");
                                    }
                                    catch (Exception)
                                    {
                                        // Fallback jika konversi FromOADate gagal (jarang terjadi untuk waktu valid)
                                        dataRow["waktu"] = timeCell.ToString(); // Coba ambil string mentah
                                    }
                                }
                                else // Jika cellType bukan numeric (mungkin string "14:30")
                                {
                                    dataRow["waktu"] = timeCell.ToString() ?? string.Empty;
                                }
                            }
                            else
                            {
                                dataRow["waktu"] = string.Empty;
                            }


                            // Harga
                            ICell hargaCell = excelRow.GetCell(3);
                            if (hargaCell != null && hargaCell.CellType == CellType.Numeric)
                            {
                                dataRow["harga"] = (decimal)hargaCell.NumericCellValue;
                            }
                            else if (decimal.TryParse(hargaCell?.ToString(), out decimal hargaExcel))
                            {
                                dataRow["harga"] = hargaExcel;
                            }
                            else
                            {
                                dataRow["harga"] = 0m;
                            }

                            // Kapasitas
                            ICell kapasitasCell = excelRow.GetCell(4);
                            if (kapasitasCell != null && kapasitasCell.CellType == CellType.Numeric)
                            {
                                dataRow["kapasitas"] = (int)kapasitasCell.NumericCellValue;
                            }
                            else if (int.TryParse(kapasitasCell?.ToString(), out int kapasitasExcel))
                            {
                                dataRow["kapasitas"] = kapasitasExcel;
                            }
                            else
                            {
                                dataRow["kapasitas"] = 0;
                            }

                            // Merk Mobil, Model Mobil, Plat Nomor, Status (Tetap string)
                            dataRow["merk_mobil"] = excelRow.GetCell(5)?.ToString() ?? string.Empty;
                            dataRow["model_mobil"] = excelRow.GetCell(6)?.ToString() ?? string.Empty;
                            dataRow["plat_nomor"] = excelRow.GetCell(7)?.ToString() ?? string.Empty;
                            dataRow["status"] = excelRow.GetCell(8)?.ToString() ?? string.Empty;

                            dt.Rows.Add(dataRow);
                        }
                    }
                }

                
        // Tampilkan PreviewForm
        // Anda perlu memastikan PreviewForm juga menggunakan SqlConnection
        PreviewForm previewForm = new PreviewForm(dt, connectionString);
                if (previewForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Data jadwal berhasil diimport!", "Import Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"File sedang digunakan oleh program lain. Harap tutup file Excel tersebut. Detail: {ex.Message}", "File Sedang Digunakan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error membaca atau memproses file Excel: {ex.Message}", "Error Impor Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}