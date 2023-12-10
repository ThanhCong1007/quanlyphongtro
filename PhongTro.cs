using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_phòng_trọ
{
    internal class PhongTro
    {
        private string m_MaPhong;
        private string m_DienTich;
        private string m_Giathue;
        private string m_DiaChi;
        private string m_TinhTrang;
        private string m_SoNguoiThue;
        private bool m_isDeleted;

        public PhongTro(string maPhong, string dienTich, string giathue, string diaChi, string tinhTrang, string soNguoiThue, bool isDeleted)
        {
            MaPhong = maPhong;
            DienTich = dienTich;
            Giathue = giathue;
            DiaChi = diaChi;
            TinhTrang = tinhTrang;
            SoNguoiThue = soNguoiThue;
            DeletedCheck = isDeleted;
        }

        public PhongTro()
        {
            MaPhong = "";
            DienTich = "";
            Giathue = "";
            DiaChi = "";
            TinhTrang = "Trống";
            SoNguoiThue = "";
            DeletedCheck= false;
        }

        public string MaPhong { get => m_MaPhong; set => m_MaPhong = value; }
        public string DienTich { get => m_DienTich; set => m_DienTich = value; }
        public string Giathue { get => m_Giathue; set => m_Giathue = value; }
        public string DiaChi { get => m_DiaChi; set => m_DiaChi = value; }
        public string TinhTrang { get => m_TinhTrang; set => m_TinhTrang = value; }
        public string SoNguoiThue { get => m_SoNguoiThue; set => m_SoNguoiThue = value; }
        public bool DeletedCheck { get => m_isDeleted; set => m_isDeleted = value; }

    }
}
