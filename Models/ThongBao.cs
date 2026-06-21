using System;

namespace ProjectTrainWeb.Models
{
    // Lop ThongBao: Dai dien cho mot thong bao toi nguoi dung
    // Muc dich: Gui ma ve dien tu (E-ticket) va ma dat cho (PNR) cho hanh khach
    public class ThongBao
    {
        public string MaThongBao { get; set; }
        public string NoiDung { get; set; }
        
        // KenhGui: Email hoac SMS
        public string KenhGui { get; set; }
        
        public DateTime ThoiGianGui { get; set; }
        
        // TrangThai: Da gui, Chua gui, That bai
        public string TrangThai { get; set; }
        
        public string MaHanhKhach { get; set; }
        public string MaVe { get; set; }

        public ThongBao(string maThongBao, string noiDung, string kenhGui, 
                       string maHanhKhach, string maVe)
        {
            MaThongBao = string.IsNullOrWhiteSpace(maThongBao) ? Guid.NewGuid().ToString() : maThongBao;
            NoiDung = noiDung;
            KenhGui = string.IsNullOrWhiteSpace(kenhGui) ? "Email" : kenhGui;
            TrangThai = "Chua Gui";
            ThoiGianGui = DateTime.Now;
            MaHanhKhach = maHanhKhach;
            MaVe = maVe;
        }

        // Danh dau la da gui thanh cong
        public void DanhDauDaGui()
        {
            TrangThai = "Da Gui";
            ThoiGianGui = DateTime.Now;
        }

        // Danh dau la that bai
        public void DanhDauThatBai()
        {
            TrangThai = "That Bai";
        }
    }
}
