using ProjectTrainWeb.Models;
using System.Text.Json;

namespace ProjectTrainWeb.Services
{
    // TrainBookingService: Service quan ly toan bo du lieu va trang thai ung dung
    // Su dung tat ca cac Model OOP: NguoiDung, HanhKhach, QuanTriVien, ChuyenTau, VeTau, etc.
    // Dang ky vao DI container dang Scoped (moi session co 1 instance rieng)
    public class TrainBookingService
    {
        // ==================== DU LIEU HE THONG ====================
        
        // Danh sach ga tau
        public List<GaTau> DanhSachGa { get; set; } = new List<GaTau>();
        
        // Danh sach chuyen tau
        public List<ChuyenTau> DanhSachChuyenTau { get; set; } = new List<ChuyenTau>();
        
        // Danh sach nguoi dung (HanhKhach va QuanTriVien - Ke thua tu NguoiDung)
        public List<HanhKhach> DanhSachHanhKhach { get; set; } = new List<HanhKhach>();
        public List<QuanTriVien> DanhSachQuanTri { get; set; } = new List<QuanTriVien>();
        
        // Danh sach ve tau (VeThuong va VeVIP - Ke thua tu VeTau, Da hinh)
        public List<VeTau> DanhSachVe { get; set; } = new List<VeTau>();
        
        // Danh sach giao dich
        public List<GiaoDich> DanhSachGiaoDich { get; set; } = new List<GiaoDich>();
        
        // Danh sach khuyen mai
        public List<KhuyenMai> DanhSachKhuyenMai { get; set; } = new List<KhuyenMai>();
        
        // Danh sach thong bao
        public List<ThongBao> DanhSachThongBao { get; set; } = new List<ThongBao>();
        
        // Lich su he thong
        public List<LichSuHeThong> DanhSachLichSu { get; set; } = new List<LichSuHeThong>();

        // ==================== TRANG THAI DANG NHAP ====================
        
        // Nguoi dung hien tai (co the la HanhKhach hoac QuanTriVien - Tinh da hinh)
        public HanhKhach? HanhKhachHienTai { get; set; }
        public QuanTriVien? QuanTriVienHienTai { get; set; }
        public bool DaDangNhap => HanhKhachHienTai != null || QuanTriVienHienTai != null;
        public bool LaQuanTri => QuanTriVienHienTai != null;
        
        public string TenHienThi => HanhKhachHienTai?.HoTen ?? QuanTriVienHienTai?.HoTen ?? "";

        // Event thong bao state thay doi (dang nhap/dang xuat) de UI cap nhat
        public event Action? OnStateChanged;
        public void ThongBaoStateChanged() => OnStateChanged?.Invoke();

        // ==================== TRANG THAI DAT VE ====================
        
        // Chuyen tau da chon
        public ChuyenTau? ChuyenTauDaChon { get; set; }
        
        // Ghe da chon
        public List<GheNgoi> GheDaChon { get; set; } = new List<GheNgoi>();
        
        // Toa da chon
        public ToaTau? ToaDaChon { get; set; }
        
        // Loai ve da chon (ordinary/vip)
        public string LoaiVeDaChon { get; set; } = "ordinary";

        // ==================== KHOI TAO DU LIEU MAU ====================
        
        // File luu tru tai khoan
        private static readonly string DATA_DIR = Path.Combine(AppContext.BaseDirectory, "Data");
        private static readonly string ACCOUNTS_FILE = Path.Combine(DATA_DIR, "accounts.json");

        public TrainBookingService()
        {
            KhoiTaoDuLieuMau();
            LoadSavedAccounts(); // Doc tai khoan da luu tu file
        }

