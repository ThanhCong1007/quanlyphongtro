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
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!");
                return;
            }
            List<string> lines = File.ReadAllLines("taikhoan.txt").ToList();
            string newID = txtTaiKhoan.Text + "," + txtMatKhau.Text;
            foreach (string line in lines)
            {
                string[] dataLine = line.Split(',');
                if (string.Compare(dataLine[0], txtTaiKhoan.Text) == 0)
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại!");
                    return;
                }
            }
            Boolean[] check = { false, false, false, false, false };
            for (int i = 0; i < txtMatKhau.Text.Length; i++)
            {
                char c = txtMatKhau.Text[i];
                if (txtMatKhau.Text.Length >= 6 && check[0] == false)
                    check[0] = true;
                else if (check[0] == true) ;
                else
                {
                    MessageBox.Show("Mật khẩu phải từ 6 kí tự trở lên");
                    return;
                }
                if ((c >= 65 && c <= 90) && check[1] == false)
                    check[1] = true;
                if ((c >= 97 && c <= 122) && check[2] == false)
                    check[2] = true;
                if ((c >= 48 && c <= 57) && check[3] == false)
                    check[3] = true;
                if (((c < 65 || (c > 90 && c < 97) || c > 122)) && check[4] == false)
                    if (c < 48 || c > 57)
                        check[4] = true;
                if (check[0] == true && check[1] == true && check[2] == true && check[3] == true && check[4] == true)
                {
                    lines.Add(newID);
                    File.WriteAllLines("taikhoan.txt", lines);
                    MessageBox.Show("Đăng ký thành công!");
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Mật khẩu có ít nhất 1 chữ hoa, 1 chữ thường, 1 số, 1 kí tự đặc biệt");

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
                btnDangKy_Click(sender, e);
            }
        }
    }
}
