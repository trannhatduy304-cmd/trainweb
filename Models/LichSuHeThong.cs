using System;

namespace ProjectTrainWeb.Models
{
    // Lop LichSuHeThong: Ghi nhan cac hoat dong cua he thong
    // Muc dich: Ho tro bao mat va quan tri. Moi thao tac cua Admin deu duoc ghi nhan
    public class LichSuHeThong
    {
        public string LogId { get; set; }
        
        // HanhDong: Them, Sua, Xoa, Dang Nhap...
        public string HanhDong { get; set; }
        
        public string NguoiThucHien { get; set; }
        public DateTime ThoiGian { get; set; }
        
        // ChiTiet: Mo ta chi tiet cua hanh dong
        public string ChiTiet { get; set; }

        public LichSuHeThong(string logId, string hanhDong, string nguoiThucHien, string chiTiet = "")
        {
            LogId = string.IsNullOrWhiteSpace(logId) ? Guid.NewGuid().ToString() : logId;
            HanhDong = string.IsNullOrWhiteSpace(hanhDong) ? "Khong Xac Dinh" : hanhDong;
            NguoiThucHien = string.IsNullOrWhiteSpace(nguoiThucHien) ? "He Thong" : nguoiThucHien;
            ThoiGian = DateTime.Now;
            ChiTiet = chiTiet;
        }

        public override string ToString()
        {
            return $"[{ThoiGian:yyyy-MM-dd HH:mm:ss}] {NguoiThucHien}: {HanhDong} - {ChiTiet}";
        }
    }
}
