namespace ProjectTrainWeb.Models
{
    // Lop GheNgoi: Dai dien cho mot ghe ngoi trong toa tau
    // Muc dich: Quan ly trang thai va gia ca cua ghe
    // Trang thai: false = Trong, true = Da dat
    public class GheNgoi
    {
        public int MaGhe { get; set; }
        public bool TrangThai { get; set; } // true = Da dat, false = Trong
        public double GiaCoBan { get; set; }

        public GheNgoi()
        {
            TrangThai = false; // Mac dinh la trong
        }

        public GheNgoi(int maGhe, double giaCoBan)
        {
            MaGhe = maGhe;
            GiaCoBan = giaCoBan > 0 ? giaCoBan : 0;
            TrangThai = false; // Mac dinh la trong khi tao moi
        }

        // Phuong thuc dat ghe
        public bool DatGhe()
        {
            if (!TrangThai)
            {
                TrangThai = true;
                return true;
            }
            return false; // Ghe da duoc dat roi
        }

        // Phuong thuc huy ghe (khi khach huy ve)
        public bool HuyGhe()
        {
            if (TrangThai)
            {
                TrangThai = false;
                return true;
            }
            return false; // Ghe dang trong
        }

        // Kiem tra ghe co trong khong
        public bool CoTrong()
        {
            return !TrangThai;
        }
    }
}