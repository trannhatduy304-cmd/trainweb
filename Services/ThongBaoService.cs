using ProjectTrainWeb.Models;

namespace ProjectTrainWeb.Services
{
    // ThongBaoService: Quan ly thong bao cho nguoi dung
    // Tinh nang web moi (khong co trong C++ console)
    // OOP: Dong goi (Encapsulation) - ThongBao.DanhDauDaGui()
    public class ThongBaoService
    {
        private readonly TrainBookingService _data;

        public ThongBaoService(TrainBookingService data)
        {
            _data = data;
        }

        // ==================== GUI THONG BAO ====================
        // Tao thong bao moi cho hanh khach
        public void GuiThongBao(string noiDung, string maHanhKhach, string maVe = "")
        {
            var tb = new ThongBao(Guid.NewGuid().ToString(), noiDung, "Email", maHanhKhach, maVe);
            tb.DanhDauDaGui(); // Dong goi - Encapsulation
            _data.DanhSachThongBao.Add(tb);
        }

        // Gui thong bao he thong (cho tat ca)
        public void GuiThongBaoHeThong(string noiDung)
        {
            var tb = new ThongBao(Guid.NewGuid().ToString(), noiDung, "Email", "", "");
            tb.DanhDauDaGui();
            _data.DanhSachThongBao.Add(tb);
        }

        // ==================== LAY THONG BAO ====================

        // Lay thong bao cua nguoi dung hien tai
        public List<ThongBao> LayThongBaoCuaToi()
        {
            if (_data.HanhKhachHienTai == null) 
            {
                // Neu chua dang nhap, chi hien thong bao cong khai (MaHanhKhach rong)
                return _data.DanhSachThongBao
                    .Where(t => string.IsNullOrEmpty(t.MaHanhKhach))
                    .OrderByDescending(t => t.ThoiGianGui)
                    .ToList();
            }

            // Hien thong bao cua hanh khach + thong bao cong khai
            return _data.DanhSachThongBao
                .Where(t => t.MaHanhKhach == _data.HanhKhachHienTai.MaNguoiDung.ToString() || 
                            string.IsNullOrEmpty(t.MaHanhKhach))
                .OrderByDescending(t => t.ThoiGianGui)
                .ToList();
        }

        // Lay tat ca thong bao (cho Admin)
        public List<ThongBao> LayTatCaThongBao()
        {
            return _data.DanhSachThongBao.OrderByDescending(t => t.ThoiGianGui).ToList();
        }

        // ==================== DEM THONG BAO ====================

        // Dem thong bao chua doc (chua gui)
        public int DemThongBaoChuaDoc()
        {
            return LayThongBaoCuaToi().Count(t => t.TrangThai == "Chua Gui");
        }

        // ==================== DANH DAU DA DOC ====================

        // Danh dau 1 thong bao da doc
        public void DanhDauDaDoc(string maThongBao)
        {
            var tb = _data.DanhSachThongBao.FirstOrDefault(t => t.MaThongBao == maThongBao);
            if (tb != null)
            {
                tb.DanhDauDaGui(); // Dong goi - Encapsulation
            }
        }

        // Danh dau tat ca thong bao da doc
        public void DanhDauTatCaDaDoc()
        {
            foreach (var tb in LayThongBaoCuaToi())
            {
                if (tb.TrangThai == "Chua Gui")
                {
                    tb.DanhDauDaGui();
                }
            }
        }
    }
}