        private void KhoiTaoDuLieuMau()
        {
            // --- Khoi tao ga tau ---
            DanhSachGa = new List<GaTau>
            {
                new GaTau(1, "Ga Ha Noi", "Ha Noi"),
                new GaTau(2, "Ga Sai Gon", "TP. Ho Chi Minh"),
                new GaTau(3, "Ga Da Nang", "Da Nang"),
                new GaTau(4, "Ga Hue", "Hue"),
                new GaTau(5, "Ga Nha Trang", "Nha Trang"),
                new GaTau(6, "Ga Hai Phong", "Hai Phong"),
                new GaTau(7, "Ga Can Tho", "Can Tho"),
                new GaTau(8, "Ga Vinh", "Vinh")
            };

            // --- Khoi tao chuyen tau (Su dung Composition: ChuyenTau chua List<ToaTau>) ---
            var now = DateTime.Now;

            var chuyen1 = new ChuyenTau(1, DanhSachGa[0], DanhSachGa[1], 
                now.AddHours(2), now.AddHours(35));
            ThemToaChoChuyen(chuyen1, new[] { ("T1", "Ngoi Cung", 40, 350000.0), ("T2", "Ngoi Mem", 40, 500000.0), ("T3", "Giuong Nam", 30, 750000.0), ("T4", "VIP", 20, 1200000.0) });

            var chuyen2 = new ChuyenTau(2, DanhSachGa[0], DanhSachGa[2],
                now.AddHours(5), now.AddHours(22));
            ThemToaChoChuyen(chuyen2, new[] { ("T1", "Ngoi Cung", 40, 280000.0), ("T2", "Ngoi Mem", 40, 420000.0), ("T3", "Giuong Nam", 30, 600000.0) });

            var chuyen3 = new ChuyenTau(3, DanhSachGa[0], DanhSachGa[3],
                now.AddHours(8), now.AddHours(22));
            ThemToaChoChuyen(chuyen3, new[] { ("T1", "Ngoi Cung", 40, 200000.0), ("T2", "Ngoi Mem", 40, 320000.0), ("T3", "Giuong Nam", 30, 480000.0) });

            var chuyen4 = new ChuyenTau(4, DanhSachGa[2], DanhSachGa[1],
                now.AddHours(3), now.AddHours(21));
            ThemToaChoChuyen(chuyen4, new[] { ("T1", "Ngoi Cung", 40, 300000.0), ("T2", "Ngoi Mem", 40, 450000.0), ("T3", "VIP", 20, 900000.0) });

            var chuyen5 = new ChuyenTau(5, DanhSachGa[1], DanhSachGa[4],
                now.AddHours(6), now.AddHours(14));
            ThemToaChoChuyen(chuyen5, new[] { ("T1", "Ngoi Cung", 40, 180000.0), ("T2", "Ngoi Mem", 40, 280000.0), ("T3", "Giuong Nam", 30, 400000.0) });

            var chuyen6 = new ChuyenTau(6, DanhSachGa[0], DanhSachGa[5],
                now.AddHours(4), now.AddHours(7));
            ThemToaChoChuyen(chuyen6, new[] { ("T1", "Ngoi Cung", 40, 120000.0), ("T2", "Ngoi Mem", 40, 180000.0) });

            var chuyen7 = new ChuyenTau(7, DanhSachGa[3], DanhSachGa[2],
                now.AddHours(10), now.AddHours(14));
            ThemToaChoChuyen(chuyen7, new[] { ("T1", "Ngoi Cung", 40, 150000.0), ("T2", "Ngoi Mem", 40, 220000.0), ("T3", "VIP", 20, 500000.0) });

            DanhSachChuyenTau = new List<ChuyenTau> { chuyen1, chuyen2, chuyen3, chuyen4, chuyen5, chuyen6, chuyen7 };

            // --- Khoi tao tai khoan mau ---
            // HanhKhach ke thua tu NguoiDung (Inheritance)
            var hk1 = new HanhKhach(1, "user1", "123456", "Nguyen Van A", "0901234567");
            var hk2 = new HanhKhach(2, "user2", "123456", "Tran Thi B", "0912345678");
            DanhSachHanhKhach = new List<HanhKhach> { hk1, hk2 };

            // QuanTriVien ke thua tu NguoiDung (Inheritance)
            var admin1 = new QuanTriVien(100, "admin", "admin123", "Admin He Thong", 2);
            DanhSachQuanTri = new List<QuanTriVien> { admin1 };

            // --- Khoi tao khuyen mai ---
            DanhSachKhuyenMai = new List<KhuyenMai>
            {
                new KhuyenMai("NEWTRAVEL2026", 20, 200000, DateTime.Now.AddMonths(3), 100),
                new KhuyenMai("SUMMER10", 10, 100000, DateTime.Now.AddMonths(1), 50),
                new KhuyenMai("VIP30", 30, 500000, DateTime.Now.AddMonths(2), 20)
            };

            // --- Khoi tao thong bao mau ---
            DanhSachThongBao = new List<ThongBao>
            {
                new ThongBao(Guid.NewGuid().ToString(), "Chao mung ban den voi ProjectTrain! Dat ve ngay de nhan uu dai giam 20%.", "Email", "", ""),
                new ThongBao(Guid.NewGuid().ToString(), "Khuyen mai mua he: Giam 10% cho tat ca cac chuyen tau. Ma: SUMMER10", "Email", "", "")
            };
        }

