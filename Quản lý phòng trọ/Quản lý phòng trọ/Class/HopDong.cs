using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_phòng_trọ
{
    internal class HopDong
    {
        private string _mahopdong, _maphong, _giathue, _dichvusudung;
        DateTime _ngayhieuluc, _ngayhethan;

        public string NgayHieuLuc { get => _ngayhieuluc.ToString("dd/MM/yyyy"); }
        public string NgayHetHan { get => _ngayhethan.ToString("dd/MM/yyyy"); }
        public DateTime setNgayHieuLuc { set => _ngayhieuluc = value; }
        public DateTime setNgayHetHan { set => _ngayhethan = value; }
        public string MaHopDong { get => _mahopdong; set => _mahopdong = value; }
        public string MaPhong { get => _maphong; set => _maphong = value; }
        public string GiaThue { get => _giathue; set => _giathue = value; }
        public string DichVuSuDung { get => _dichvusudung; set => _dichvusudung = value; }

        public HopDong(DateTime ngayHieuLuc, DateTime ngayHetHan, string maHD, string maPhong, string giaThue, string DichVu)
        {
            setNgayHieuLuc = ngayHieuLuc;
            setNgayHetHan = ngayHetHan;
            MaHopDong = maHD;
            MaPhong = maPhong;
            GiaThue = giaThue;
            DichVuSuDung = DichVu;
        }

        public HopDong()
        {
            setNgayHieuLuc = System.DateTime.Today;
            setNgayHetHan = System.DateTime.Today;
            MaHopDong = ""; 
            MaPhong = "";
            GiaThue = "";
            DichVuSuDung = "";
        }
    }
}
