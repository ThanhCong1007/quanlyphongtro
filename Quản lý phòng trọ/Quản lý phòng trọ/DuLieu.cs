using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Quản_lý_phòng_trọ
{
    public partial class DuLieu : Form
    {
        CustomLinkedList<PhongTro> danhSachPhong = new CustomLinkedList<PhongTro>();
        CustomLinkedList<NguoiThue> danhSachNguoiThue = new CustomLinkedList<NguoiThue>();
        //List<PhongTro> danhSachPhong = new List<PhongTro>();
        //List<NguoiThue> danhSachNguoiThue = new List<NguoiThue>();
        private Dictionary<string, PhongTro> dataDanhSachPhongTro = new Dictionary<string, PhongTro>();
        private Dictionary<string, NguoiThue> dataDanhSachNguoiThue = new Dictionary<string, NguoiThue>();
        public DuLieu()
        {
            InitializeComponent();

            danhSachPhong = XmlHelper.ReadPhongTroListFromXml(@"C:\Users\grass\OneDrive\Documents\Documents\Study\giáo án cntt\Đồ Án Tin Học\Quản lý phòng trọ XML\Báo Cáo\DanhSachPhongTro.xml");
            danhSachNguoiThue = XmlHelper.ReadNguoiThueListFromXml(@"C:\Users\grass\OneDrive\Documents\Documents\Study\giáo án cntt\Đồ Án Tin Học\Quản lý phòng trọ XML\Báo Cáo\DanhSachNguoiThue.xml");
            CustomNode<PhongTro> node = danhSachPhong.First;
            while (node!=null)
            {
                PhongTro pt = new PhongTro();
                pt.MaPhong = node.Data.MaPhong;
                pt.DiaChi = node.Data.DiaChi;
                pt.DienTich = node.Data.DienTich;
                pt.Giathue = node.Data.Giathue;
                pt.TinhTrang = node.Data.TinhTrang;
                dataDanhSachPhongTro.Add(node.Data.MaPhong, pt);
                node = node.Next;
            }
            troLai.Hide();
            hienThiDanhSachPhong();
            gridViewDanhSachNguoiThue.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát ra ?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;
        }

        private void hienThiDanhSachPhong()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataDanhSachPhongTro.Values;
            gridViewDanhSachPhongTro.DataSource = bs;
        }

        private void hienThiDanhSachNguoiThue()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataDanhSachNguoiThue.Values;
            gridViewDanhSachNguoiThue.DataSource = bs;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (danhSachPhong.Count > 0)
            {
                MessageBox.Show("Lưu thành công!");
                //XmlHelper.LuuDanhSachPhongTro(danhSachPhong, @"C:\Users\grass\OneDrive\Documents\Documents\Study\giáo án cntt\Đồ Án Tin Học\Báo Cáo - Copy\Báo Cáo\DanhSachPhongTro.xml");
            }
        }

        private void danhSachPhongTro_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataDanhSachNguoiThue = new Dictionary<string, NguoiThue>();
            CustomNode<NguoiThue> node = danhSachNguoiThue.First;
            if (gridViewDanhSachPhongTro.SelectedRows.Count == 0) return;
            int index = gridViewDanhSachPhongTro.SelectedRows[0].Index;
            string maPhong = gridViewDanhSachPhongTro.Rows[index].Cells[0].Value.ToString();
            string tinhTrang = gridViewDanhSachPhongTro.Rows[index].Cells[4].Value.ToString();
            if (tinhTrang == "False")
            {
                MessageBox.Show("Phòng trống!");
                return;
            }
            while(node != null)
            {
                if (node.Data.MaPhong == maPhong && dataDanhSachNguoiThue.ContainsKey(node.Data.CCCD) == false)
                {
                    NguoiThue nt = new NguoiThue();
                    nt.SDT = node.Data.SDT;
                    nt.CCCD = node.Data.CCCD;
                    nt.GioiTinh = node.Data.GioiTinh;
                    nt.HoTen = node.Data.HoTen;
                    dataDanhSachNguoiThue.Add(node.Data.CCCD, nt);
                }
                node = node.Next;
            }
            troLai.Show();
            gridViewDanhSachPhongTro.Hide();
            gridViewDanhSachNguoiThue.Show();
            hienThiDanhSachNguoiThue();
        }

        private void quayLai_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            troLai.Hide();
            gridViewDanhSachNguoiThue.Hide();
            gridViewDanhSachPhongTro.Show();
            hienThiDanhSachPhong();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //lay du lieu tu text box
            string srMaphong = txtMaPhong.Text;
            string srDiaChi = txtDiaChi.Text;
            string srDienTich = txtDienTich.Text;
            string srGiaThue = txtGiaThue.Text;
            string srTinhTrang = rdbDaThue.Checked.ToString();
            PhongTro phongTro = new PhongTro()
            {
                MaPhong = srMaphong,
                DiaChi = srDiaChi,
                DienTich = srDienTich,
                Giathue = srGiaThue,
                TinhTrang = srTinhTrang,
            };
            //kiem tra da ton tai ma phong tro chua
            CustomNode<PhongTro> node = new CustomNode<PhongTro>(phongTro);
            CustomNode<PhongTro> pointer = danhSachPhong.First;
            while (pointer != null)
            {
                if (node.Data.MaPhong == pointer.Data.MaPhong)
                {
                    MessageBox.Show("Phòng đã tồn tại!");
                    return;
                }
                pointer = pointer.Next;
            }
            //truyen phan tu vua them vao sach phong tro hien thi len giao dien
            danhSachPhong.AddLast(node);
            dataDanhSachPhongTro.Add(node.Data.MaPhong, phongTro);
            hienThiDanhSachPhong();
        }
    }
}
