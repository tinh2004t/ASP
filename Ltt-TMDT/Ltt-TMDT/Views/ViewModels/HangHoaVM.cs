﻿using Ltt_TMDT.Controllers;
using Ltt_TMDT.Data;

namespace Ltt_TMDT.Views.ViewModels
{
	public class HangHoaVM
	{
		
		public int MaHh { get; set; }
		public string TenHh { get; set; }

		public string Hinh { get; set; }
		public double DonGia { get; set; }

		public string MoTaNgan { get; set; }
		public string TenLoai { get; set; }
	}
    public class ChiTietHangHoaVM
    {

        public int MaHh { get; set; }
        public string TenHh { get; set; }

        public string Hinh { get; set; }
        public double DonGia { get; set; }

        public string MoTaNgan { get; set; }
        public string TenLoai { get; set; }

		public string ChiTiet {  get; set; }
		public int DiemDanhGia { get; set; }
		public int SoLuongTon {  get; set; }
    }

}
