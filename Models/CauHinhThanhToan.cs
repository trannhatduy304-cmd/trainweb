namespace ProjectTrainWeb.Models
{
    /// <summary>
    /// Cấu hình thanh toán - Admin upload QR + thông tin ngân hàng
    /// </summary>
    public class CauHinhThanhToan
    {
        public string SoTaiKhoan { get; set; } = "0123456789";
        public string TenNganHang { get; set; } = "Vietcombank";
        public string TenChuTaiKhoan { get; set; } = "NGUYEN VAN A";
        public string ChiNhanh { get; set; } = "TP. Hồ Chí Minh";
        
        // Base64 string của hình QR (hoặc URL)
        public string HinhQR { get; set; } = "";
        
        // Nội dung chuyển khoản mẫu
        public string NoiDungMau { get; set; } = "THANHTOAN {MaVe}";
    }
}