        private void ThemToaChoChuyen(ChuyenTau chuyen, (string maToa, string loaiToa, int soGhe, double gia)[] toaConfigs)
        {
            foreach (var (maToa, loaiToa, soGhe, gia) in toaConfigs)
            {
                var toa = new ToaTau($"{chuyen.MaChuyen}-{maToa}", loaiToa, soGhe, gia);
                // Gia lap dat truoc mot so ghe
                var random = new Random(chuyen.MaChuyen * 100 + int.Parse(maToa.Replace("T", "")));
                foreach (var ghe in toa.DanhSachGhe)
                {
                    if (random.Next(100) < 25) // 25% ghe da dat
                    {
                        ghe.DatGhe();
                    }
                }
                chuyen.ThemToa(toa);
            }
        }

        // ==================== CHUC NANG DANG NHAP / DANG KY ====================

        // Dang nhap - Su dung tinh da hinh (Polymorphism): kiem tra ca HanhKhach va QuanTriVien
        public (bool thanhCong, string thongBao) DangNhap(string taiKhoan, string matKhau)
        {
            // Kiem tra trong danh sach quan tri vien TRUOC
            // (Quan trong: neu kiem tra HanhKhach truoc, admin-as-HanhKhach se duoc tim thay
            //  va QuanTriVienHienTai se = null → mat quyen admin)
            var admin = DanhSachQuanTri.FirstOrDefault(a => a.TaiKhoan == taiKhoan && a.MatKhau == matKhau);
            if (admin != null)
            {
                QuanTriVienHienTai = admin;
                // Tao HanhKhach tam tu thong tin admin de admin cung co the dat ve
                HanhKhachHienTai = DanhSachHanhKhach.FirstOrDefault(h => h.TaiKhoan == admin.TaiKhoan);
                if (HanhKhachHienTai == null)
                {
                    // Tao hanh khach tam cho admin
                    var adminAsHK = new HanhKhach(admin.MaNguoiDung, admin.TaiKhoan, admin.MatKhau, admin.HoTen, "0000000000");
                    DanhSachHanhKhach.Add(adminAsHK);
                    HanhKhachHienTai = adminAsHK;
                }
                GhiLichSu("Dang Nhap", admin.HoTen, $"Quan tri vien {admin.HoTen} da dang nhap");
                ThongBaoStateChanged();
                return (true, $"Chao mung Quan tri vien {admin.HoTen}!");
            }

            // Kiem tra trong danh sach hanh khach
            var hk = DanhSachHanhKhach.FirstOrDefault(h => h.TaiKhoan == taiKhoan && h.MatKhau == matKhau);
            if (hk != null)
            {
                HanhKhachHienTai = hk;
                QuanTriVienHienTai = null;
                GhiLichSu("Dang Nhap", hk.HoTen, $"Hanh khach {hk.HoTen} da dang nhap");
                ThongBaoStateChanged();
                return (true, $"Chao mung {hk.HoTen}!");
            }

            return (false, "Tai khoan hoac mat khau khong dung!");
        }

        // Dang ky - Tao doi tuong HanhKhach (Ke thua tu NguoiDung)
        public (bool thanhCong, string thongBao) DangKy(string taiKhoan, string matKhau, string hoTen, string soDienThoai)
        {
            if (string.IsNullOrWhiteSpace(taiKhoan) || string.IsNullOrWhiteSpace(matKhau) || 
                string.IsNullOrWhiteSpace(hoTen))
            {
                return (false, "Vui long nhap day du thong tin!");
            }

            if (DanhSachHanhKhach.Any(h => h.TaiKhoan == taiKhoan) || 
                DanhSachQuanTri.Any(a => a.TaiKhoan == taiKhoan))
            {
                return (false, "Tai khoan da ton tai!");
            }

            int newId = DanhSachHanhKhach.Count + DanhSachQuanTri.Count + 1;
            var hanhKhach = new HanhKhach(newId, taiKhoan, matKhau, hoTen, soDienThoai);
            DanhSachHanhKhach.Add(hanhKhach);

            // Tao thong bao chao mung
            var tb = new ThongBao(Guid.NewGuid().ToString(),
                $"Chao mung {hoTen} da dang ky thanh cong!",
                "Email", newId.ToString(), "");
            tb.DanhDauDaGui();
            DanhSachThongBao.Add(tb);

            GhiLichSu("Dang Ky", hoTen, $"Hanh khach {hoTen} da dang ky tai khoan");

            // Luu tai khoan vao file
            SaveAccounts();

            return (true, "Dang ky thanh cong! Ban co the dang nhap ngay.");
        }

