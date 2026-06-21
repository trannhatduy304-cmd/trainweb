using ProjectTrainWeb.Models;

namespace ProjectTrainWeb.Services
{
    // ThanhToanService: Xu ly thanh toan ve, kiem tra qua han, khuyen mai
    // Chuyen tu C++: Logic thanh toan ve chua TT (menu [7]) va hoanLaiVeQuaHan()
    //                trong HeThongDatVe.cpp
    // OOP: Da hinh (Polymorphism) - TinhGiaVe() khac nhau cho VeThuong va VeVIP
    //       Pattern matching (C# is) - Thay the dynamic_cast cua C++
    public class ThanhToanService
    {
        private readonly TrainBookingService _data;

        public ThanhToanService(TrainBookingService data)
        {
            _data = data;
        }

        // ==================== THANH TOAN VE ====================
        // Chuyen tu C++: HeThongDatVe::menuHanhKhach() case 7
        // "Thanh toan ve chua thanh toan"
        public (bool thanhCong, string thongBao) ThanhToanVe(string maVe)
        {
            if (_data.HanhKhachHienTai == null)
                return (false, "Vui long dang nhap truoc!");

            var ve = _data.DanhSachVe.FirstOrDefault(v => v.MaVe == maVe);
            if (ve == null)
                return (false, "Khong tim thay ve!");

            if (ve.DaThanhToan)
                return (false, "Ve nay da duoc thanh toan truoc do!");

            if (ve.TrangThai == "Da Huy")
                return (false, "Ve nay da bi huy, khong the thanh toan!");

            // Da hinh (Polymorphism): TinhGiaVe() tra ve gia khac nhau
            // cho VeThuong (= GiaCoBan) va VeVIP (= GiaCoBan * 1.3)
            double giaVe = ve.TinhGiaVe();

            // Danh dau da thanh toan (Dong goi)
            ve.ThanhToanVe();

            // Tao giao dich
            var giaoDich = new GiaoDich(Guid.NewGuid().ToString(), giaVe, "Chuyen Khoan",
                "Thanh Toan", _data.HanhKhachHienTai.MaNguoiDung.ToString(), maVe);
            giaoDich.XacNhanThanhCong();
            _data.DanhSachGiaoDich.Add(giaoDich);

            _data.GhiLichSu("Thanh Toan Ve", _data.HanhKhachHienTai.HoTen,
                $"Thanh toan ve {maVe.Substring(0, 8)}: {giaVe:N0} VND");

            return (true, $"Thanh toan thanh cong! So tien: {giaVe:N0} VND.");
        }

        // ==================== THANH TOAN NHIEU VE ====================
        public (bool thanhCong, string thongBao) ThanhToanNhieuVe(List<string> danhSachMaVe)
        {
            if (_data.HanhKhachHienTai == null)
                return (false, "Vui long dang nhap truoc!");

            double tongTien = 0;
            int soVeThanhToan = 0;

            foreach (var maVe in danhSachMaVe)
            {
                var ve = _data.DanhSachVe.FirstOrDefault(v => v.MaVe == maVe);
                if (ve != null && !ve.DaThanhToan && ve.TrangThai == "Dang Hoat Dong")
                {
                    tongTien += ve.TinhGiaVe(); // Da hinh
                    soVeThanhToan++;
                }
            }

            if (soVeThanhToan == 0)
                return (false, "Khong co ve nao can thanh toan!");

            // Danh dau thanh toan
            foreach (var maVe in danhSachMaVe)
            {
                var ve = _data.DanhSachVe.FirstOrDefault(v => v.MaVe == maVe);
                if (ve != null && !ve.DaThanhToan && ve.TrangThai == "Dang Hoat Dong")
                {
                    ve.ThanhToanVe();
                }
            }

            return (true, $"Thanh toan thanh cong {soVeThanhToan} ve! " +
                $"Tong: {tongTien:N0} VND.");
        }

