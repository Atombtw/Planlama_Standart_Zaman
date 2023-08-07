﻿using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace GEN_Planlama
{
    public partial class Frm_AdminKullaniciEkle : Form
    {
        public Frm_AdminKullaniciEkle()
        {
            InitializeComponent();
        }

        SqliteBaglantisi bgl = new SqliteBaglantisi();
        Frm_AdminEnvanter _AdminEnvanter = new Frm_AdminEnvanter();

        void Sayı()
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source = Y:\\BILGI TEKNOJILERI PROGRAMLAR\\Database\\GEN_Planlama.db; Version = 3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("Select COUNT(*) From Kullanıcı_Listesi", c))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                    {
                        lblSayı.Text = rdr[0].ToString();
                        int sayi1 = Convert.ToInt32(lblSayı.Text);
                        txtID.Text = Convert.ToString(sayi1 + 1);
                    }
                }
                }
            }

            //SQLiteCommand cmd = new SQLiteCommand("Select COUNT(*) From Kullanıcı_Listesi", bgl.baglanti());
            //SQLiteDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    lblSayı.Text = dr[0].ToString();
            //    int sayi1 = Convert.ToInt32(lblSayı.Text);
            //    txtID.Text = Convert.ToString(sayi1 + 1);
            //}
            //bgl.baglanti().Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            _AdminEnvanter.Show();
        }

        void Listele()
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source = Y:\\BILGI TEKNOJILERI PROGRAMLAR\\Database\\GEN_Planlama.db; Version = 3;"))
            {
                c.Open();
                using (SQLiteDataAdapter cmd = new SQLiteDataAdapter("Select * From Kullanıcı_Listesi", c))
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    cmd.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            //System.Data.DataTable dt = new System.Data.DataTable();
            //SQLiteDataAdapter da = new SQLiteDataAdapter("Select * From Kullanıcı_Listesi", bgl.baglanti());
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            //bgl.baglanti().Close();
        }

        private void Frm_AdminKullaniciEkle_Load(object sender, EventArgs e)
        {
            Listele();
            Sayı();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                txtKullanıcıAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                txtKullanıcıSifre.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                ctxtAdmin.SelectedItem = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                ctxtDurum.SelectedItem = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source = Y:\\BILGI TEKNOJILERI PROGRAMLAR\\Database\\GEN_Planlama.db; Version = 3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("insert into Kullanıcı_Listesi (ID, KULLANICI_ADI, KULLANICI_SIFRESI, KULLANICI_ADMIN, DURUM) values (@p1, @p2, @p3, @p4, @p5)", c))
                {
                    cmd.Parameters.AddWithValue("@p1", txtID.Text);
                    cmd.Parameters.AddWithValue("@p2", txtKullanıcıAd.Text);
                    cmd.Parameters.AddWithValue("@p3", txtKullanıcıSifre.Text);
                    cmd.Parameters.AddWithValue("@p4", ctxtAdmin.Text);
                    cmd.Parameters.AddWithValue("@p5", ctxtDurum.Text);
                    cmd.ExecuteNonQuery();
                    Temizle();
                    Listele();
                    Sayı();
                }
            }

            //try
            //{
            //    SQLiteCommand cmd = new SQLiteCommand("insert into Kullanıcı_Listesi (ID, KULLANICI_ADI, KULLANICI_SIFRESI, KULLANICI_ADMIN, DURUM) values (@p1, @p2, @p3, @p4, @p5)", bgl.baglanti());
            //    cmd.Parameters.AddWithValue("@p1", txtID.Text);
            //    cmd.Parameters.AddWithValue("@p2", txtKullanıcıAd.Text);
            //    cmd.Parameters.AddWithValue("@p3", txtKullanıcıSifre.Text);
            //    cmd.Parameters.AddWithValue("@p4", ctxtAdmin.Text);
            //    cmd.Parameters.AddWithValue("@p5", ctxtDurum.Text);
            //    cmd.ExecuteNonQuery();
            //    bgl.baglanti().Close();
            //    Temizle();
            //    Listele();
            //    Sayı();
            //}
            //catch (Exception hata)
            //{
            //    MessageBox.Show(hata.ToString());
            //}
        }

        void Temizle()
        {
            txtID.Text = "";
            txtKullanıcıAd.Text = "";
            txtKullanıcıSifre.Text = "";
            ctxtAdmin.Text = "";
            ctxtDurum.Text = "";
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source = Y:\\BILGI TEKNOJILERI PROGRAMLAR\\Database\\GEN_Planlama.db; Version = 3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("Update Kullanıcı_Listesi set KULLANICI_ADI = @p1, KULLANICI_SIFRESI = @p2, KULLANICI_ADMIN = @p3, DURUM = @p4 where ID = @p5", c))
                {
                    cmd.Parameters.AddWithValue("@p1", txtKullanıcıAd.Text);
                    cmd.Parameters.AddWithValue("@p2", txtKullanıcıSifre.Text);
                    cmd.Parameters.AddWithValue("@p3", ctxtAdmin.Text);
                    cmd.Parameters.AddWithValue("@p4", ctxtDurum.Text);
                    cmd.Parameters.AddWithValue("@p5", txtID.Text);
                    cmd.ExecuteNonQuery();
                    Listele();
                    Temizle();
                }
            }

            //try
            //{
            //    SQLiteCommand cmd = new SQLiteCommand("Update Kullanıcı_Listesi set KULLANICI_ADI = @p1, KULLANICI_SIFRESI = @p2, KULLANICI_ADMIN = @p3, DURUM = @p4 where ID = @p5", bgl.baglanti());
            //    cmd.Parameters.AddWithValue("@p1", txtKullanıcıAd.Text);
            //    cmd.Parameters.AddWithValue("@p2", txtKullanıcıSifre.Text);
            //    cmd.Parameters.AddWithValue("@p3", ctxtAdmin.Text);
            //    cmd.Parameters.AddWithValue("@p4", ctxtDurum.Text);
            //    cmd.Parameters.AddWithValue("@p5", txtID.Text);
            //    cmd.ExecuteNonQuery();
            //    bgl.baglanti().Close();
            //    Listele();
            //    Temizle();
            //}
            //catch (Exception hata)
            //{
            //    MessageBox.Show(hata.ToString());
            //}
        }
    }
}
