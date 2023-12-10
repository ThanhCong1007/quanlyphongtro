using Microsoft.SqlServer.Server;
using Quản_lý_phòng_trọ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Quản_lý_phòng_trọ
{
    class XmlHelper
    {
        public static void SaveDataHopDongToXml(string xmlFilePath, Dictionary<string, HopDong> data)
        {
            try
            {
                XDocument xmlDoc;

                // Kiểm tra xem tệp XML đã tồn tại hay chưa
                if (File.Exists(xmlFilePath))
                {
                    // Nếu tệp đã tồn tại, tải nó lên
                    xmlDoc = XDocument.Load(xmlFilePath);
                }
                else
                {
                    // Nếu tệp chưa tồn tại, tạo một tài liệu XML mới
                    xmlDoc = new XDocument(new XElement("DanhSachHopDong"));
                }

                XElement danhSachHopDong = xmlDoc.Root;

                foreach (var kv in data)
                {
                    // Kiểm tra xem mã phòng đã tồn tại trong tài liệu XML hay chưa
                    if (!danhSachHopDong.Elements("HopDong").Any(hopDong => hopDong.Element("MaPhong").Value == kv.Value.MaPhong))
                    {
                        XElement hopDong = new XElement("HopDong",
                            new XElement("NgayHieuLuc", kv.Value.NgayHieuLuc.ToString()),
                            new XElement("NgayHetHan", kv.Value.NgayHetHan.ToString()),
                            new XElement("MaHopDong", kv.Value.MaHopDong),
                            new XElement("MaPhong", kv.Value.MaPhong),
                            new XElement("GiaThue", kv.Value.GiaThue),
                            new XElement("DichVuSuDung", kv.Value.DichVuSuDung)
                        );
                        danhSachHopDong.Add(hopDong);
                    }
                }

                xmlDoc.Save(xmlFilePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong luu duocw file"+ex.Message);
            }
        }

        public static void SaveDataNguoiThueToXml(string xmlFilePath, Dictionary<string, NguoiThue> dataDanhSachNguoiThue)
        {
            try
            {
                // Tạo một tài liệu XML bằng XDocument
                XDocument xmlDoc;

                // Kiểm tra xem tệp XML đã tồn tại hay chưa
                if (File.Exists(xmlFilePath))
                {
                    xmlDoc = XDocument.Load(xmlFilePath);

                    // Lấy danh sách các CCCD đã tồn tại trong tài liệu XML
                    var existingCCCDs = xmlDoc.Descendants("CCCD").Select(e => e.Value).ToList();

                    foreach (var kv in dataDanhSachNguoiThue)
                    {
                        if (!existingCCCDs.Contains(kv.Value.CCCD))
                        {
                            // Kiểm tra xem CCCD đã tồn tại trong tài liệu XML hay chưa
                            XElement nguoiThue = new XElement("NguoiThue",
                                new XElement("MaPhong", kv.Value.MaPhong),
                                new XElement("HoTen", kv.Value.HoTen),
                                new XElement("CCCD", kv.Value.CCCD),
                                new XElement("SDT", kv.Value.SDT),
                                new XElement("GioiTinh", kv.Value.GioiTinh),
                                new XElement("MaHopDong", kv.Value.MaHopDong)
                            );
                            xmlDoc.Root.Add(nguoiThue);
                        }
                    }
                }
                else
                {
                    // Nếu tệp chưa tồn tại, tạo một tài liệu XML mới
                    xmlDoc = new XDocument(
                        new XElement("DanhSachNguoiThue",
                            dataDanhSachNguoiThue.Select(kv =>
                                new XElement("NguoiThue",
                                    new XElement("MaPhong", kv.Value.MaPhong),
                                    new XElement("HoTen", kv.Value.HoTen),
                                    new XElement("CCCD", kv.Value.CCCD),
                                    new XElement("SDT", kv.Value.SDT),
                                    new XElement("GioiTinh", kv.Value.GioiTinh),
                                    new XElement("MaHopDong", kv.Value.MaHopDong)
                                )
                            )
                        )
                    );
                }

                // Lưu tài liệu XML vào tệp
                xmlDoc.Save(xmlFilePath);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi ghi vào tệp tin XML: " + ex.Message);
            }
        }
        public static void SavePhongTroDataToXML(string filePath, CustomLinkedList<PhongTro> danhSachPhongTro)
        {
            Dictionary<string, PhongTro> dataDanhSachPhongTro = new Dictionary<string, PhongTro>();
            CustomNode<PhongTro> node = danhSachPhongTro.First;
            while (node != null)
            {
                PhongTro pt = new PhongTro();
                pt = node.Data;
                dataDanhSachPhongTro.Add(node.Data.MaPhong, pt);
                node = node.Next;
            }
            try
            {
                XDocument xmlDoc = new XDocument(
                new XElement("PhongTroList",
                dataDanhSachPhongTro.Select(kv =>
                    new XElement("DanhSachPhong",
                        new XElement("MaPhong", kv.Value.MaPhong),
                        new XElement("DienTich", kv.Value.DienTich),
                        new XElement("DiaChi", kv.Value.DiaChi),
                        new XElement("GiaThue", kv.Value.Giathue),
                        new XElement("SoNguoiThue", kv.Value.SoNguoiThue),
                        new XElement("TinhTrang", kv.Value.TinhTrang),
                        new XElement("Is_Deleted", kv.Value.DeletedCheck)
                    )
                )
            )
        );
                xmlDoc.Save(filePath);
            }
            catch (Exception ex) { Console.WriteLine("Lỗi khi ghi vào tệp tin XML: " + ex.Message); }
        }

        public static CustomLinkedList<DichVu> ReadDichVuListFromXml()
        {
            CustomLinkedList<DichVu> dsDV = new CustomLinkedList<DichVu>();
            try
            {
                using (XmlTextReader reader = new XmlTextReader("DanhSachDichVu.xml"))
                {
                    DichVu dv = new DichVu();
                    CustomNode<DichVu> node = new CustomNode<DichVu>(dv);
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DanhSachDichVu") { dv = new DichVu(); node = new CustomNode<DichVu>(dv); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "MaDichVu") { node.Data.MaDichVu = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "TenDichVu") { node.Data.TenDichVu = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "GiaDichVu") { node.Data.GiaDichVu = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DanhSachDichVu") { dsDV.AddLast(node); }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Lỗi khi đọc XML:" + ex.Message); dsDV = null; }
            return dsDV;
        }
        public static CustomLinkedList<HopDong> ReadHopDongListFromXml()
        {
            CustomLinkedList<HopDong> dsHD = new CustomLinkedList<HopDong>();
            try
            {
                using (XmlTextReader reader = new XmlTextReader("DanhSachHopDong.xml"))
                {
                    HopDong hd = new HopDong();
                    CustomNode<HopDong> node = new CustomNode<HopDong>(hd);
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DanhSachHopDong") { hd = new HopDong(); node = new CustomNode<HopDong>(hd); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "MaHopDong") { node.Data.MaHopDong = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "MaPhong") { node.Data.MaPhong = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "NgayHieuLuc") { node.Data.setNgayHieuLuc = DateTime.Parse(reader.ReadElementContentAsString()); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "NgayHetHan") { node.Data.setNgayHetHan = DateTime.Parse(reader.ReadElementContentAsString()); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "GiaThuePhong") { node.Data.GiaThue = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DichVuSuDung") { node.Data.DichVuSuDung = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DanhSachHopDong") { dsHD.AddLast(node); }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Lỗi khi đọc XML:" + ex.Message); dsHD = null; }
            return dsHD;
        }
        public static CustomLinkedList<ThanhToan> ReadThanhToanListFromXml()
        {
            CustomLinkedList<ThanhToan> dsLSTT = new CustomLinkedList<ThanhToan>();
            try
            {
                using (XmlTextReader reader = new XmlTextReader("LichSuThanhToan.xml"))
                {
                    ThanhToan pt = new ThanhToan();
                    CustomNode<ThanhToan> node = new CustomNode<ThanhToan>(pt);
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "LichSuThanhToan") { pt = new ThanhToan(); node = new CustomNode<ThanhToan>(pt); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "MaHopDong") { node.Data.MaHopDong = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "NgayThanhToan") { node.Data.setNgayThanhToan = DateTime.Parse(reader.ReadElementContentAsString()); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "TongTien") { node.Data.TongTien = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DaDong") { node.Data.SoTienDaDong = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "ConLai") { node.Data.SoTienConLai = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "LichSuThanhToan") { dsLSTT.AddLast(node); }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Lỗi khi đọc XML:" + ex.Message); dsLSTT = null; }
            return dsLSTT;
        }
        public static CustomLinkedList<PhongTro> ReadPhongTroListFromXml()
        {
            CustomLinkedList<PhongTro> dsPT = new CustomLinkedList<PhongTro>();
            try
            {
                using (XmlTextReader reader = new XmlTextReader("DanhSachPhongTro.xml"))
                {
                    PhongTro pt = new PhongTro();
                    CustomNode<PhongTro> node = new CustomNode<PhongTro>(pt);
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DanhSachPhong") { pt = new PhongTro(); node = new CustomNode<PhongTro>(pt); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "MaPhong") { node.Data.MaPhong = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DienTich") { node.Data.DienTich = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DiaChi") { node.Data.DiaChi = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "GiaThue") { node.Data.Giathue = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "SoNguoiThue") { node.Data.SoNguoiThue = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "TinhTrang") { node.Data.TinhTrang = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Is_Deleted") { node.Data.DeletedCheck = reader.ReadElementContentAsBoolean(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DanhSachPhong") { dsPT.AddLast(node); }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Lỗi khi đọc XML:" + ex.Message); dsPT = null; }
            return dsPT;
        }
        public static CustomLinkedList<NguoiThue> ReadNguoiThueListFromXml()
        {
            CustomLinkedList<NguoiThue> dsNT = new CustomLinkedList<NguoiThue>();
            try
            {
                using (XmlTextReader reader = new XmlTextReader("DanhSachNguoiThue.xml"))
                {
                    NguoiThue nt = new NguoiThue();
                    CustomNode<NguoiThue> node = new CustomNode<NguoiThue>(nt);
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DanhSachNguoiThue") { nt = new NguoiThue(); node = new CustomNode<NguoiThue>(nt); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "MaPhong") { node.Data.MaPhong = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "HoTen") { node.Data.HoTen = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "SDT") { node.Data.SDT = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "GioiTinh") { node.Data.GioiTinh = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "CCCD") { node.Data.CCCD = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "MaHopDong") { node.Data.MaHopDong = reader.ReadElementContentAsString(); }
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DanhSachNguoiThue") { dsNT.AddLast(node); }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Lỗi khi đọc XML:" + ex.Message); dsNT = null; }
            return dsNT;
        }

    }
}