        // Dang xuat
        public void DangXuat()
        {
            string ten = TenHienThi;
            GhiLichSu("Dang Xuat", ten, $"{ten} da dang xuat");
            HanhKhachHienTai = null;
            QuanTriVienHienTai = null;
            ChuyenTauDaChon = null;
            GheDaChon.Clear();
            ToaDaChon = null;
            ThongBaoStateChanged();
        }

        // ==================== CHUC NANG DAT VE ====================

        // Dat ve - Su dung Da hinh: tao VeThuong hoac VeVIP tuy theo loai
        public (bool thanhCong, string thongBao, VeTau? ve) DatVe(string loaiVe = "ordinary")
        {
            if (HanhKhachHienTai == null)
                return (false, "Vui long dang nhap truoc khi dat ve!", null);
            
            if (ChuyenTauDaChon == null)
                return (false, "Vui long chon chuyen tau!", null);
            
            if (GheDaChon.Count == 0)
                return (false, "Vui long chon it nhat 1 ghe!", null);

            var danhSachVeMoi = new List<VeTau>();

            foreach (var ghe in GheDaChon)
            {
                // Su dung Da hinh (Polymorphism): Tao VeThuong hoac VeVIP
                VeTau ve;
                if (loaiVe == "vip")
                {
                    ve = new VeVIP(Guid.NewGuid().ToString(), ChuyenTauDaChon, HanhKhachHienTai,
                        ToaDaChon?.MaToa ?? "", ghe.MaGhe, ghe.GiaCoBan);
                }
                else
                {
                    ve = new VeThuong(Guid.NewGuid().ToString(), ChuyenTauDaChon, HanhKhachHienTai,
                        ToaDaChon?.MaToa ?? "", ghe.MaGhe, ghe.GiaCoBan);
                }

                // Dat ghe (Encapsulation)
                ghe.DatGhe();
                
                DanhSachVe.Add(ve);
                danhSachVeMoi.Add(ve);
            }

            // Tinh tong tien su dung Da hinh: TinhGiaVe() khac nhau cho VeThuong va VeVIP
            double tongTien = danhSachVeMoi.Sum(v => v.TinhGiaVe());

            // Tao giao dich
            var giaoDich = new GiaoDich(Guid.NewGuid().ToString(), tongTien, "Vi He Thong",
                "Thanh Toan", HanhKhachHienTai.MaNguoiDung.ToString(), danhSachVeMoi.First().MaVe);
            giaoDich.XacNhanThanhCong();
            DanhSachGiaoDich.Add(giaoDich);

            // Tao thong bao
            var thongBao = new ThongBao(Guid.NewGuid().ToString(),
                $"Ban da dat thanh cong {GheDaChon.Count} ve tau. Ma ve: {danhSachVeMoi.First().MaVe.Substring(0, 8)}. Tong tien: {tongTien:N0} VND",
                "Email", HanhKhachHienTai.MaNguoiDung.ToString(), danhSachVeMoi.First().MaVe);
            thongBao.DanhDauDaGui();
            DanhSachThongBao.Add(thongBao);

            // Ghi lich su
            GhiLichSu("Dat Ve", HanhKhachHienTai.HoTen,
                $"Dat {GheDaChon.Count} ve, tong {tongTien:N0} VND, chuyen {ChuyenTauDaChon.MaChuyen}");

            // Reset trang thai
            GheDaChon.Clear();

            return (true, $"Dat ve thanh cong! Tong tien: {tongTien:N0} VND", danhSachVeMoi.First());
        }

