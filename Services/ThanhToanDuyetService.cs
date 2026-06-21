using ProjectTrainWeb.Models;

namespace ProjectTrainWeb.Services
{
    /// <summary>
    /// Service quản lý cơ chế duyệt thanh toán:
    /// - User gửi yêu cầu thanh toán → Admin duyệt → Vé thành công
    /// - Admin upload QR + thông tin ngân hàng
    /// - Admin chỉnh sửa văn bản hiển thị (thông báo, chính sách)
    /// </summary>
    public class ThanhToanDuyetService
    {
        private readonly TrainBookingService _data;

        public ThanhToanDuyetService(TrainBookingService data)
        {
            _data = data;
        }

        // ==================== DỮ LIỆU ====================
        
        // Danh sách yêu cầu thanh toán
        public List<YeuCauThanhToan> DanhSachYeuCau { get; set; } = new();
        
        // Cấu hình thanh toán (QR + ngân hàng)
        public CauHinhThanhToan CauHinh { get; set; } = new();
        
        // Cấu hình hệ thống (văn bản hiển thị)
        public CauHinhHeThong CauHinhHT { get; set; } = new();

        // ==================== USER: GỬI YÊU CẦU ====================

        /// <summary>
        /// User gửi yêu cầu thanh toán sau khi đặt vé
        /// </summary>
        public (bool thanhCong, string thongBao, YeuCauThanhToan? yeuCau) GuiYeuCauThanhToan(
            VeTau ve, HanhKhach nguoiDat, double soTien)
        {
            if (ve == null || nguoiDat == null)
                return (false, "Thông tin vé hoặc người đặt không hợp lệ.", null);

            var yeuCau = new YeuCauThanhToan
            {
                Ve = ve,
                NguoiDat = nguoiDat,
                SoTienThanhToan = soTien,
                ThoiGianGui = DateTime.Now,
                TrangThai = "Chờ Duyệt"
            };

            DanhSachYeuCau.Add(yeuCau);

            // Thêm thông báo cho admin
            var thongBaoAdmin = new ThongBao(
                Guid.NewGuid().ToString(),
                $"Hành khách {nguoiDat.HoTen} đã gửi yêu cầu thanh toán {soTien:N0} VND cho vé {ve.MaVe.Substring(0, 8).ToUpper()}.",
                "Email",
                nguoiDat.TaiKhoan,
                ve.MaVe
            );
            thongBaoAdmin.DanhDauDaGui();
            _data.DanhSachThongBao.Add(thongBaoAdmin);

            return (true, "Yêu cầu thanh toán đã được gửi. Vui lòng chờ Admin duyệt.", yeuCau);
        }

        // ==================== USER: XEM LỊCH SỬ ====================

        /// <summary>
        /// Lấy danh sách yêu cầu thanh toán của một hành khách
        /// </summary>
        public List<YeuCauThanhToan> LayLichSuDatVe(HanhKhach hk)
        {
            if (hk == null) return new();
            return DanhSachYeuCau
                .Where(yc => yc.NguoiDat.TaiKhoan == hk.TaiKhoan)
                .OrderByDescending(yc => yc.ThoiGianGui)
                .ToList();
        }

        // ==================== ADMIN: DUYỆT THANH TOÁN ====================

        /// <summary>
        /// Lấy danh sách yêu cầu chờ duyệt
        /// </summary>
        public List<YeuCauThanhToan> LayYeuCauChoDuyet()
        {
            return DanhSachYeuCau
                .Where(yc => yc.TrangThai == "Chờ Duyệt")
                .OrderBy(yc => yc.ThoiGianGui)
                .ToList();
        }

        /// <summary>
        /// Lấy tất cả yêu cầu (cho admin xem lịch sử)
        /// </summary>
        public List<YeuCauThanhToan> LayTatCaYeuCau()
        {
            return DanhSachYeuCau
                .OrderByDescending(yc => yc.ThoiGianGui)
                .ToList();
        }

