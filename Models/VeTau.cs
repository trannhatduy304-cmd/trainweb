using System;

namespace ProjectTrainWeb.Models
{
    // Lop VeTau: Lop tru tuong (abstract) dai dien cho mot ve tau
    // Muc dich: Lien ket thong tin hanh khach, chuyen tau va ghe ngoi
    // Da hinh: Cac lop con (VeThuong, VeVIP) se ghi de phuong thuc TinhGiaVe()
    public abstract class VeTau
    {
        public string MaVe { get; set; } = string.Empty;
        public ChuyenTau? ChuyenTau { get; set; }
        public HanhKhach? HanhKhach { get; set; }
        public string MaToa { get; set; } = string.Empty;
        public int ViTriGhe { get; set; }
        public DateTime NgayDat { get; set; }
        public double GiaCoBanGhe { get; set; }
        
        // Trang thai thanh toan (chuyen tu C++: daThanhToan)
        public bool DaThanhToan { get; set; } = false;
        
        // Trang thai: Dang Hoat Dong, Da Huy
        public string TrangThai { get; set; } = string.Empty;

        protected VeTau()
        {
            NgayDat = DateTime.Now;
            TrangThai = "Dang Hoat Dong";
        }

        public VeTau(string maVe, ChuyenTau chuyenTau, HanhKhach hanhKhach, 
                    string maToa, int viTriGhe, double giaCoBan)
        {
            MaVe = string.IsNullOrWhiteSpace(maVe) ? Guid.NewGuid().ToString() : maVe;
            ChuyenTau = chuyenTau;
            HanhKhach = hanhKhach;
            MaToa = maToa;
            ViTriGhe = viTriGhe;
            GiaCoBanGhe = giaCoBan > 0 ? giaCoBan : 0;
            NgayDat = DateTime.Now; // Lay thoi gian thuc te luc dat ve
            TrangThai = "Dang Hoat Dong";
        }

        // Phuong thuc ao tinh gia ve (Se duoc ghi de boi cac lop con)
        public abstract double TinhGiaVe();

        // Phuong thuc huy ve
        public void HuyVe()
        {
            TrangThai = "Da Huy";
        }

        // Kiem tra ve con hop le
        public bool ConHopLe()
        {
            return TrangThai == "Dang Hoat Dong" && ChuyenTau != null && ChuyenTau.ConHopLe();
        }

        // Kiem tra ve qua han: chua thanh toan va qua 3 ngay ke tu ngay dat
        // (Chuyen tu C++: kiemTraQuaHan() trong VeTau.cpp)
        public bool KiemTraQuaHan()
        {
            if (DaThanhToan) return false; // Da thanh toan thi khong qua han
            if (TrangThai == "Da Huy") return false; // Da huy thi khong xet
            
            // Tinh so ngay tu ngay dat den hien tai
            TimeSpan khoangCach = DateTime.Now - NgayDat;
            return khoangCach.TotalDays > 3; // Qua 3 ngay la qua han
        }

        // Thanh toan ve (danh dau da thanh toan)
        public void ThanhToanVe()
        {
            DaThanhToan = true;
        }
    }
}