        // Huy ve - Su dung Polymorphism: VeTau.HuyVe()
        public (bool thanhCong, string thongBao) HuyVe(string maVe)
        {
            var ve = DanhSachVe.FirstOrDefault(v => v.MaVe == maVe);
            if (ve == null)
                return (false, "Khong tim thay ve!");

            if (ve.TrangThai == "Da Huy")
                return (false, "Ve nay da bi huy truoc do!");

            // Huy ve (Polymorphism)
            ve.HuyVe();

            // Huy ghe
            if (ve.ChuyenTau != null)
            {
                foreach (var toa in ve.ChuyenTau.DanhSachToa)
                {
                    var ghe = toa.TimGhe(ve.ViTriGhe);
                    if (ghe != null && toa.MaToa == ve.MaToa)
                    {
                        ghe.HuyGhe();
                    }
                }
            }

            // Tao giao dich hoan tien
            double giaVeHuy = ve.TinhGiaVe();
            var giaoDich = new GiaoDich(Guid.NewGuid().ToString(), giaVeHuy, "Hoan Tien",
                "Hoan Tien", HanhKhachHienTai?.MaNguoiDung.ToString() ?? "", maVe);
            giaoDich.XacNhanThanhCong();
            DanhSachGiaoDich.Add(giaoDich);

            GhiLichSu("Huy Ve", HanhKhachHienTai?.HoTen ?? "He Thong",
                $"Huy ve {maVe.Substring(0, 8)}");

            return (true, "Huy ve thanh cong!");
        }

        // ==================== CHUC NANG TIM KIEM ====================

        // Tim chuyen tau theo ga di va ga den
        public List<ChuyenTau> TimChuyenTau(int? maGaDi = null, int? maGaDen = null)
        {
            var result = DanhSachChuyenTau.AsEnumerable();

            if (maGaDi.HasValue && maGaDi > 0)
                result = result.Where(ct => ct.GaDi?.MaGa == maGaDi);

            if (maGaDen.HasValue && maGaDen > 0)
                result = result.Where(ct => ct.GaDen?.MaGa == maGaDen);

            return result.Where(ct => ct.ConHopLe()).ToList();
        }

        // Tra cuu ve theo ma ve
        public VeTau? TraCuuVe(string maVe)
        {
            return DanhSachVe.FirstOrDefault(v => v.MaVe.Contains(maVe, StringComparison.OrdinalIgnoreCase));
        }

        // Lay ve cua hanh khach hien tai
        public List<VeTau> LayVeCuaToi()
        {
            if (HanhKhachHienTai == null) return new List<VeTau>();
            return DanhSachVe.Where(v => v.HanhKhach?.MaNguoiDung == HanhKhachHienTai.MaNguoiDung).ToList();
        }

        // ==================== CHUC NANG KHUYEN MAI ====================

        // Ap dung ma khuyen mai
        public (bool thanhCong, double tienGiam, string thongBao) ApDungKhuyenMai(string maCode, double giaGoc)
        {
            var km = DanhSachKhuyenMai.FirstOrDefault(k => 
                k.MaCode.Equals(maCode, StringComparison.OrdinalIgnoreCase));
            
            if (km == null)
                return (false, 0, "Ma khuyen mai khong ton tai!");

            if (!km.ConHieuLuc())
                return (false, 0, "Ma khuyen mai da het han hoac het luot su dung!");

            double tienGiam = km.TinhSoTienGiam(giaGoc);
            km.SuDung();

            return (true, tienGiam, $"Ap dung thanh cong! Giam {tienGiam:N0} VND");
        }

        // ==================== CHUC NANG THONG BAO ====================

        // Lay thong bao cua nguoi dung hien tai
        public List<ThongBao> LayThongBaoCuaToi()
        {
            if (HanhKhachHienTai == null) return DanhSachThongBao.Where(t => string.IsNullOrEmpty(t.MaHanhKhach)).ToList();
            return DanhSachThongBao.Where(t => 
                t.MaHanhKhach == HanhKhachHienTai.MaNguoiDung.ToString() || string.IsNullOrEmpty(t.MaHanhKhach)).ToList();
        }

        // Dem thong bao chua gui
        public int DemThongBaoChuaDoc()
        {
            return LayThongBaoCuaToi().Count(t => t.TrangThai == "Chua Gui");
        }

        // ==================== CHUC NANG LICH SU ====================

        // Ghi lich su he thong
        public void GhiLichSu(string hanhDong, string nguoiThucHien, string chiTiet = "")
        {
            var lichSu = new LichSuHeThong(Guid.NewGuid().ToString(), hanhDong, nguoiThucHien, chiTiet);
            DanhSachLichSu.Add(lichSu);
        }

        // Lay lich su cua nguoi dung hien tai
        public List<LichSuHeThong> LayLichSuCuaToi()
        {
            string ten = TenHienThi;
            if (string.IsNullOrEmpty(ten)) return new List<LichSuHeThong>();
            return DanhSachLichSu.Where(l => l.NguoiThucHien == ten).OrderByDescending(l => l.ThoiGian).ToList();
        }

