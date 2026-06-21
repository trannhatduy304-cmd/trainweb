namespace ProjectTrainWeb.Models
{
    // Lop GaTau: Dai dien cho mot ga tau
    // Muc dich: Tieu chuan hoa du lieu ga di va ga den, tranh loi chinh ta
    public class GaTau
    {
        public int MaGa { get; set; }
        public string TenGa { get; set; }
        public string ThanhPho { get; set; }

        public GaTau(int maGa, string tenGa, string thanhPho)
        {
            MaGa = maGa;
            TenGa = string.IsNullOrWhiteSpace(tenGa) ? "Chua_Xac_Dinh" : tenGa;
            ThanhPho = string.IsNullOrWhiteSpace(thanhPho) ? "Chua_Xac_Dinh" : thanhPho;
        }

        public override string ToString()
        {
            return $"{TenGa} ({ThanhPho})";
        }
    }
}
