using System;
using System.Collections.Generic;

namespace ProjectTrainWeb.Models
{
    // Lop ChuyenTau: Dai dien cho mot chuyen tau
    // Muc dich: Quan ly thong tin chuyen tau, ga di, ga den va danh sach toa tau
    // Su dung Composition: Chua danh sach (List) cac ToaTau
    public class ChuyenTau
    {
        public int MaChuyen { get; set; }
        public GaTau? GaDi { get; set; } // Su dung GaTau object thay vi string, nullable
        public GaTau? GaDen { get; set; }
        public DateTime ThoiGianKhoiHanh { get; set; }
        public DateTime ThoiGianDenDu { get; set; }
        public List<ToaTau> DanhSachToa { get; set; } = new List<ToaTau>();

        public ChuyenTau()
        {
            GaDi = null;
            GaDen = null;
            DanhSachToa = new List<ToaTau>();
        }

        public ChuyenTau(int maChuyen, GaTau? gaDi, GaTau? gaDen, 
                        DateTime thoiGianKhoiHanh, DateTime thoiGianDenDu)
        {
            MaChuyen = maChuyen;
            GaDi = gaDi;
            GaDen = gaDen;
            ThoiGianKhoiHanh = thoiGianKhoiHanh;
            ThoiGianDenDu = thoiGianDenDu;
            DanhSachToa = new List<ToaTau>();
        }

        // Phuong thuc them toa vao chuyen tau
        public void ThemToa(ToaTau toa)
        {
            if (toa != null)
            {
                DanhSachToa.Add(toa);
            }
        }

        // Phuong thuc xoa toa khoi chuyen tau
        public bool XoaToa(string maToa)
        {
            var toa = DanhSachToa.Find(t => t.MaToa == maToa);
            if (toa != null)
            {
                DanhSachToa.Remove(toa);
                return true;
            }
            return false;
        }

        // Kiem tra chuyen tau con hop le (khong phai trong qua khu)
        public bool ConHopLe()
        {
            return DateTime.Now <= ThoiGianKhoiHanh;
        }
    }
}