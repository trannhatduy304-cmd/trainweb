using System;

namespace ProjectTrainWeb.Models
{
    // Lop GiaoDich: Dai dien cho mot giao dich tai chinh
    // Muc dich: Tach biet luong tai chinh khoi ve tau, dam bao tinh minh bach
    public class GiaoDich
    {
        public string MaGiaoDich { get; set; }
        public double SoTien { get; set; }
        
        // PhuongThuc: Vi he thong, VNPay, Momo
        public string PhuongThuc { get; set; }
        
        // LoaiGiaoDich: Thanh toan hoac Hoan tien
        public string LoaiGiaoDich { get; set; }
        
        public DateTime NgayThucHien { get; set; }
        
        // TrangThai: Thanh cong, Thất bai, Dang xu ly
        public string TrangThai { get; set; }
        
        public string MaHanhKhach { get; set; }
        public string MaVe { get; set; }

        public GiaoDich(string maGiaoDich, double soTien, string phuongThuc, 
                       string loaiGiaoDich, string maHanhKhach, string maVe)
        {
            MaGiaoDich = string.IsNullOrWhiteSpace(maGiaoDich) ? Guid.NewGuid().ToString() : maGiaoDich;
            SoTien = soTien > 0 ? soTien : 0;
            PhuongThuc = string.IsNullOrWhiteSpace(phuongThuc) ? "Vi He Thong" : phuongThuc;
            LoaiGiaoDich = string.IsNullOrWhiteSpace(loaiGiaoDich) ? "Thanh Toan" : loaiGiaoDich;
            TrangThai = "Dang Xu Ly";
            NgayThucHien = DateTime.Now;
            MaHanhKhach = maHanhKhach;
            MaVe = maVe;
        }

        // Phuong thuc xac nhan giao dich
        public void XacNhanThanhCong()
        {
            TrangThai = "Thanh Cong";
        }

        // Phuong thuc huy giao dich
        public void HuyGiaoDich()
        {
            TrangThai = "That Bai";
        }
    }
}
