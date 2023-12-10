using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_phòng_trọ
{
    internal class NguoiThue
    {
        private string _maphong,_ten, _cccd, _sdt, _gioitinh, _mahopdong;

        public string MaPhong {  get => _maphong; set=>_maphong = value; }
        public string HoTen { get => _ten; set => _ten = value; }
        public string CCCD { get => _cccd; set => _cccd = value; }
        public string SDT { get => _sdt; set => _sdt = value; }
        public string GioiTinh { get => _gioitinh; set => _gioitinh = value; }
        public string MaHopDong { get => _mahopdong; set => _mahopdong = value; }

        public NguoiThue(string maPhong, string hoTen, string cccd, string sdt, string gioiTinh, string maHopDong)
        {
            MaPhong = maPhong;
            HoTen = hoTen;
            CCCD = cccd;
            GioiTinh = gioiTinh;
            SDT = sdt;
            MaHopDong = maHopDong;
        }

        public NguoiThue()
        {
            MaPhong = "";
            HoTen = "";
            CCCD = "";
            GioiTinh = "";
            SDT = "";
            MaHopDong = "";
        }
    }
}
