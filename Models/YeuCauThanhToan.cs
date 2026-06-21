namespace ProjectTrainWeb.Models
{
    /// <summary>
    /// Yêu cầu thanh toán - gửi từ user đến admin để duyệt
    /// </summary>
    public class YeuCauThanhToan
    {
        public string MaYeuCau { get; set; } = Guid.NewGuid().ToString();
        public VeTau Ve { get; set; } = null!;
        public HanhKhach NguoiDat { get; set; } = null!;
        public DateTime ThoiGianGui { get; set; } = DateTime.Now;
        
        // Trạng thái: "Chờ Duyệt" | "Đã Duyệt" | "Từ Chối"
        public string TrangThai { get; set; } = "Chờ Duyệt";
        public string GhiChuAdmin { get; set; } = "";
        public DateTime? ThoiGianDuyet { get; set; }
        
        // Thông tin thanh toán
        public double SoTienThanhToan { get; set; }
        public string PhuongThucThanhToan { get; set; } = "Chuyển khoản";
    }
}