        // ==================== LAY VE CHUA THANH TOAN ====================
        // Chuyen tu C++: Logic loc ve chua TT trong menuHanhKhach() case 7
        public List<VeTau> LayVeChuaThanhToan()
        {
            if (_data.HanhKhachHienTai == null) return new List<VeTau>();

            return _data.DanhSachVe.Where(v => 
                v.HanhKhach?.MaNguoiDung == _data.HanhKhachHienTai.MaNguoiDung &&
                !v.DaThanhToan &&
                v.TrangThai == "Dang Hoat Dong").ToList();
        }

        // ==================== KIEM TRA VE QUA HAN ====================
        // Chuyen tu C++: HeThongDatVe::kiemTraVeQuaHan()
        // Tra ve danh sach ve da qua han 3 ngay chua thanh toan
        public List<VeTau> KiemTraVeQuaHan()
        {
            return _data.DanhSachVe.Where(v => v.KiemTraQuaHan()).ToList();
        }

        // ==================== TU DONG HUY VE QUA HAN ====================
        // Chuyen tu C++: HeThongDatVe::hoanLaiVeQuaHan()
        // Tu dong huy ve chua thanh toan qua 3 ngay va giai phong ghe
        public int HoanLaiVeQuaHan()
        {
            var veQuaHan = KiemTraVeQuaHan();
            int soVeHuy = 0;

            foreach (var ve in veQuaHan)
            {
                // Huy ve (Polymorphism - phuong thuc cua lop cha)
                ve.HuyVe();

                // Giai phong ghe (tuong tu C++: gheNgoi.setTrangThai(false))
                if (ve.ChuyenTau != null)
                {
                    foreach (var toa in ve.ChuyenTau.DanhSachToa)
                    {
                        if (toa.MaToa == ve.MaToa)
                        {
                            var ghe = toa.TimGhe(ve.ViTriGhe);
                            if (ghe != null)
                            {
                                ghe.HuyGhe(); // Dong goi - Encapsulation
                            }
                        }
                    }
                }

                // Tao thong bao cho hanh khach
                if (ve.HanhKhach != null)
                {
                    var tb = new ThongBao(Guid.NewGuid().ToString(),
                        $"Ve {ve.MaVe.Substring(0, 8)} da bi huy tu dong do qua 3 ngay chua thanh toan.",
                        "Email", ve.HanhKhach.MaNguoiDung.ToString(), ve.MaVe);
                    tb.DanhDauDaGui();
                    _data.DanhSachThongBao.Add(tb);
                }

                _data.GhiLichSu("Huy Ve Qua Han", "He Thong",
                    $"Tu dong huy ve {ve.MaVe.Substring(0, 8)} cua {ve.HanhKhach?.HoTen ?? "N/A"}");

                soVeHuy++;
            }

            return soVeHuy;
        }

        // ==================== AP DUNG KHUYEN MAI ====================
        // Chuyen tu C++: Logic khuyen mai (khong co trong C++ console, la tinh nang web moi)
        public (bool thanhCong, double tienGiam, string thongBao) ApDungKhuyenMai(
            string maCode, double giaGoc)
        {
            var km = _data.DanhSachKhuyenMai.FirstOrDefault(k => 
                k.MaCode.Equals(maCode, StringComparison.OrdinalIgnoreCase));
            
            if (km == null)
                return (false, 0, "Ma khuyen mai khong ton tai!");

            if (!km.ConHieuLuc())
                return (false, 0, "Ma khuyen mai da het han hoac het luot su dung!");

            // Tinh so tien giam (Dong goi - Encapsulation)
            double tienGiam = km.TinhSoTienGiam(giaGoc);
            km.SuDung(); // Giam so luong con lai

            _data.GhiLichSu("Ap Dung Khuyen Mai", 
                _data.HanhKhachHienTai?.HoTen ?? "Khach",
                $"Ma: {maCode}, giam {tienGiam:N0} VND");

            return (true, tienGiam, $"Ap dung thanh cong! Giam {tienGiam:N0} VND");
        }
    }
}
