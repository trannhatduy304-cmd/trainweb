using ProjectTrainWeb.Models;

namespace ProjectTrainWeb.Services
{
    // NguoiDungService: Quan ly dang nhap, dang ky, nap tien
    // Chuyen tu C++: NguoiDung.cpp + phan dang nhap/dang ky trong HeThongDatVe.cpp
    // OOP: Su dung Ke thua (HanhKhach/QuanTriVien : NguoiDung)
    //       va Da hinh (kiem tra ca 2 loai nguoi dung khi dang nhap)
    public class NguoiDungService
    {
        private readonly TrainBookingService _data;

        // Inject TrainBookingService de truy cap du lieu chung (DI - Dependency Injection)
        public NguoiDungService(TrainBookingService data)
        {
            _data = data;
        }

        // ==================== TRANG THAI DANG NHAP ====================

        // Nguoi dung hien tai (co the la HanhKhach hoac QuanTriVien - Da hinh)
        public HanhKhach? HanhKhachHienTai
        {
            get => _data.HanhKhachHienTai;
            set => _data.HanhKhachHienTai = value;
        }

        public QuanTriVien? QuanTriVienHienTai
        {
            get => _data.QuanTriVienHienTai;
            set => _data.QuanTriVienHienTai = value;
        }

        // Computed properties (tuong tu C++: userHienTai != nullptr)
        public bool DaDangNhap => _data.DaDangNhap;
        public bool LaQuanTri => _data.LaQuanTri;
        public string TenHienThi => _data.TenHienThi;

        // ==================== DANG NHAP ====================
        // Chuyen tu C++: HeThongDatVe::dangNhap()
        // Su dung Da hinh: kiem tra ca HanhKhach va QuanTriVien (ke thua tu NguoiDung)
        public (bool thanhCong, string thongBao) DangNhap(string taiKhoan, string matKhau)
        {
            // Kiem tra trong danh sach hanh khach (HanhKhach ke thua NguoiDung)
            var hk = _data.DanhSachHanhKhach.FirstOrDefault(h => 
                h.TaiKhoan == taiKhoan && h.MatKhau == matKhau);
            if (hk != null)
            {
                HanhKhachHienTai = hk;
                QuanTriVienHienTai = null;
                _data.GhiLichSu("Dang Nhap", hk.HoTen, $"Hanh khach {hk.HoTen} da dang nhap");
                return (true, $"Chao mung {hk.HoTen}!");
            }

            // Kiem tra trong danh sach quan tri vien (QuanTriVien ke thua NguoiDung)
            var admin = _data.DanhSachQuanTri.FirstOrDefault(a => 
                a.TaiKhoan == taiKhoan && a.MatKhau == matKhau);
            if (admin != null)
            {
                QuanTriVienHienTai = admin;
                HanhKhachHienTai = null;
                _data.GhiLichSu("Dang Nhap", admin.HoTen, $"Quan tri vien {admin.HoTen} da dang nhap");
                return (true, $"Chao mung Quan tri vien {admin.HoTen}!");
            }

            return (false, "Tai khoan hoac mat khau khong dung!");
        }

        // ==================== DANG KY ====================
        // Chuyen tu C++: HeThongDatVe::dangKyHanhKhach()
        // Tao doi tuong HanhKhach (Ke thua tu NguoiDung - Inheritance)
        public (bool thanhCong, string thongBao) DangKy(string taiKhoan, string matKhau, 
            string hoTen, string soDienThoai)
        {
            if (string.IsNullOrWhiteSpace(taiKhoan) || string.IsNullOrWhiteSpace(matKhau) || 
                string.IsNullOrWhiteSpace(hoTen))
            {
                return (false, "Vui long nhap day du thong tin!");
            }

            // Kiem tra trung tai khoan (tuong tu C++: kiem tra adminTaiKhoan va dsHanhKhach)
            if (_data.DanhSachHanhKhach.Any(h => h.TaiKhoan == taiKhoan) || 
                _data.DanhSachQuanTri.Any(a => a.TaiKhoan == taiKhoan))
            {
                return (false, "Tai khoan da ton tai!");
            }

            int newId = _data.DanhSachHanhKhach.Count + _data.DanhSachQuanTri.Count + 1;
            var hanhKhach = new HanhKhach(newId, taiKhoan, matKhau, hoTen, soDienThoai);
            _data.DanhSachHanhKhach.Add(hanhKhach);

            // Tao thong bao chao mung
            var tb = new ThongBao(Guid.NewGuid().ToString(),
                $"Chao mung {hoTen} da dang ky thanh cong!",
                "Email", newId.ToString(), "");
            tb.DanhDauDaGui();
            _data.DanhSachThongBao.Add(tb);

            _data.GhiLichSu("Dang Ky", hoTen, $"Hanh khach {hoTen} da dang ky tai khoan");

            return (true, "Dang ky thanh cong! Ban co the dang nhap ngay.");
        }

        // ==================== DANG XUAT ====================
        // Chuyen tu C++: userHienTai = nullptr
        public void DangXuat()
        {
            string ten = TenHienThi;
            _data.GhiLichSu("Dang Xuat", ten, $"{ten} da dang xuat");
            HanhKhachHienTai = null;
            QuanTriVienHienTai = null;
            _data.ChuyenTauDaChon = null;
            _data.GheDaChon.Clear();
            _data.ToaDaChon = null;
        }

        // ==================== LAY THONG TIN ====================

        // Lay danh sach hanh khach (cho Admin xem)
        public List<HanhKhach> LayDanhSachHanhKhach()
        {
            return _data.DanhSachHanhKhach;
        }
    }
}
