using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_phòng_trọ
{
    internal class ThanhToan
    {
        private string _mahopdong, _sotien, _no, _tongtien;
        private DateTime _ngay;

        public string MaHopDong { get => _mahopdong; set => _mahopdong = value; }
        public string NgayThanhToan { get => _ngay.ToString("dd/MM/yyyy");}
        public DateTime setNgayThanhToan {  set => _ngay = value; }
        public string SoTienDaDong { get => _sotien; set => _sotien = value; }
        public string SoTienConLai { get => _no; set => _no = value; }
        public string TongTien { get => _tongtien; set => _tongtien = value; }

        public ThanhToan(string maHopDong, DateTime ngayThanhToan, string soTienDong, string soTienThieu, string tienPhong, string tienDien, string tienNuoc, string tongTien)
        {
            MaHopDong = maHopDong;
            setNgayThanhToan = ngayThanhToan;
            SoTienDaDong = soTienDong;
            SoTienConLai = soTienThieu;
            TongTien = tongTien;
        }

        public ThanhToan()
        {
            MaHopDong = "";
            setNgayThanhToan = System.DateTime.Today;
            SoTienDaDong = "";
            SoTienConLai = "";
            TongTien = "";
        }
    }
}
