using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Quản_lý_phòng_trọ
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "" || txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!");
                return;
            }
            List<string> lines = File.ReadAllLines("taikhoan.txt").ToList();
            foreach (string line in lines)
            {
                string[] dataLine = line.Split(',');
                if (string.Compare(dataLine[0], txtTaiKhoan.Text) == 0 && string.Compare(dataLine[1], txtMatKhau.Text) == 0)
                {
                    DuLieu duLieu = new DuLieu();
                    this.Hide();
                    duLieu.ShowDialog();
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
        }


        private void linkDangKiTk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangKy dk = new DangKy();
            this.Hide();
            dk.ShowDialog();
            this.Show();
        }

        private void txtTaiKhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMatKhau.Focus();
                e.Handled = true;
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
            }
        }
    }
}
