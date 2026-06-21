namespace ProjectTrainWeb.Models
{
    // Lop HanhKhach: Dai dien cho mot hanh khach
    // Ke thua: Ke thua tu NguoiDung va bo sung So dien thoai
    // Muc dich: Luu tru thong tin khach hang
    public class HanhKhach : NguoiDung
    {
        public string SoDienThoai { get; set; } = string.Empty;

        public HanhKhach()
        {
        }

        // Khoi tao HanhKhach voi du lieu day du
        public HanhKhach(int maNguoiDung, string taiKhoan, string matKhau, 
                         string hoTen, string soDienThoai) 
            : base(maNguoiDung, taiKhoan, matKhau, hoTen)
        {
            SoDienThoai = string.IsNullOrWhiteSpace(soDienThoai) ? "" : soDienThoai;
        }
    }
}