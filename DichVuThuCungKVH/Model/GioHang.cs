using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DichVuThuCungKVH.Model
{
    public class GioHang
    {
        private DACSEntities db = new DACSEntities();
        public int iMaSP { get; set; }
        public string sṬenSP { get; set; }
        public string sTenSP { get; }
        public string sAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dTongThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        public GioHang(int ms)

        {
            iMaSP = ms;
            SanPham s = db.SanPhams.Single(n => n.MaSP == iMaSP);
            sTenSP = s.TenSP;
            sAnh = s.Anh;
            dDonGia = double.Parse(s.GiaBan.ToString());
            iSoLuong = 1;

        }
    }
}