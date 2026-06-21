namespace ProjectTrainWeb.Models
{
    // Lop VeThuong: Dai dien cho mot ve tau thuong
    // Ke thua: Ke thua tu VeTau
    // Muc dich: Tinh gia ve theo gia co ban (khong co the tin suat)
    public class VeThuong : VeTau
    {
        public VeThuong()
        {
        }

        public VeThuong(string maVe, ChuyenTau chuyenTau, HanhKhach hanhKhach, 
                       string maToa, int viTriGhe, double giaCoBan) 
            : base(maVe, chuyenTau, hanhKhach, maToa, viTriGhe, giaCoBan)
        {
        }

        // Ghi de phuong thuc cua lop cha: Tinh chinh xac theo gia co ban
        public override double TinhGiaVe()
        {
            return GiaCoBanGhe; // Ve thuong tinh dung gia co ban
        }
    }
}