        // ==================== CHUC NANG QUAN TRI ====================

        // Thong ke cho admin
        public (int tongVe, int tongHanhKhach, double tongDoanhThu, int tongChuyenTau) LayThongKe()
        {
            return (
                DanhSachVe.Count,
                DanhSachHanhKhach.Count,
                DanhSachGiaoDich.Where(g => g.LoaiGiaoDich == "Thanh Toan" && g.TrangThai == "Thanh Cong").Sum(g => g.SoTien),
                DanhSachChuyenTau.Count
            );
        }

        // ==================== LUU TRU TAI KHOAN (FILE JSON) ====================

        private class AccountData
        {
            public List<AccountEntry> HanhKhach { get; set; } = new();
            public List<AdminEntry> QuanTriVien { get; set; } = new();
        }

        private class AccountEntry
        {
            public int Id { get; set; }
            public string TaiKhoan { get; set; } = "";
            public string MatKhau { get; set; } = "";
            public string HoTen { get; set; } = "";
            public string SoDienThoai { get; set; } = "";
        }

        private class AdminEntry
        {
            public int Id { get; set; }
            public string TaiKhoan { get; set; } = "";
            public string MatKhau { get; set; } = "";
            public string HoTen { get; set; } = "";
            public int MucCapQuyen { get; set; }
        }

        // Doc tai khoan da luu tu file JSON
        private void LoadSavedAccounts()
        {
            try
            {
                if (!File.Exists(ACCOUNTS_FILE)) return;

                var json = File.ReadAllText(ACCOUNTS_FILE);
                var data = JsonSerializer.Deserialize<AccountData>(json);
                if (data == null) return;

                // Them cac tai khoan da luu (khong trung voi tai khoan mac dinh)
                foreach (var entry in data.HanhKhach)
                {
                    if (!DanhSachHanhKhach.Any(h => h.TaiKhoan == entry.TaiKhoan))
                    {
                        var hk = new HanhKhach(entry.Id, entry.TaiKhoan, entry.MatKhau, entry.HoTen, entry.SoDienThoai);
                        DanhSachHanhKhach.Add(hk);
                    }
                }

                foreach (var entry in data.QuanTriVien)
                {
                    if (!DanhSachQuanTri.Any(a => a.TaiKhoan == entry.TaiKhoan))
                    {
                        var admin = new QuanTriVien(entry.Id, entry.TaiKhoan, entry.MatKhau, entry.HoTen, entry.MucCapQuyen);
                        DanhSachQuanTri.Add(admin);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi doc file tai khoan: {ex.Message}");
            }
        }

        // Luu tat ca tai khoan vao file JSON
        public void SaveAccounts()
        {
            try
            {
                Directory.CreateDirectory(DATA_DIR);

                var data = new AccountData
                {
                    HanhKhach = DanhSachHanhKhach.Select(h => new AccountEntry
                    {
                        Id = h.MaNguoiDung,
                        TaiKhoan = h.TaiKhoan,
                        MatKhau = h.MatKhau,
                        HoTen = h.HoTen,
                        SoDienThoai = h.SoDienThoai
                    }).ToList(),
                    QuanTriVien = DanhSachQuanTri.Select(a => new AdminEntry
                    {
                        Id = a.MaNguoiDung,
                        TaiKhoan = a.TaiKhoan,
                        MatKhau = a.MatKhau,
                        HoTen = a.HoTen,
                        MucCapQuyen = a.MucCapQuyen
                    }).ToList()
                };

                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ACCOUNTS_FILE, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi luu file tai khoan: {ex.Message}");
            }
        }

        // Xoa tai khoan hanh khach (Admin)
        public (bool thanhCong, string thongBao) XoaTaiKhoan(string taiKhoan)
        {
            var hk = DanhSachHanhKhach.FirstOrDefault(h => h.TaiKhoan == taiKhoan);
            if (hk == null)
                return (false, "Khong tim thay tai khoan!");

            // Khong cho xoa tai khoan dang dang nhap
            if (HanhKhachHienTai?.TaiKhoan == taiKhoan)
                return (false, "Khong the xoa tai khoan dang dang nhap!");

            DanhSachHanhKhach.Remove(hk);
            GhiLichSu("Xoa Tai Khoan", "Admin", $"Da xoa tai khoan {hk.HoTen} ({taiKhoan})");
            SaveAccounts();
            return (true, $"Da xoa tai khoan {hk.HoTen}.");
        }
    }
}
