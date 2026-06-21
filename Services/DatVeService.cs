using ProjectTrainWeb.Models;

namespace ProjectTrainWeb.Services
{
    // DatVeService: Xu ly dat ve va huy ve tau
    // Chuyen tu C++: datVeTau() va huyVeTau() trong HeThongDatVe.cpp
    // OOP: Da hinh (Polymorphism) - Tao VeThuong hoac VeVIP, goi TinhGiaVe() khac nhau
    //       Ke thua (Inheritance) - VeThuong/VeVIP ke thua VeTau
    //       Dong goi (Encapsulation) - GheNgoi.DatGhe()
    public class DatVeService
    {
        private readonly TrainBookingService _data;

        public DatVeService(TrainBookingService data)
        {
            _data = data;
        }

        // ==================== TRANG THAI CHON GHE ====================

        public ChuyenTau? ChuyenTauDaChon
        {
            get => _data.ChuyenTauDaChon;
            set => _data.ChuyenTauDaChon = value;
        }

        public ToaTau? ToaDaChon
        {
            get => _data.ToaDaChon;
            set => _data.ToaDaChon = value;
        }

        public List<GheNgoi> GheDaChon
        {
            get => _data.GheDaChon;
            set => _data.GheDaChon = value;
        }

        public string LoaiVeDaChon
        {
            get => _data.LoaiVeDaChon;
            set => _data.LoaiVeDaChon = value;
        }

        // ==================== DAT VE ====================
        // Chuyen tu C++: HeThongDatVe::datVeTau(HanhKhach* hk)
        // Da hinh (Polymorphism): Tao VeThuong hoac VeVIP tuy theo lua chon
        // Ke thua (Inheritance): Ca 2 lop con deu ke thua tu VeTau
        // Dong goi (Encapsulation): GheNgoi.DatGhe()
        public (bool thanhCong, string thongBao, VeTau? ve) DatVe(string loaiVe = "ordinary")
        {
            if (_data.HanhKhachHienTai == null)
                return (false, "Vui long dang nhap truoc khi dat ve!", null);
            
            if (ChuyenTauDaChon == null)
                return (false, "Vui long chon chuyen tau!", null);
            
            if (GheDaChon.Count == 0)
                return (false, "Vui long chon it nhat 1 ghe!", null);

            var danhSachVeMoi = new List<VeTau>();

            foreach (var ghe in GheDaChon)
            {
                // === DA HINH (Polymorphism) ===
                // Tao doi tuong ve dung loai (VeThuong hoac VeVIP)
                // Ca 2 deu ke thua tu lop truu tuong VeTau
                // Tuong tu C++: if (loaiVe == 1) ve = new VeThuong(...) else ve = new VeVIP(...)
                VeTau ve;
                if (loaiVe == "vip")
                {
                    // VeVIP ke thua VeTau, ghi de TinhGiaVe() = GiaCoBan * 1.3
                    ve = new VeVIP(Guid.NewGuid().ToString(), ChuyenTauDaChon, _data.HanhKhachHienTai,
                        ToaDaChon?.MaToa ?? "", ghe.MaGhe, ghe.GiaCoBan);
                }
                else
                {
                    // VeThuong ke thua VeTau, ghi de TinhGiaVe() = GiaCoBan
                    ve = new VeThuong(Guid.NewGuid().ToString(), ChuyenTauDaChon, _data.HanhKhachHienTai,
                        ToaDaChon?.MaToa ?? "", ghe.MaGhe, ghe.GiaCoBan);
                }

                // Dong goi (Encapsulation): Dat ghe thong qua phuong thuc cua doi tuong
                ghe.DatGhe();
                
                _data.DanhSachVe.Add(ve);
                danhSachVeMoi.Add(ve);
            }

            // === DA HINH (Polymorphism) ===
            // TinhGiaVe() duoc goi qua bien kieu VeTau (lop cha)
            // Nhung thuc te se chay phien ban cua VeThuong hoac VeVIP (lop con)
            // Tuong tu C++: double tongTien = danhSachVeMoi.Sum(v => v.tinhGiaVe())
            double tongTien = danhSachVeMoi.Sum(v => v.TinhGiaVe());

            // Tao giao dich
            var giaoDich = new GiaoDich(Guid.NewGuid().ToString(), tongTien, "Vi He Thong",
                "Thanh Toan", _data.HanhKhachHienTai.MaNguoiDung.ToString(), 
                danhSachVeMoi.First().MaVe);
            giaoDich.XacNhanThanhCong();
            _data.DanhSachGiaoDich.Add(giaoDich);

            // Danh dau da thanh toan cho tat ca ve moi (mac dinh thanh toan ngay)
            foreach (var ve in danhSachVeMoi)
            {
                ve.ThanhToanVe(); // Dong goi
            }

            // Tao thong bao
            var thongBao = new ThongBao(Guid.NewGuid().ToString(),
                $"Ban da dat thanh cong {GheDaChon.Count} ve tau. " +
                $"Ma ve: {danhSachVeMoi.First().MaVe.Substring(0, 8)}. " +
                $"Tong tien: {tongTien:N0} VND",
                "Email", _data.HanhKhachHienTai.MaNguoiDung.ToString(), 
                danhSachVeMoi.First().MaVe);
            thongBao.DanhDauDaGui();
            _data.DanhSachThongBao.Add(thongBao);

            // Ghi lich su
            _data.GhiLichSu("Dat Ve", _data.HanhKhachHienTai.HoTen,
                $"Dat {GheDaChon.Count} ve {(loaiVe == "vip" ? "VIP" : "Thuong")}, " +
                $"tong {tongTien:N0} VND, chuyen {ChuyenTauDaChon.MaChuyen}");

            // Reset trang thai chon ghe
            GheDaChon.Clear();

            return (true, $"Dat ve thanh cong! Tong tien: {tongTien:N0} VND", danhSachVeMoi.First());
        }

