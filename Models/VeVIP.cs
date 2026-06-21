namespace ProjectTrainWeb.Models
{
    // Lop VeVIP: Dai dien cho mot ve tau VIP
    // Ke thua: Ke thua tu VeTau
    // Muc dich: Tinh gia ve theo gia co ban nhan them he so VIP
    public class VeVIP : VeTau
    {
        public double HeSoVip { get; set; } = 1.3; // Mac dinh he so VIP la 1.3 (cao hon 30% so voi ve thuong)

        public VeVIP()
        {
        }

        public VeVIP(string maVe, ChuyenTau chuyenTau, HanhKhach hanhKhach, 
                    string maToa, int viTriGhe, double giaCoBan, double heSoVip = 1.3) 
            : base(maVe, chuyenTau, hanhKhach, maToa, viTriGhe, giaCoBan)
        {
            HeSoVip = heSoVip > 0 ? heSoVip : 1.3;
        }

        // Ghi de phuong thuc cua lop cha: Tinh gia VIP voi he so
        public override double TinhGiaVe()
        {
            return GiaCoBanGhe * HeSoVip; // Ve VIP nhan them he so
        }
    }
}