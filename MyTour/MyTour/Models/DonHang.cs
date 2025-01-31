﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyTour.Models
{
    public class DonHang
    {
        public int MaDH { get; set; }
        public int MaTour { get; set; }
        public int MaKH { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayKhoiHanh { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
        public int TrangThai { get; set; }
        public string GhiChu { get; set; }

        public KhachHang KH { get; set; }

    }
}