        // ==================== HUY VE ====================
        // Chuyen tu C++: HeThongDatVe::huyVeTau(HanhKhach* hk)
        // Da hinh: ve.TinhGiaVe() goi phien ban cua VeThuong hoac VeVIP
        // Dong goi: ghe.HuyGhe()
        public (bool thanhCong, string thongBao) HuyVe(string maVe)
        {
            var ve = _data.DanhSachVe.FirstOrDefault(v => v.MaVe == maVe);
            if (ve == null)
                return (false, "Khong tim thay ve!");

            if (ve.TrangThai == "Da Huy")
                return (false, "Ve nay da bi huy truoc do!");

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

            // Tao giao dich hoan tien
            if (ve.DaThanhToan)
            {
                double giaVeHuy = ve.TinhGiaVe();
                var giaoDich = new GiaoDich(Guid.NewGuid().ToString(), giaVeHuy, "Hoan Tien",
                    "Hoan Tien", _data.HanhKhachHienTai?.MaNguoiDung.ToString() ?? "", maVe);
                giaoDich.XacNhanThanhCong();
                _data.DanhSachGiaoDich.Add(giaoDich);
            }

            _data.GhiLichSu("Huy Ve", _data.HanhKhachHienTai?.HoTen ?? "He Thong",
                $"Huy ve {maVe.Substring(0, 8)}");

            return (true, "Huy ve thanh cong!");
        }

        // ==================== TRA CUU VE ====================

        // Tra cuu ve theo ma ve
        public VeTau? TraCuuVe(string maVe)
        {
            return _data.DanhSachVe.FirstOrDefault(v => 
                v.MaVe.Contains(maVe, StringComparison.OrdinalIgnoreCase));
        }

        // Lay tat ca ve cua hanh khach hien tai
        // Tuong tu C++: xemLichSuGiaoDich(hk)
        public List<VeTau> LayVeCuaToi()
        {
            if (_data.HanhKhachHienTai == null) return new List<VeTau>();
            return _data.DanhSachVe.Where(v => 
                v.HanhKhach?.MaNguoiDung == _data.HanhKhachHienTai.MaNguoiDung).ToList();
        }

        // Lay tat ca ve trong he thong (cho Admin)
        public List<VeTau> GetAllVe()
        {
            return _data.DanhSachVe;
        }
    }
}
