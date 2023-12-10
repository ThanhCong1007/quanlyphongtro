using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_phòng_trọ
{
    internal class DichVu
    {
        private string _madichvu, _tendichvu, _giadichvu;

        public string MaDichVu { get => _madichvu; set => _madichvu = value; }
        public string TenDichVu { get => _tendichvu; set => _tendichvu = value; }
        public string GiaDichVu { get => _giadichvu; set => _giadichvu = value; }

        public DichVu(string maDichVu, string tenDichVu, string giaDichVu)
        {
            MaDichVu = maDichVu;
            TenDichVu = tenDichVu;
            GiaDichVu = giaDichVu;
        }

        public DichVu()
        {
            MaDichVu = "";
            TenDichVu = "";
            GiaDichVu = "";
        }
    }
}
