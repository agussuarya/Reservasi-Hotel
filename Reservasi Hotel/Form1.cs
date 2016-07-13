﻿using System;
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
    public partial class Form1 : Form
    {
        bool detik;
        public Form1()
        {
            InitializeComponent();
            cek_tgl_jam(DateTime.Now.ToString());
            label6.Text = "Home";
        }

        private void cek_tgl_jam(string date)
        {
            string[] tmp = date.Split(' ');
            string[] tmp_jam = tmp[1].Split(':');
            string am_pm = tmp[2];
            string hasil_tgl, hasil_jam1;

            hasil_jam1 = tmp_jam[0];

            hasil_tgl = tmp[0];

            label1.Text = hasil_tgl;
            if (Convert.ToInt32(tmp_jam[0]) < 10)
            {
                label2.Text = "0"+hasil_jam1;

            }
            else
            {
                label2.Text = hasil_jam1;
            }
            label4.Text = tmp_jam[1] + " " + am_pm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cari_no_kamar a = new cari_no_kamar();
            DialogResult dr = a.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            extra_bed a = new extra_bed();
            DialogResult dr = a.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            check_in a = new check_in();
            DialogResult dr = a.ShowDialog();
        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check_in a = new check_in();
            DialogResult dr = a.ShowDialog();
        }

        private void checkOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kamarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kamar a = new kamar();
            DialogResult dr = a.ShowDialog();
        }

        private void konsumsiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            konsumsi a = new konsumsi();
            DialogResult dr = a.ShowDialog();
        }

        private void extraBedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extra_bed a = new extra_bed();
            DialogResult dr = a.ShowDialog();
        }        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(detik==false)
            {
                label3.Visible = false;
                detik = true;
                cek_tgl_jam(DateTime.Now.ToString());
            }
            else
            {
                label3.Visible = true;
                detik = false;
            }
        }

        private void semuaTarifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tarif a = new tarif();
            DialogResult dr = a.ShowDialog();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            buat_navigasi_tree_view();
            string pilihan = treeView1.SelectedNode.Text;
            switch (pilihan)
            {
                case "Check in":
                    check_in ci = new check_in();
                    DialogResult dci = ci.ShowDialog();
                    break;
                case "Check out":
                    cari_no_kamar co = new cari_no_kamar();
                    DialogResult dco = co.ShowDialog();
                    break;
                case "Extra bed":
                    extra_bed eb = new extra_bed();
                    DialogResult deb = eb.ShowDialog();
                    break;
                case "Room":
                    kamar r = new kamar();
                    DialogResult dr = r.ShowDialog();
                    break;
                case "Price":
                    tarif p = new tarif();
                    DialogResult dp = p.ShowDialog();
                    break;
                case "Consumption":
                    konsumsi c = new konsumsi();
                    DialogResult dc = c.ShowDialog();
                    break;

            }
        }
        
        private void buat_navigasi_tree_view()
        {
            string dir = treeView1.SelectedNode.FullPath.ToString();
            string[] tmp_dir = dir.Split('\\');
            string hasil = "Home>";
            for (int i = 0; i < tmp_dir.Length; i++)
            {
                if(i<tmp_dir.Length-1)
                {
                    hasil += tmp_dir[i] + ">";
                }
                else
                {
                    hasil += tmp_dir[i];

                }
            }
            label6.Text = hasil;    
        }
    }
}
