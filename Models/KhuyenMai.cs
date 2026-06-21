using System;

namespace ProjectTrainWeb.Models
{
    // Lop KhuyenMai: Dai dien cho mot ma khuyen mai / voucher
    // Muc dich: Ho tro tinh nang khuyen mai, giam gia ve tau
    public class KhuyenMai
    {
        public string MaCode { get; set; }
        public double PhanTramGiam { get; set; } // % giam (vd: 10 = 10%)
        public double GiamToiDa { get; set; } // So tien giam toi da
        public DateTime NgayHetHan { get; set; }
        public int SoLuongConLai { get; set; }

        public KhuyenMai(string maCode, double phanTramGiam, double giamToiDa, 
                        DateTime ngayHetHan, int soLuongConLai)
        {
            MaCode = string.IsNullOrWhiteSpace(maCode) ? Guid.NewGuid().ToString().Substring(0, 8) : maCode;
            PhanTramGiam = phanTramGiam > 0 && phanTramGiam <= 100 ? phanTramGiam : 0;
            GiamToiDa = giamToiDa > 0 ? giamToiDa : 0;
            NgayHetHan = ngayHetHan;
            SoLuongConLai = soLuongConLai > 0 ? soLuongConLai : 0;
        }

        // Kiem tra khuyen mai con hieu luc
        public bool ConHieuLuc()
        {
            return DateTime.Now <= NgayHetHan && SoLuongConLai > 0;
        }

        // Tinh so tien giam dua tren gia goc
        public double TinhSoTienGiam(double giaGoc)
        {
            if (!ConHieuLuc()) return 0;

            double tienGiam = (giaGoc * PhanTramGiam) / 100;
            
            // Khong duoc vuot qua so tien giam toi da
            if (tienGiam > GiamToiDa)
                tienGiam = GiamToiDa;

            return tienGiam;
        }

        // Su dung mot luong khuyen mai
        public void SuDung()
        {
            if (SoLuongConLai > 0)
                SoLuongConLai--;
        }
    }
}
