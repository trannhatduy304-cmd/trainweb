using System.Collections.Generic;
using System.Linq;

namespace ProjectTrainWeb.Models
{
    // Lop ToaTau: Dai dien cho mot toa tau
    // Muc dich: Quan ly cac ghe ngoi trong mot toa tau
    // Su dung Composition: Chua danh sach (List) cac GheNgoi
    public class ToaTau
    {
        public string MaToa { get; set; } = string.Empty;
        public string LoaiToa { get; set; } = string.Empty; // Loai: Nhat, Nhi, Ba...
        public double GiaCoBan { get; set; } // Gia co ban cua toa
        public List<GheNgoi> DanhSachGhe { get; set; } = new List<GheNgoi>();

        public ToaTau()
        {
            DanhSachGhe = new List<GheNgoi>();
        }

        public ToaTau(string maToa, string loaiToa, int soGhe, double giaCoBan)
        {
            MaToa = string.IsNullOrWhiteSpace(maToa) ? "Chua_Xac_Dinh" : maToa;
            LoaiToa = string.IsNullOrWhiteSpace(loaiToa) ? "Thong_Thuong" : loaiToa;
            GiaCoBan = giaCoBan > 0 ? giaCoBan : 0;
            DanhSachGhe = new List<GheNgoi>();
            
            for (int i = 1; i <= soGhe; i++)
            {
                DanhSachGhe.Add(new GheNgoi(i, giaCoBan));
            }
        }

        // Kiem tra so luong ghe trong
        public int DemGheTrong()
        {
            return DanhSachGhe.Count(g => !g.TrangThai);
        }

        // Kiem tra so luong ghe da dat
        public int DemGheDaDat()
        {
            return DanhSachGhe.Count(g => g.TrangThai);
        }

        // Tim ghe theo ma
        public GheNgoi? TimGhe(int maGhe)
        {
            return DanhSachGhe.FirstOrDefault(g => g.MaGhe == maGhe);
        }
    }
}