        /// <summary>
        /// Admin duyệt yêu cầu thanh toán → Vé đặt thành công
        /// </summary>
        public (bool thanhCong, string thongBao) DuyetThanhToan(string maYeuCau, string ghiChu = "")
        {
            var yeuCau = DanhSachYeuCau.FirstOrDefault(yc => yc.MaYeuCau == maYeuCau);
            if (yeuCau == null)
                return (false, "Không tìm thấy yêu cầu thanh toán.");

            if (yeuCau.TrangThai != "Chờ Duyệt")
                return (false, "Yêu cầu này đã được xử lý trước đó.");

            yeuCau.TrangThai = "Đã Duyệt";
            yeuCau.GhiChuAdmin = ghiChu;
            yeuCau.ThoiGianDuyet = DateTime.Now;

            // Đánh dấu vé đã thanh toán
            if (yeuCau.Ve != null)
            {
                yeuCau.Ve.ThanhToanVe();
            }

            // Ghi nhận giao dịch
            var giaoDich = new GiaoDich(
                Guid.NewGuid().ToString(),
                yeuCau.SoTienThanhToan,
                "Chuyển khoản",
                "Thanh toán vé",
                yeuCau.NguoiDat.TaiKhoan,
                yeuCau.Ve?.MaVe ?? ""
            );
            giaoDich.XacNhanThanhCong();
            _data.DanhSachGiaoDich.Add(giaoDich);

            // Thông báo cho user
            var thongBao = new ThongBao(
                Guid.NewGuid().ToString(),
                $"Yêu cầu thanh toán cho vé {yeuCau.Ve?.MaVe.Substring(0, 8).ToUpper()} đã được Admin duyệt. Vé của bạn đã được xác nhận!",
                "Email",
                yeuCau.NguoiDat.TaiKhoan,
                yeuCau.Ve?.MaVe ?? ""
            );
            thongBao.DanhDauDaGui();
            _data.DanhSachThongBao.Add(thongBao);

            // Ghi lịch sử
            _data.DanhSachLichSu.Add(new LichSuHeThong(
                Guid.NewGuid().ToString(),
                "Duyệt thanh toán",
                "Admin",
                $"Duyệt thanh toán vé {yeuCau.Ve?.MaVe.Substring(0, 8).ToUpper()} - {yeuCau.SoTienThanhToan:N0} VND"
            ));

            return (true, "Đã duyệt thanh toán thành công.");
        }

        /// <summary>
        /// Admin từ chối yêu cầu thanh toán → Hủy vé + giải phóng ghế
        /// </summary>
        public (bool thanhCong, string thongBao) TuChoiThanhToan(string maYeuCau, string lyDo = "")
        {
            var yeuCau = DanhSachYeuCau.FirstOrDefault(yc => yc.MaYeuCau == maYeuCau);
            if (yeuCau == null)
                return (false, "Không tìm thấy yêu cầu thanh toán.");

            if (yeuCau.TrangThai != "Chờ Duyệt")
                return (false, "Yêu cầu này đã được xử lý trước đó.");

            yeuCau.TrangThai = "Từ Chối";
            yeuCau.GhiChuAdmin = lyDo;
            yeuCau.ThoiGianDuyet = DateTime.Now;

            // Hủy vé + giải phóng ghế
            if (yeuCau.Ve != null)
            {
                _data.HuyVe(yeuCau.Ve.MaVe);
            }

            // Thông báo cho user
            var thongBao = new ThongBao(
                Guid.NewGuid().ToString(),
                $"Yêu cầu thanh toán cho vé {yeuCau.Ve?.MaVe.Substring(0, 8).ToUpper()} đã bị từ chối. Lý do: {(string.IsNullOrEmpty(lyDo) ? "Không có" : lyDo)}",
                "Email",
                yeuCau.NguoiDat.TaiKhoan,
                yeuCau.Ve?.MaVe ?? ""
            );
            thongBao.DanhDauDaGui();
            _data.DanhSachThongBao.Add(thongBao);

            return (true, "Đã từ chối yêu cầu thanh toán.");
        }

        // ==================== ADMIN: CẤU HÌNH THANH TOÁN ====================

        /// <summary>
        /// Admin lưu cấu hình thanh toán (QR + ngân hàng)
        /// </summary>
        public void LuuCauHinhThanhToan(CauHinhThanhToan cauHinh)
        {
            CauHinh = cauHinh;
        }

        /// <summary>
        /// Lấy cấu hình thanh toán hiện tại
        /// </summary>
        public CauHinhThanhToan LayCauHinhThanhToan()
        {
            return CauHinh;
        }

        // ==================== ADMIN: CẤU HÌNH HỆ THỐNG ====================

        /// <summary>
        /// Admin lưu cấu hình hệ thống (văn bản hiển thị)
        /// </summary>
        public void LuuCauHinhHeThong(CauHinhHeThong cauHinh)
        {
            CauHinhHT = cauHinh;
        }

        /// <summary>
        /// Lấy cấu hình hệ thống hiện tại
        /// </summary>
        public CauHinhHeThong LayCauHinhHeThong()
        {
            return CauHinhHT;
        }

        // ==================== THỐNG KÊ ====================

        public int DemYeuCauChoDuyet()
        {
            return DanhSachYeuCau.Count(yc => yc.TrangThai == "Chờ Duyệt");
        }

        public int DemYeuCauDaDuyet()
        {
            return DanhSachYeuCau.Count(yc => yc.TrangThai == "Đã Duyệt");
        }

        public int DemYeuCauTuChoi()
        {
            return DanhSachYeuCau.Count(yc => yc.TrangThai == "Từ Chối");
        }
    }
}
