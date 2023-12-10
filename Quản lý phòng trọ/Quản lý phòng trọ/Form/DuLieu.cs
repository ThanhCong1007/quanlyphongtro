using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Quản_lý_phòng_trọ
{
    public partial class DuLieu : Form
    {
        private string phongDangChon;
        CustomLinkedList<PhongTro> danhSachPhong = new CustomLinkedList<PhongTro>();
        CustomLinkedList<NguoiThue> danhSachNguoiThue = new CustomLinkedList<NguoiThue>();
        CustomLinkedList<ThanhToan> lichSuThanhToan = new CustomLinkedList<ThanhToan>();
        CustomLinkedList<HopDong> danhSachHopDong = new CustomLinkedList<HopDong>();
        CustomLinkedList<DichVu> danhSachDichVu = new CustomLinkedList<DichVu>();
        private Dictionary<string, PhongTro> dataDanhSachPhongTro = new Dictionary<string, PhongTro>();
        private Dictionary<string, NguoiThue> dataDanhSachNguoiThue = new Dictionary<string, NguoiThue>();
        private Dictionary<DateTime, ThanhToan> dataLichSuThanhToan = new Dictionary<DateTime, ThanhToan>();
        private Dictionary<string, HopDong> dataDanhSachHopDong = new Dictionary<string, HopDong>();
        private Dictionary<string, DichVu> dataDanhSachDichVu = new Dictionary<string, DichVu>();
        public DuLieu()
        {
            InitializeComponent();
            phongDangChon = "";
            danhSachPhong = XmlHelper.ReadPhongTroListFromXml();
            danhSachNguoiThue = XmlHelper.ReadNguoiThueListFromXml();
            lichSuThanhToan = XmlHelper.ReadThanhToanListFromXml();
            danhSachHopDong = XmlHelper.ReadHopDongListFromXml();
            danhSachDichVu = XmlHelper.ReadDichVuListFromXml();
            CustomNode<PhongTro> node = danhSachPhong.First;
            while (node != null)
            {
                PhongTro pt = new PhongTro();
                if (node.Data.DeletedCheck == false)
                {
                    pt = node.Data;
                    dataDanhSachPhongTro.Add(node.Data.MaPhong, pt);
                }
                node = node.Next;
            }
            CustomNode<DichVu> node2 = danhSachDichVu.First;
            while (node2 != null)
            {
                DichVu dv = new DichVu();
                dv = node2.Data;
                dataDanhSachDichVu.Add(node2.Data.MaDichVu, dv);
                node2 = node2.Next;
            }
            gridViewDanhSachPhongTro.AutoGenerateColumns = false;
            gridViewDanhSachNguoiThue.AutoGenerateColumns = false;
            gridViewDanhSachHopDong.AutoGenerateColumns = false;
            gridViewDichVuPhong.AutoGenerateColumns = false;
            gridViewThanhToan.AutoGenerateColumns = false;
            linkTroLai.Visible = false;
            gridViewDanhSachNguoiThue.Visible = false;
            grpNguoiThue.Visible = false;
            gridViewThanhToan.Visible = false;
            grpThanhToan.Visible = false;
            grpHopDong.Visible = false;
            gridViewDanhSachHopDong.Visible = false;
            gridViewDichVuPhong.Visible = false;
            gridViewDanhSachDichVu.Visible = false;
            grpDichVu.Visible = false;
            lbThngTinDngXem.Text = "  - Danh sách phòng trọ";
            hienThiDanhSachPhong();
            grpTimKiem.Visible = false;
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

        private void hienThiLichSuThanhToan()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataLichSuThanhToan.Values;
            gridViewThanhToan.DataSource = bs;
        }

        private void hienThiDanhSachHopDong()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataDanhSachHopDong.Values;
            gridViewDanhSachHopDong.DataSource = bs;
        }

        private void hienThiDanhSachDichVu()
        {
            if (dataDanhSachDichVu.Count < danhSachDichVu.Count)
            {
                dataDanhSachDichVu.Clear();
                CustomNode<DichVu> node2 = danhSachDichVu.First;
                while (node2 != null)
                {
                    DichVu dv = new DichVu();
                    dv = node2.Data;
                    dataDanhSachDichVu.Add(node2.Data.MaDichVu, dv);
                    node2 = node2.Next;
                }
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = dataDanhSachDichVu.Values;
            gridViewDanhSachDichVu.DataSource = bs;
        }

        private void linkDanhSachPhong_Clicked()
        {
            phongDangChon = "";
            lbPhongDangChon.Text = phongDangChon;
            lbThngTinDngXem.Text = "  - Danh sách phòng trọ ";
            linkTroLai.Visible = false;
            grpThanhToan.Visible = false;
            grpHopDong.Visible = false;
            grpNguoiThue.Visible = false;
            grpTimKiem.Visible = false;
            grpDichVu.Visible = false;
            grpPhongTro.Visible = true;
            gridViewThanhToan.Visible = false;
            gridViewDanhSachHopDong.Visible = false; 
            gridViewDanhSachNguoiThue.Visible = false;
            gridViewDanhSachDichVu.Visible = false;
            gridViewDichVuPhong.Visible = false;
            gridViewDanhSachPhongTro.Visible = true;
            hienThiDanhSachPhong();
        }

        private void linkHopDong_clicked()
        {
            if (dataDanhSachNguoiThue.Count == 0) { MessageBox.Show("Phòng phải có ít nhất 1 người để có hợp đồng!"); return; }
            dataDanhSachHopDong = new Dictionary<string, HopDong>();
            dataDanhSachDichVu = new Dictionary<string, DichVu>();
            string maPhong = phongDangChon;
            string[] dichVuSuDung;
            CustomNode<HopDong> node = danhSachHopDong.First;
            CustomNode<DichVu> node2 = danhSachDichVu.First;
            while (node != null)
            {
                if (node.Data.MaPhong == maPhong)
                {
                    HopDong hd = new HopDong();
                    hd = node.Data;
                    dataDanhSachHopDong.Add(node.Data.MaHopDong, hd);
                    dichVuSuDung = node.Data.DichVuSuDung.Split(',');
                    while (node2 != null)
                    {
                        DichVu dv = new DichVu();
                        for (int i = 0; i  < dichVuSuDung.Length;i++)
                        {
                            if (node2.Data.MaDichVu == dichVuSuDung[i])
                            {
                                dv = node2.Data;
                                dataDanhSachDichVu.Add(node2.Data.MaDichVu, dv);
                            }
                        }
                        node2 = node2.Next;
                    }
                    break;
                }
                node = node.Next;
            }

            if (gridViewDanhSachNguoiThue.Visible == true)
            {
                grpNguoiThue.Visible = false;
                gridViewDanhSachNguoiThue.Visible = false;
            }
            else if (gridViewThanhToan.Visible == true)
            {
                grpThanhToan.Visible = false;
                gridViewThanhToan.Visible = false;
            }
            linkTroLai.Visible = true;
            gridViewDanhSachHopDong.Visible = true;
            gridViewDichVuPhong.Visible = true;
            grpDichVu.Visible = true;
            txtTenDichVu.Visible = false;
            txtGiaDichVu.Visible = false;
            lbTenDV.Visible = false;
            lbGiaDV.Visible = false;
            hienThiDanhSachHopDong();
            if (dataDanhSachHopDong.Count == 0)
            {
                grpHopDong.Visible = true;
                grpDichVu.Visible = false;
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = dataDanhSachDichVu.Values;
            gridViewDichVuPhong.DataSource = bs;
            gridViewDichVuPhong.Rows[0].Selected = false;
            lbThngTinDngXem.Text = "  - Hợp đồng ";
        }

        private void linkThanhToan_clicked()
        {
            if (dataDanhSachNguoiThue.Count == 0) { MessageBox.Show("Cần có hợp đồng để thanh toán!"); return; }
            dataLichSuThanhToan = new Dictionary<DateTime, ThanhToan>();
            string maPhong = phongDangChon;
            CustomNode<ThanhToan> node = lichSuThanhToan.First;
            while (node != null)
            {
                CustomNode<HopDong> pointerHD = danhSachHopDong.First;
                while (pointerHD != null)
                {
                    if (pointerHD.Data.MaHopDong == node.Data.MaHopDong && pointerHD.Data.MaPhong == maPhong)
                    {
                        ThanhToan thanhToan = new ThanhToan();
                        thanhToan = node.Data;
                        dataLichSuThanhToan.Add(DateTime.Parse(node.Data.NgayThanhToan), thanhToan);
                        break;
                    }
                    pointerHD = pointerHD.Next;
                }
                node = node.Next;
            }
            if (gridViewDanhSachNguoiThue.Visible == true)
            {
                grpNguoiThue.Visible = false;
                gridViewDanhSachNguoiThue.Visible = false;
            }
            else if (gridViewDanhSachHopDong.Visible == true)
            {
                grpHopDong.Visible = false;
                gridViewDanhSachHopDong.Visible = false;
            }
            linkTroLai.Visible = true;
            grpThanhToan.Visible = true;
            gridViewThanhToan.Visible = true;
            hienThiLichSuThanhToan();
            lbThngTinDngXem.Text = "  - Lịch sử thanh toán ";

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (gridViewDanhSachPhongTro.Visible == true && grpPhongTro.Visible == true)
                if (danhSachPhong.Count > 0)
                {
                    XmlHelper.SavePhongTroDataToXML("DanhSachPhongTro.xml", danhSachPhong);
                  
                }
            if (gridViewDanhSachNguoiThue.Visible == true && grpNguoiThue.Visible == true)
                if (danhSachNguoiThue.Count > 0)
                {
                    XmlHelper.SaveDataNguoiThueToXml("DanhSachNguoiThue.xml", dataDanhSachNguoiThue);
                }
                if (gridViewDanhSachHopDong.SelectedRows.Count > 0)
                {
                    XmlHelper.SaveDataHopDongToXml("DanhSachHopDong.xml", dataDanhSachHopDong);
                    MessageBox.Show("Lưu thành công!");

                }
        }

        private void quayLai_clicked()
        {
            if (gridViewDanhSachDichVu.Visible == true)
            {
                if (gridViewDichVuPhong.Visible == false && gridViewDanhSachHopDong.Visible == true)
                {
                    gridViewDanhSachDichVu.Visible = false;
                    gridViewDichVuPhong.Visible = true;
                    lbTenDV.Visible = false;
                    lbGiaDV.Visible = false;
                    txtTenDichVu.Visible = false;
                    txtGiaDichVu.Visible = false;
                    return;
                }
                gridViewDanhSachDichVu.Visible = false;
                grpDichVu.Visible = false;
                return;
            }
            else if (gridViewDanhSachNguoiThue.Visible == true)
            {
                linkTroLai.Visible = false;
                gridViewDanhSachNguoiThue.Visible = false;
                grpNguoiThue.Visible = false;
                grpPhongTro.Visible = true;
                phongDangChon = "";
                lbPhongDangChon.Text = phongDangChon;
                gridViewDanhSachPhongTro.Visible = true;
                hienThiDanhSachPhong();
                lbThngTinDngXem.Text = "  - Danh sách phòng trọ ";
                return;
            }
            else if (gridViewThanhToan.Visible == true)
            {
                gridViewThanhToan.Visible = false;
                grpThanhToan.Visible = false;
            }
            else if (gridViewDanhSachHopDong.Visible == true)
            {
                gridViewDanhSachHopDong.Visible = false;
                grpHopDong.Visible = false;
                gridViewDichVuPhong.Visible = false;
                grpDichVu.Visible = false;
            }
            linkTroLai.Visible = true;
            grpNguoiThue.Visible = true;
            gridViewDanhSachNguoiThue.Visible = true;
            hienThiDanhSachNguoiThue();
            lbThngTinDngXem.Text = "  - Danh sách người thuê ";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (grpDichVu.Visible == true)
            {
                if (gridViewDanhSachDichVu.Visible == true)
                {
                    DichVu dichVu = new DichVu();
                    dichVu.MaDichVu = txtMaDichVu.Text;
                    dichVu.TenDichVu = txtTenDichVu.Text;
                    dichVu.GiaDichVu = txtGiaDichVu.Text;
                    CustomNode<DichVu> node = new CustomNode<DichVu>(dichVu);
                    CustomNode<DichVu> pointer = danhSachDichVu.First;
                    while (pointer != null)
                    {
                        if (node.Data.MaDichVu == pointer.Data.MaDichVu) { MessageBox.Show("Dịch vụ " +pointer.Data.MaDichVu + " đã tồn tại!"); return; }
                        pointer = pointer.Next;
                    }
                    danhSachDichVu.AddLast(node);
                    dataDanhSachDichVu.Add(node.Data.MaDichVu, dichVu);
                    hienThiDanhSachDichVu();
                }
                else if (gridViewDichVuPhong.Visible == true)
                {
                    CustomNode<HopDong> pointerHD = danhSachHopDong.First;
                    while (pointerHD != null)
                    {
                        if (pointerHD.Data.MaHopDong == gridViewDanhSachHopDong.Rows[0].Cells[0].Value.ToString())
                        {
                            string[] dvDaSuDung = pointerHD.Data.DichVuSuDung.Split(',');
                            string[] dvThem = txtMaDichVu.Text.Split(',');
                            string existed = "";
                            foreach (string dichVu in dvThem)
                            {
                                for (int i = 0; i < dvDaSuDung.Length; i++)
                                {
                                    if (dichVu == dvDaSuDung[i]) { existed += dichVu + " "; }
                                }
                            }
                            if (existed.Length > 0) { MessageBox.Show("Dịch vụ " + existed + " đã được sử dụng!"); return; }
                            pointerHD.Data.DichVuSuDung = pointerHD.Data.DichVuSuDung + ',' + txtMaDichVu.Text;
                            break;
                        }
                        pointerHD = pointerHD.Next;
                    }
                    quayLai_clicked();
                    linkHopDong_clicked();
                }
            }
            else if (grpPhongTro.Visible == true && gridViewDanhSachPhongTro.Visible == true)
            {
                PhongTro phong = new PhongTro();
                phong.MaPhong = txtMaPhong.Text;
                CustomNode<PhongTro> pointer = danhSachPhong.First;
                while (pointer != null)
                {
                    if (phong.MaPhong == pointer.Data.MaPhong && pointer.Data.DeletedCheck == false) { MessageBox.Show("Phòng đã tồn tại!"); return; }
                    else if (phong.MaPhong == pointer.Data.MaPhong && pointer.Data.DeletedCheck == true)
                    {
                        pointer.Data.DeletedCheck = false;
                        dataDanhSachPhongTro.Add(pointer.Data.MaPhong, pointer.Data);
                        hienThiDanhSachPhong();
                        return;
                    }
                    pointer = pointer.Next;
                }
                phong.DiaChi = txtDiaChi.Text;
                phong.DienTich = txtDienTich.Text;
                phong.Giathue = txtGiaThue.Text;
                phong.SoNguoiThue = txtSo_ngthue.Text;
                if (rdbDaThue.Checked == true && int.Parse(phong.SoNguoiThue) > 0)
                    phong.TinhTrang = "Đang thuê";
                else if (rdbPhongTrong.Checked == true && int.Parse(phong.SoNguoiThue) == 0)
                    phong.TinhTrang = "Trống";
                else { MessageBox.Show("Kiểm tra lại số người thuê và tình trạng phòng!"); return; }
                phong.DeletedCheck = false;
                //kiem tra da ton tai ma phong tro chua
                CustomNode<PhongTro> node = new CustomNode<PhongTro>(phong);
                //truyen phan tu vua them vao sach phong tro hien thi len giao dien
                danhSachPhong.AddLast(node);
                dataDanhSachPhongTro.Add(node.Data.MaPhong, phong);
                hienThiDanhSachPhong();
            }
            else if (grpNguoiThue.Visible == true && gridViewDanhSachNguoiThue.Visible == true)
            {
                int count = dataDanhSachNguoiThue.Count;
                NguoiThue nguoi = new NguoiThue();
                nguoi.MaPhong = phongDangChon;
                nguoi.HoTen = txtHoTen.Text;
                if (rdbNam.Checked == true)
                    nguoi.GioiTinh = rdbNam.Text;
                else if (rdbNu.Checked == true)
                    nguoi.GioiTinh = rdbNu.Text;
                nguoi.SDT = txtSDT.Text;
                nguoi.CCCD = txtCCCD.Text;
                nguoi.MaHopDong = txtMaHopDong.Text;
                //kiem tra da ton tai ma phong tro chua
                CustomNode<NguoiThue> node = new CustomNode<NguoiThue>(nguoi);
                CustomNode<NguoiThue> pointer = danhSachNguoiThue.First;
                while (pointer != null)
                {
                    if (node.Data.CCCD == pointer.Data.CCCD) { MessageBox.Show("Người thuê đã có thông tin ở phòng " + pointer.Data.MaPhong); return; }
                    pointer = pointer.Next;
                }
                //truyen phan tu vua them vao sach phong tro hien thi len giao dien
                danhSachNguoiThue.AddLast(node);
                dataDanhSachNguoiThue.Add(node.Data.CCCD, nguoi);
                hienThiDanhSachNguoiThue();
                if (count == 0)
                {
                    MessageBox.Show("Thêm người đứng tên hợp đồng thành công!\nClick Ok để thêm hợp đồng!");
                    txtMaHopDongHD.Text = txtCCCD.Text;
                    linkHopDong_clicked();
                }
            }
            else if (grpHopDong.Visible == true && gridViewDanhSachHopDong.Visible == true)
            {
                if (dataDanhSachHopDong.Count == 0)
                {
                    HopDong hopDong = new HopDong();
                    hopDong.MaHopDong = txtMaHopDongHD.Text;
                    hopDong.MaPhong = phongDangChon;
                    hopDong.setNgayHieuLuc = dateTimePicker1.Value;
                    hopDong.setNgayHetHan = dateTimePicker2.Value;
                    hopDong.GiaThue = txtGiaThueHD.Text;
                    hopDong.DichVuSuDung = txtDichVuSuDungHD.Text;
                    CustomNode<HopDong> node = new CustomNode<HopDong>(hopDong);
                    CustomNode<HopDong> pointer = danhSachHopDong.First;
                    while (pointer != null)
                    {
                        if (node.Data.MaHopDong == pointer.Data.MaHopDong) { MessageBox.Show("Hợp đồng đã tồn tại ở phòng " + pointer.Data.MaPhong); return; }
                        pointer = pointer.Next;
                    }
                    foreach (DataGridViewRow row in gridViewDanhSachNguoiThue.Rows)
                    {
                        if (gridViewDanhSachNguoiThue.Rows[row.Index].Cells[4].Value.ToString() == hopDong.MaHopDong)
                        {
                            danhSachHopDong.AddLast(node);
                            dataDanhSachHopDong.Add(node.Data.MaHopDong, hopDong);
                            hienThiDanhSachHopDong();
                            quayLai_clicked();
                            linkHopDong_clicked();
                            return;
                        }
                    }
                    MessageBox.Show("Người đại diện hợp đồng không có trong phòng này!");
                }
                else MessageBox.Show("Mỗi phòng chỉ có 1 hợp đồng!");
            }
            else if (grpThanhToan.Visible == true && gridViewThanhToan.Visible == true)
            {
                ThanhToan thanhToan = new ThanhToan();
                thanhToan.MaHopDong = gridViewThanhToan.Rows[0].Cells[4].Value.ToString();
                thanhToan.setNgayThanhToan = dtPickTime.Value;
                thanhToan.TongTien = txtTongTien.Text;
                thanhToan.SoTienDaDong = txtTienDaDong.Text;
                thanhToan.SoTienConLai = (int.Parse(thanhToan.TongTien) - int.Parse(thanhToan.SoTienDaDong)).ToString();
                CustomNode<ThanhToan> node = new CustomNode<ThanhToan>(thanhToan);
                CustomNode<ThanhToan> pointer = lichSuThanhToan.First;
                while (pointer != null)
                {
                    if (pointer.Data.NgayThanhToan == node.Data.NgayThanhToan) { MessageBox.Show("Đã có lịch sử thanh toán vào ngày " + pointer.Data.NgayThanhToan); return; }    
                    pointer = pointer.Next;
                }
                lichSuThanhToan.AddLast(node);
                dataLichSuThanhToan.Add(DateTime.Parse(node.Data.NgayThanhToan), thanhToan);
                hienThiLichSuThanhToan();
                quayLai_clicked();
                linkThanhToan_clicked();
            }
   
        }
        
        private void linkThanhToan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){ linkThanhToan_clicked(); }
        private void linkHopDong_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){ linkHopDong_clicked(); }
        private void linkHopDongTT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {linkHopDong_clicked(); }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { linkThanhToan_LinkClicked(sender, e); }
        private void linkDanhSachPhong_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e){ linkDanhSachPhong_Clicked(); }
        private void linkDongTimKiem_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e) { grpTimKiem.Hide(); }
        private void gridViewDanhSachPhongTro_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) phong_clicked(); }
        private void quayLai_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { quayLai_clicked(); }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            grpTimKiem.Visible = true;
            lbTimPhong.Visible = false;
            txtThongTinCanTim.Visible = false;
            lbTimNguoiThue.Visible = false;
            lbTimHopDong.Visible = false;
            lbTimMaThanhToan.Visible = false;
            btnTim.Visible = false;
        }

        private void chonThongTinCanTim_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == 0)
            {
                lbTimPhong.Visible = true;
                txtThongTinCanTim.Visible = true;
                lbTimNguoiThue.Visible = false;
                lbTimHopDong.Visible = false;
                lbTimMaThanhToan.Visible = false;
            }
            else if (cb.SelectedIndex == 1)
            {
                lbTimPhong.Visible = false;
                txtThongTinCanTim.Visible = true;
                lbTimNguoiThue.Visible = true;
                lbTimHopDong.Visible = false;
                lbTimMaThanhToan.Visible = false;
            }
            else if (cb.SelectedIndex == 2)
            {
                lbTimPhong.Visible = false;
                txtThongTinCanTim.Visible = true;
                lbTimNguoiThue.Visible = false;
                lbTimHopDong.Visible = true;
                lbTimMaThanhToan.Visible = false;
            }
            else if (cb.SelectedIndex == 3)
            {
                lbTimPhong.Visible = false;
                txtThongTinCanTim.Visible = true;
                lbTimNguoiThue.Visible = false;
                lbTimHopDong.Visible = false;
                lbTimMaThanhToan.Visible = true;
            }
        }

        private void phong_clicked()
        {
            dataDanhSachNguoiThue = new Dictionary<string, NguoiThue>();
            CustomNode<NguoiThue> node = danhSachNguoiThue.First;
            if (gridViewDanhSachPhongTro.SelectedRows.Count == 0) return;
            int index = gridViewDanhSachPhongTro.SelectedRows[0].Index;
            string maPhong = gridViewDanhSachPhongTro.Rows[index].Cells[0].Value.ToString();
            string tinhTrang = gridViewDanhSachPhongTro.Rows[index].Cells[5].Value.ToString();
            if (tinhTrang == "Trống")
            {
                MessageBox.Show("Phòng trống!");
                return;
            }
            while (node != null)
            {
                if (node.Data.MaPhong == maPhong && dataDanhSachNguoiThue.ContainsKey(node.Data.CCCD) == false)
                {
                    phongDangChon = maPhong;
                    NguoiThue nt = new NguoiThue();
                    nt = node.Data;
                    dataDanhSachNguoiThue.Add(node.Data.CCCD, nt);
                }
                node = node.Next;
            }
            if (dataDanhSachNguoiThue.Count == 0) { MessageBox.Show("Phòng chưa có hợp đồng và người thuê!\nVui lòng thêm người đứng tên hợp đồng!\nThêm hợp đồng sau khi thêm người đứng tên!"); }
            phongDangChon = maPhong;
            lbPhongDangChon.Text = phongDangChon;
            lbThngTinDngXem.Text = "  - Danh sách người thuê ";
            grpPhongTro.Visible = false;
            gridViewDanhSachPhongTro.Visible = false;
            grpNguoiThue.Visible = true;
            gridViewDanhSachNguoiThue.Visible = true;
            linkTroLai.Visible = true;
            hienThiDanhSachNguoiThue();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (chonThongTinCanTim.SelectedIndex == 0)
                timPhong(sender);
            else if (chonThongTinCanTim.SelectedIndex == 1)
            {
                timNguoi(sender);
                foreach (DataGridViewRow row in gridViewDanhSachNguoiThue.Rows)
                {
                    int index = row.Index;
                    if (row.Cells[3].Value.ToString() == txtThongTinCanTim.Text)
                    {
                        gridViewDanhSachNguoiThue.Rows[index].Selected = true;
                        return;
                    }
                    gridViewDanhSachNguoiThue.Rows[index].Selected = false;
                }
            }
            else if (chonThongTinCanTim.SelectedIndex == 2)
            {
                timNguoi(sender);
                linkHopDong_clicked();

            }
            else if (chonThongTinCanTim.SelectedIndex == 3)
            {
                timNguoi(sender);
                linkThanhToan_clicked();
            }
        }

        private void timPhong(object sender)
        {
            CustomNode<PhongTro> pointer = danhSachPhong.First;
            while (pointer != null)
            {
                if (pointer.Data.MaPhong == txtThongTinCanTim.Text)
                {
                    linkDanhSachPhong_Clicked();
                    BindingSource bs = new BindingSource();
                    bs.DataSource = pointer.Data;
                    gridViewDanhSachPhongTro.DataSource = bs;
                    return;
                }
                pointer = pointer.Next;
            }
            MessageBox.Show("Không tìm thấy!");
        }

        private void timNguoi(object sender)
        {
            CustomNode<NguoiThue> pointer = danhSachNguoiThue.First;
            while (pointer != null)
            {
                if (pointer.Data.CCCD == txtThongTinCanTim.Text)
                {
                    foreach (DataGridViewRow row in gridViewDanhSachPhongTro.Rows)
                    {
                        int index = row.Index;
                        if (row.Cells[0].Value.ToString() == pointer.Data.MaPhong)
                        {
                            linkDanhSachPhong_Clicked();
                            gridViewDanhSachPhongTro.Rows[index].Selected = true;
                            phong_clicked();
                            return;
                        }
                    }
                }
                pointer = pointer.Next;
            }
            MessageBox.Show("Không tìm thấy!");
        }

        private void chonThongTinCanTim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chonThongTinCanTim.SelectedIndex == 4)
            {
                btnTim.Visible = false;
                gridViewDichVuPhong.Visible = false;
                gridViewDanhSachDichVu.Visible = true;
                grpDichVu.Visible = true;
                txtTenDichVu.Visible = true;
                txtGiaDichVu.Visible = true;
                lbTenDV.Visible = true;
                lbGiaDV.Visible = true;
                lbTimMaThanhToan.Visible = false;
                lbTimHopDong.Visible = false;
                lbTimNguoiThue.Visible = false;
                lbTimPhong.Visible = false;
                txtThongTinCanTim.Visible = false;
                hienThiDanhSachDichVu();
            }
            else
                btnTim.Visible = true;
        }

        private void gridViewDichVuPhong_CellClick(object sender, DataGridViewCellEventArgs e) { gridViewDanhSachHopDong.Rows[0].Selected = false; }
        private void gridViewDanhSachHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in gridViewDichVuPhong.Rows)
            {
                int index = row.Index;
                if (gridViewDichVuPhong.Rows[index].Selected == true)
                    gridViewDichVuPhong.Rows[index].Selected = false;
            }
        }

        private void gridViewDanhSachPhongTro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridViewDanhSachPhongTro.Visible == true && grpPhongTro.Visible == true)
            {
                int index = gridViewDanhSachPhongTro.SelectedRows[0].Index;
                txtMaPhong.Text = gridViewDanhSachPhongTro.Rows[index].Cells[0].Value.ToString();
                txtDiaChi.Text = gridViewDanhSachPhongTro.Rows[index].Cells[1].Value.ToString();
                txtDienTich.Text = gridViewDanhSachPhongTro.Rows[index].Cells[2].Value.ToString();
                txtGiaThue.Text = gridViewDanhSachPhongTro.Rows[index].Cells[3].Value.ToString();
                txtSo_ngthue.Text = gridViewDanhSachPhongTro.Rows[index].Cells[4].Value.ToString();
                if (gridViewDanhSachPhongTro.Rows[index].Cells[5].Value.ToString() == "Trống")
                    rdbPhongTrong.Checked = true;
                else if (gridViewDanhSachPhongTro.Rows[index].Cells[5].Value.ToString() == "Đang thuê")
                    rdbDaThue.Checked = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (gridViewDanhSachPhongTro.Visible == true && grpPhongTro.Visible == true)
            {
                int index = gridViewDanhSachPhongTro.SelectedRows[0].Index;
                string maPhongCanSua = gridViewDanhSachPhongTro.Rows[index].Cells[0].Value.ToString();
                string maPhongCheck = txtMaPhong.Text;
                if (maPhongCanSua != maPhongCheck) 
                    foreach (string MaPhong in dataDanhSachPhongTro.Keys) { if (MaPhong == maPhongCheck) { MessageBox.Show("Mã phòng trùng!"); return; } }
                foreach (PhongTro phongTro in dataDanhSachPhongTro.Values)
                {
                    if (phongTro.MaPhong == maPhongCanSua)
                    {
                        if (rdbDaThue.Checked == true && int.Parse(txtSo_ngthue.Text) > 0)
                            phongTro.TinhTrang = "Đang thuê";
                        else if (rdbPhongTrong.Checked == true && int.Parse(txtSo_ngthue.Text) == 0 && gridViewDanhSachPhongTro.Rows[index].Cells[5].Value.ToString() == "Đang thuê")
                        {
                            bool hopDongChecked = checkHopDongHetHan(index);
                            if (hopDongChecked == true) phongTro.TinhTrang = "Trống";
                            else if (hopDongChecked == false) { MessageBox.Show("Hợp đồng chưa đến hạn!"); return; }
                        }
                        else if (rdbPhongTrong.Checked == false && gridViewDanhSachPhongTro.Rows[index].Cells[5].Value.ToString() == "Trống" && txtSo_ngthue.Text != "0")
                            { MessageBox.Show("Kiểm tra lại số người thuê và tình trạng phòng!"); return; }
                        phongTro.MaPhong = txtMaPhong.Text;
                        phongTro.DiaChi = txtDiaChi.Text;
                        phongTro.DienTich = txtDienTich.Text;
                        phongTro.Giathue = txtGiaThue.Text;
                        phongTro.SoNguoiThue = txtSo_ngthue.Text;
                        dataDanhSachPhongTro.Remove(maPhongCanSua);
                        dataDanhSachPhongTro.Add(phongTro.MaPhong, phongTro);
                        CustomNode<PhongTro> phong = new CustomNode<PhongTro>(phongTro);
                        CustomNode<PhongTro> pointer = danhSachPhong.First;
                        while (pointer != null)
                        {
                            if (pointer.Data.MaPhong == maPhongCanSua) { danhSachPhong.Remove(pointer); danhSachPhong.AddLast(phong); break; }    
                            pointer = pointer.Next;
                        }
                        break;
                    }
                }
                hienThiDanhSachPhong();
                gridViewDanhSachPhongTro.Rows[0].Selected = false;
                gridViewDanhSachPhongTro.Rows[index].Selected = true;
            }
        }
        private bool checkHopDongHetHan(int index)
        {
            phong_clicked();
            linkHopDong_clicked();
            if (dataDanhSachHopDong.Count == 0) { return true; }
            else if (DateTime.Parse(gridViewDanhSachHopDong.Rows[0].Cells[3].Value.ToString()) <= DateTime.Today) { linkDanhSachPhong_Clicked(); return true; }
            else if (DateTime.Parse(gridViewDanhSachHopDong.Rows[0].Cells[3].Value.ToString()) > DateTime.Today)
            {
                linkDanhSachPhong_Clicked();
                gridViewDanhSachPhongTro.Rows[0].Selected = false;
                gridViewDanhSachPhongTro.Rows[index].Selected = true;
            }
            return false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gridViewDanhSachPhongTro.Visible == true && grpPhongTro.Visible == true)
            {
                int index = gridViewDanhSachPhongTro.SelectedRows[0].Index;
                string maPhong = gridViewDanhSachPhongTro.Rows[index].Cells[0].Value.ToString();
                foreach (PhongTro phongTro in dataDanhSachPhongTro.Values)
                {
                    if (phongTro.MaPhong == maPhong)
                    {
                        if (gridViewDanhSachPhongTro.Rows[index].Cells[5].Value.ToString() != "Trống") { MessageBox.Show("Chỉ được xóa phòng trống!\nVui lòng kiểm tra tình trạng phòng!"); return; }
                        CustomNode<PhongTro> pointer = danhSachPhong.First;
                        phongTro.DeletedCheck = true;
                        while (pointer != null)
                        {
                            if (pointer.Data.MaPhong == maPhong) { pointer.Data.DeletedCheck = true; break; }
                            pointer = pointer.Next;
                        }
                        dataDanhSachPhongTro.Remove(phongTro.MaPhong);
                        hienThiDanhSachPhong();
                        return; 
                    }
                }
            }
        }
    }
}
