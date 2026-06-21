using ProjectTrainWeb.Models;

namespace ProjectTrainWeb.Services
{
    // ChuyenTauService: Quan ly chuyen tau, toa tau, ghe ngoi
    // Chuyen tu C++: TauHoa.cpp + quanLyChuyenTau() trong HeThongDatVe.cpp
    // OOP: Su dung Composition (ChuyenTau chua List<ToaTau>, ToaTau chua List<GheNgoi>)
    //       va Dong goi (Encapsulation) de kiem tra ghe trong
    public class ChuyenTauService
    {
        private readonly TrainBookingService _data;

        public ChuyenTauService(TrainBookingService data)
        {
            _data = data;
        }

        // ==================== LAY DANH SACH ====================

        // Lay tat ca chuyen tau
        public List<ChuyenTau> GetAllChuyenTau()
        {
            return _data.DanhSachChuyenTau;
        }

        // Lay chuyen tau theo ma
        public ChuyenTau? GetChuyenTauById(int maChuyen)
        {
            return _data.DanhSachChuyenTau.FirstOrDefault(ct => ct.MaChuyen == maChuyen);
        }

        // Lay danh sach ga tau
        public List<GaTau> GetAllGaTau()
        {
            return _data.DanhSachGa;
        }

        // ==================== TIM KIEM CHUYEN TAU ====================
        // Chuyen tu C++: HeThongDatVe::timKiemChuyenTau()
        // Tim theo ga di va ga den (tuong tu C++: tim substring)
        public List<ChuyenTau> TimChuyenTau(int? maGaDi = null, int? maGaDen = null)
        {
            var result = _data.DanhSachChuyenTau.AsEnumerable();

            if (maGaDi.HasValue && maGaDi > 0)
                result = result.Where(ct => ct.GaDi?.MaGa == maGaDi);

            if (maGaDen.HasValue && maGaDen > 0)
                result = result.Where(ct => ct.GaDen?.MaGa == maGaDen);

            return result.Where(ct => ct.ConHopLe()).ToList();
        }

        // ==================== KIEM TRA GHE TRONG ====================
        // Chuyen tu C++: ToaTau::hienThiSoDoToa() logic dem ghe
        // Su dung Dong goi (Encapsulation): GheNgoi.CoTrong()

        // Dem ghe trong tren 1 toa (Encapsulation)
        public int DemGheTrong(ToaTau toa)
        {
            return toa.DemGheTrong(); // Goi phuong thuc cua doi tuong - Encapsulation
        }

        // Dem tong ghe trong tren 1 chuyen tau
        // Chuyen tu C++: tongGheTrong logic trong hienThiThongTinChuyen()
        public int DemTongGheTrong(ChuyenTau chuyen)
        {
            int tong = 0;
            foreach (var toa in chuyen.DanhSachToa)
            {
                tong += toa.DemGheTrong(); // Composition: ChuyenTau -> ToaTau -> GheNgoi
            }
            return tong;
        }

        // Dem tong ghe tren 1 chuyen tau
        public int DemTongGhe(ChuyenTau chuyen)
        {
            int tong = 0;
            foreach (var toa in chuyen.DanhSachToa)
            {
                tong += toa.DanhSachGhe.Count;
            }
            return tong;
        }

        // Kiem tra 1 ghe cu the co trong khong (Encapsulation)
        public bool KiemTraGheTrong(ToaTau toa, int maGhe)
        {
            var ghe = toa.TimGhe(maGhe);
            if (ghe == null) return false;
            return ghe.CoTrong(); // Dong goi - Encapsulation
        }

        // ==================== THEM CHUYEN TAU (ADMIN) ====================
        // Chuyen tu C++: HeThongDatVe::quanLyChuyenTau() case 2
        // Tu dong tao 10 toa khi them chuyen tau moi (nhu C++)
        public (bool thanhCong, string thongBao) ThemChuyenTau(
            GaTau gaDi, GaTau gaDen, DateTime thoiGianDi, DateTime thoiGianDen)
        {
            // Tao ma chuyen tu dong
            int maChuyen = _data.DanhSachChuyenTau.Count > 0 
                ? _data.DanhSachChuyenTau.Max(ct => ct.MaChuyen) + 1 
                : 1;

            var chuyen = new ChuyenTau(maChuyen, gaDi, gaDen, thoiGianDi, thoiGianDen);

            // Tao cac toa tau mac dinh (tuong tu C++: 5 toa ghe ngoi + 5 toa giuong nam)
            // Nhung tren web, toa duoc cau hinh linh hoat hon:
            var toaConfigs = new[]
            {
                ("T1", "Ngoi Cung",  40, 350000.0),
                ("T2", "Ngoi Mem",   40, 500000.0),
                ("T3", "Giuong Nam", 30, 750000.0),
                ("T4", "VIP",        20, 1200000.0)
            };

            foreach (var (maToa, loaiToa, soGhe, gia) in toaConfigs)
            {
                // Composition: ChuyenTau tao va chua cac ToaTau
                var toa = new ToaTau($"{maChuyen}-{maToa}", loaiToa, soGhe, gia);
                chuyen.ThemToa(toa); // Composition
            }

            _data.DanhSachChuyenTau.Add(chuyen);

            _data.GhiLichSu("Them Chuyen Tau", "Admin", 
                $"Them chuyen {maChuyen}: {gaDi.TenGa} -> {gaDen.TenGa}");

            return (true, $"Them chuyen tau {maChuyen} thanh cong!");
        }

        // ==================== XOA CHUYEN TAU (ADMIN) ====================
        // Chuyen tu C++: HeThongDatVe::quanLyChuyenTau() case 3
        // Kiem tra khong cho xoa neu con ve da dat (tuong tu C++)
        public (bool thanhCong, string thongBao) XoaChuyenTau(int maChuyen)
        {
            var chuyen = _data.DanhSachChuyenTau.FirstOrDefault(ct => ct.MaChuyen == maChuyen);
            if (chuyen == null)
                return (false, "Khong tim thay chuyen tau!");

            // Kiem tra con ve da dat khong (tuong tu C++: coVe check)
            bool coVe = _data.DanhSachVe.Any(v => 
                v.ChuyenTau?.MaChuyen == maChuyen && v.TrangThai == "Dang Hoat Dong");
            if (coVe)
                return (false, "Khong the xoa! Chuyen tau nay con ve da dat.");

            _data.DanhSachChuyenTau.Remove(chuyen);

            _data.GhiLichSu("Xoa Chuyen Tau", "Admin", $"Xoa chuyen {maChuyen}");

            return (true, $"Da xoa chuyen tau {maChuyen} thanh cong!");
        }
    }
}
