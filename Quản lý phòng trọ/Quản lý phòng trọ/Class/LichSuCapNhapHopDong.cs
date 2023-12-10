using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_phòng_trọ
{
    internal class LichSuCapNhapHopDong
    {
        private string _mahopdong, _ngaythaydoi, _noidungthaydoi;

        public string MaHopDong { get => _mahopdong; set => _mahopdong = value; }
        public string NgayThayDoi { get => _ngaythaydoi; set => _ngaythaydoi = value; }
        public string NoiDungThayDoi { get => _noidungthaydoi; set => _noidungthaydoi = value; }

        public LichSuCapNhapHopDong(string maHopDong, string ngayThayDoi, string noiDungThayDoi)
        {
            MaHopDong = maHopDong;
            NgayThayDoi = ngayThayDoi;
            NoiDungThayDoi = noiDungThayDoi;
        }

        public LichSuCapNhapHopDong()
        {
            MaHopDong = "";
            NgayThayDoi = "";
            NoiDungThayDoi = "";
        }
    }
}
