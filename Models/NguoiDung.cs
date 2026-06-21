namespace ProjectTrainWeb.Models
{
    // Lop NguoiDung: Lop tru tuong (abstract) dai dien cho mot nguoi dung
    // Muc dich: Dong goi cac thuoc tinh chung nhat (Tai khoan, Mat khau, Ho ten)
    // Danh cho ke thua boi HanhKhach va QuanTriVien
    public abstract class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public string TaiKhoan { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty; // Chu y: Mat khau phai duoc ma hoa (Hash) truoc khi luu vao Database
        public string HoTen { get; set; } = string.Empty;

        protected NguoiDung()
        {
            MaNguoiDung = 0;
        }

        public NguoiDung(int maNguoiDung, string taiKhoan, string matKhau, string hoTen)
        {
            MaNguoiDung = maNguoiDung;
            TaiKhoan = string.IsNullOrWhiteSpace(taiKhoan) ? "" : taiKhoan;
            MatKhau = string.IsNullOrWhiteSpace(matKhau) ? "" : matKhau; // Se duoc ma hoa o tieu khoang khac
            HoTen = string.IsNullOrWhiteSpace(hoTen) ? "Khong Xac Dinh" : hoTen;
        }
    }
}