using ProjectTrainWeb.Models;

namespace ProjectTrainWeb.Services
{
    // ThongKeService: Thong ke doanh thu va bao cao
    // Chuyen tu C++: xemThongKeDoanhThu() trong HeThongDatVe.cpp
    // OOP: Pattern matching (is) thay the dynamic_cast cua C++
    //       Da hinh (Polymorphism): TinhGiaVe() khac nhau cho tung loai ve
    public class ThongKeService
    {
        private readonly TrainBookingService _data;

        public ThongKeService(TrainBookingService data)
        {
            _data = data;
        }

        // ==================== THONG KE TONG QUAT ====================
        // Chuyen tu C++: HeThongDatVe::xemThongKeDoanhThu() phan tong
        public (int tongVe, int tongHanhKhach, double tongDoanhThu, int tongChuyenTau) LayThongKeTongQuat()
        {
            return (
                _data.DanhSachVe.Count,
                _data.DanhSachHanhKhach.Count,
                // Chi tinh doanh thu tu ve da thanh toan (tuong tu C++: if (ve->isThanhToan()))
                _data.DanhSachVe
                    .Where(v => v.DaThanhToan && v.TrangThai == "Dang Hoat Dong")
                    .Sum(v => v.TinhGiaVe()), // Da hinh - TinhGiaVe() khac nhau
                _data.DanhSachChuyenTau.Count
            );
        }

        // ==================== DOANH THU THEO LOAI VE ====================
        // Chuyen tu C++: xemThongKeDoanhThu() phan phan loai VeThuong/VeVIP
        // Su dung Pattern matching (is) thay the dynamic_cast cua C++
        // C++: VeVIP* veVIP = dynamic_cast<VeVIP*>(ve)
        // C#:  if (ve is VeVIP veVip) { ... }
        public (int soVeThuong, double doanhThuThuong, int soVeVIP, double doanhThuVIP,
                int soVeDaThanhToan, int soVeChuaThanhToan) LayDoanhThuTheoLoaiVe()
        {
            int soVeThuong = 0, soVeVIP = 0;
            double doanhThuThuong = 0, doanhThuVIP = 0;
            int soVeDaThanhToan = 0, soVeChuaThanhToan = 0;

            foreach (var ve in _data.DanhSachVe)
            {
                if (ve.TrangThai == "Da Huy") continue; // Bo qua ve da huy

                // === PATTERN MATCHING (thay the dynamic_cast cua C++) ===
                // C++: VeVIP* veVIP = dynamic_cast<VeVIP*>(ve);
                //      if (veVIP != nullptr) { soVeVIP++; ... }
                // C#:  if (ve is VeVIP) { soVeVIP++; ... }
                if (ve is VeVIP)
                {
                    soVeVIP++;
                    if (ve.DaThanhToan)
                    {
                        doanhThuVIP += ve.TinhGiaVe(); // Da hinh - goi VeVIP.TinhGiaVe()
                        soVeDaThanhToan++;
                    }
                    else
                    {
                        soVeChuaThanhToan++;
                    }
                }
                else if (ve is VeThuong)
                {
                    soVeThuong++;
                    if (ve.DaThanhToan)
                    {
                        doanhThuThuong += ve.TinhGiaVe(); // Da hinh - goi VeThuong.TinhGiaVe()
                        soVeDaThanhToan++;
                    }
                    else
                    {
                        soVeChuaThanhToan++;
                    }
                }
            }

            return (soVeThuong, doanhThuThuong, soVeVIP, doanhThuVIP,
                    soVeDaThanhToan, soVeChuaThanhToan);
        }

        // ==================== DOANH THU THEO CHUYEN TAU ====================
        // Logic moi cho web (khong co trong C++ console)
        public List<(ChuyenTau chuyen, int soVe, double doanhThu)> LayDoanhThuTheoChuyenTau()
        {
            var result = new List<(ChuyenTau, int, double)>();

            foreach (var chuyen in _data.DanhSachChuyenTau)
            {
                var veCuaChuyen = _data.DanhSachVe.Where(v => 
                    v.ChuyenTau?.MaChuyen == chuyen.MaChuyen &&
                    v.DaThanhToan &&
                    v.TrangThai == "Dang Hoat Dong").ToList();

                if (veCuaChuyen.Any())
                {
                    // Da hinh: Sum goi TinhGiaVe() cua tung loai ve
                    double doanhThu = veCuaChuyen.Sum(v => v.TinhGiaVe());
                    result.Add((chuyen, veCuaChuyen.Count, doanhThu));
                }
            }

            return result.OrderByDescending(r => r.Item3).ToList();
        }

        // ==================== LICH SU HE THONG ====================
        // Chuyen tu C++: HeThongDatVe luu lich su (khong co trong C++ console)

        // Lay tat ca lich su (cho Admin)
        public List<LichSuHeThong> LayTatCaLichSu()
        {
            return _data.DanhSachLichSu.OrderByDescending(l => l.ThoiGian).ToList();
        }

        // Lay lich su cua nguoi dung hien tai
        public List<LichSuHeThong> LayLichSuCuaToi()
        {
            string ten = _data.TenHienThi;
            if (string.IsNullOrEmpty(ten)) return new List<LichSuHeThong>();
            return _data.DanhSachLichSu
                .Where(l => l.NguoiThucHien == ten)
                .OrderByDescending(l => l.ThoiGian).ToList();
        }
    }
}
