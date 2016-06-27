﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservasi_Hotel
{
    public partial class pembayaran1 : Form
    {
        MySqlConnection conn = conectionservice.getconection();
        int jumlah_extra_bed, lama_sewa_extra_bed, harga_total;
        public pembayaran1(int nomor_kamar, string id_tamu)
        {
            InitializeComponent();
            harga_total = 0;
            init(nomor_kamar, id_tamu);
        }

        private void init(int nomor_kamar, string id_tamu)
        {
            try
            {
                string tgl, jam;
                DateTime tgl_awal, tgl_akhir;
                int lama_sewa = 0;
                tgl_awal = new DateTime(2013, 1, 13);
                tgl_akhir = new DateTime(2015, 1, 13);

                string SQL = "SELECT *  FROM transaksi_tamu WHERE id_kamar=" + nomor_kamar + " AND id_tamu=" + id_tamu + ";";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SQL, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label5.Text = reader.GetString("id_kamar");
                    label10.Text = reader.GetString("nama");

                    tgl = reader.GetDateTime("tgl_check_in").ToString("yyyy-M-d");
                    jam = reader.GetTimeSpan("jam_check_in").ToString();
                    label6.Text = konversi_tgl_jam(tgl, jam);

                    tgl = DateTime.Now.ToString("yyyy-M-d");
                    jam = DateTime.Now.ToString("H:m:s");
                    label8.Text = konversi_tgl_jam(tgl, jam);

                    tgl_awal = reader.GetDateTime("tgl_check_in");
                    tgl_akhir = DateTime.Now;
                }
                conn.Close();

                jumlah_ekstra_bed(nomor_kamar);
                lama_sewa = Convert.ToInt32((tgl_akhir - tgl_awal).TotalDays);
                harga_total += (lama_sewa * 100000);
                label7.Text = "Rp. " + harga_total;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        private string konversi_tgl_jam(string tgl, string jam)
        {
            string[] tmp_tgl = tgl.Split('-');
            string[] tmp_jam = jam.Split(':');
            string hasil_tgl, hasil_jam, hasil;
            hasil_jam = tmp_jam[0] + ":" + tmp_jam[1];
            hasil_tgl = tmp_tgl[2];
            int bulan = Convert.ToInt32(tmp_tgl[1]);
            switch(bulan)
            {
                case 1:
                    hasil_tgl += " Januari ";
                    break;
                case 2:
                    hasil_tgl += " Februari ";
                    break;
                case 3:
                    hasil_tgl += " Maret ";
                    break;
                case 4:
                    hasil_tgl += " April ";
                    break;
                case 5:
                    hasil_tgl += " Mei ";
                    break;
                case 6:
                    hasil_tgl += " Juni ";
                    break;
                case 7:
                    hasil_tgl += " Juli ";
                    break;
                case 8:
                    hasil_tgl += " Agustus ";
                    break;
                case 9:
                    hasil_tgl += " September ";
                    break;
                case 10:
                    hasil_tgl += " Oktober ";
                    break;
                case 11:
                    hasil_tgl += " November ";
                    break;
                case 12:
                    hasil_tgl += " Desember ";
                    break;
            }
            hasil_tgl += tmp_tgl[0];
            hasil = hasil_tgl+" ("+hasil_jam+")";
            return hasil;
        }

        private void jumlah_ekstra_bed(int nomor_kamar)
        {          
            try
            {
                DateTime tgl_awal, tgl_akhir;
                tgl_awal = new DateTime(2013, 1, 13);
                tgl_akhir = new DateTime(2015, 1, 13);

                jumlah_extra_bed = 0;
                lama_sewa_extra_bed = 0;
                string SQL = "SELECT extra_bed.tgl_sewa, extra_bed.tgl_berhenti FROM extra_bed, reservasi WHERE reservasi.id=extra_bed.id_reservasi AND reservasi.id_kamar='" + nomor_kamar + "'";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SQL, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.IsDBNull(reader["tgl_berhenti"]))
                    {
                        tgl_awal = reader.GetDateTime("tgl_sewa");
                        tgl_akhir = DateTime.Now;
                    }
                    else
                    {
                        tgl_awal = reader.GetDateTime("tgl_sewa");
                        tgl_akhir = reader.GetDateTime("tgl_berhenti");
                    }
                    

                    jumlah_extra_bed += 1;
                    lama_sewa_extra_bed += Convert.ToInt32((tgl_akhir - tgl_awal).TotalDays);
                }
                conn.Close();

                label12.Text = jumlah_extra_bed.ToString() + " bed (" + lama_sewa_extra_bed + " hari)";
                harga_total += (lama_sewa_extra_bed * jumlah_extra_bed * 50000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
