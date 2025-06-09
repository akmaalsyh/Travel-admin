using System.Drawing;
using System.Windows.Forms;

namespace Travel
{
    partial class reservasi
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbPelanggan;
        private System.Windows.Forms.ComboBox cbJadwal;
        private System.Windows.Forms.TextBox tJumlahTiket;
        private System.Windows.Forms.TextBox tTotalHarga;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;

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

        private void InitializeComponent()
        {
            this.cbPelanggan = new System.Windows.Forms.ComboBox();
            this.cbJadwal = new System.Windows.Forms.ComboBox();
            this.tJumlahTiket = new System.Windows.Forms.TextBox();
            this.tTotalHarga = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbPelanggan
            // 
            this.cbPelanggan.FormattingEnabled = true;
            this.cbPelanggan.Location = new System.Drawing.Point(30, 30);
            this.cbPelanggan.Name = "cbPelanggan";
            this.cbPelanggan.Size = new System.Drawing.Size(245, 24);
            this.cbPelanggan.TabIndex = 0;
            this.cbPelanggan.SelectedIndexChanged += new System.EventHandler(this.cbPelanggan_SelectedIndexChanged);
            // 
            // cbJadwal
            // 
            this.cbJadwal.FormattingEnabled = true;
            this.cbJadwal.Location = new System.Drawing.Point(30, 70);
            this.cbJadwal.Name = "cbJadwal";
            this.cbJadwal.Size = new System.Drawing.Size(245, 24);
            this.cbJadwal.TabIndex = 1;
            // 
            // tJumlahTiket
            // 
            this.tJumlahTiket.Location = new System.Drawing.Point(30, 110);
            this.tJumlahTiket.Name = "tJumlahTiket";
            this.tJumlahTiket.Size = new System.Drawing.Size(245, 22);
            this.tJumlahTiket.TabIndex = 2;
            this.tJumlahTiket.Text = "Jumlah Tiket";
            // 
            // tTotalHarga
            // 
            this.tTotalHarga.Location = new System.Drawing.Point(30, 150);
            this.tTotalHarga.Name = "tTotalHarga";
            this.tTotalHarga.Size = new System.Drawing.Size(245, 22);
            this.tTotalHarga.TabIndex = 3;
            this.tTotalHarga.Text = "Total Harga";
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "pending",
            "terbayar",
            "dibatalkan"});
            this.cbStatus.Location = new System.Drawing.Point(30, 190);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(245, 24);
            this.cbStatus.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(296, 30);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(517, 274);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(30, 230);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 31);
            this.btnTambah.TabIndex = 6;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(115, 230);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 31);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(200, 230);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 31);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Hapus";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(30, 267);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(245, 37);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Segarkan Data";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(30, 321);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(783, 139);
            this.reportViewer1.TabIndex = 10;
            // 
            // reservasi
            // 
            this.ClientSize = new System.Drawing.Size(836, 512);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.tTotalHarga);
            this.Controls.Add(this.tJumlahTiket);
            this.Controls.Add(this.cbJadwal);
            this.Controls.Add(this.cbPelanggan);
            this.Name = "reservasi";
            this.Text = "Formulir Reservasi";
            this.Load += new System.EventHandler(this.reservasi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}