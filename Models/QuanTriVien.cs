namespace ProjectTrainWeb.Models
{
    // Lop QuanTriVien: Dai dien cho mot quan tri vien cua he thong
    // Ke thua: Ke thua tu NguoiDung
    // Muc dich: Co quyen han truy cap va quan ly cac chuc nang quan tri he thong
    public class QuanTriVien : NguoiDung
    {
        // Muc cap quyen: 1 = Quan tri vien, 2 = Quan tri vien cap cao
        public int MucCapQuyen { get; set; }

        public QuanTriVien()
        {
            MucCapQuyen = 1;
        }

        public QuanTriVien(int maNguoiDung, string taiKhoan, string matKhau, 
                          string hoTen, int mucCapQuyen = 1)
            : base(maNguoiDung, taiKhoan, matKhau, hoTen)
        {
            MucCapQuyen = mucCapQuyen > 0 ? mucCapQuyen : 1;
        }

        // Kiem tra xem co quyen truy cap chuc nang quan tri
        public bool CoQuyenQuanTri()
        {
            return MucCapQuyen >= 1;
        }

        // Kiem tra xem co quyen quan li o muc cao cap
        public bool CoQuyenCapCao()
        {
            return MucCapQuyen >= 2;
        }
    }
}