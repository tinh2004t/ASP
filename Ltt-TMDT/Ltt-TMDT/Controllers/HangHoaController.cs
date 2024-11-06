﻿using Ltt_TMDT.Data;
using Ltt_TMDT.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ltt_TMDT.Controllers
{
	public class HangHoaController : Controller
	{
		private readonly LttTmdtContext db;
		public HangHoaController(LttTmdtContext context)
		{
			db = context;
		}
		public IActionResult Index(int? loai)
		{
			var hangHoas = db.HangHoas.AsQueryable();

			if (loai.HasValue)
			{
				hangHoas = hangHoas.Where(p => p.MaLoai == loai.Value);
			}

			var result = hangHoas.Select(p => new HangHoaVM
			{
				MaHh = p.MaHh,
				TenHh = p.TenHh,
				DonGia = p.DonGia ?? 0,
				Hinh = p.Hinh ?? "",
				MoTaNgan = p.MoTaDonVi ?? "",
				TenLoai = p.MaLoaiNavigation.TenLoai,
			});

			return View(result);
		}

		public IActionResult Search (string? query)
		{
			var hangHoas = db.HangHoas.AsQueryable();

			if (query != null)
			{
				hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
			}

			var result = hangHoas.Select(p => new HangHoaVM
			{
				MaHh = p.MaHh,
				TenHh = p.TenHh,
				DonGia = p.DonGia ?? 0,
				Hinh = p.Hinh ?? "",
				MoTaNgan = p.MoTaDonVi ?? "",
				TenLoai = p.MaLoaiNavigation.TenLoai,
			});

			return View(result);
		}

		public IActionResult Detail(int id) { 
			var data = db.HangHoas
				.Include(p => p.MaLoaiNavigation)
				.SingleOrDefault(p => p.MaHh == id);
			if (data == null)
			{
				TempData["Messege"] = $"Không thấy sản phẩm có mã {id}";
				return Redirect("/404");
			}
			var result = new ChiTietHangHoaVM
			{
				MaHh = data.MaHh,
				TenHh = data.TenHh,
				DonGia = data.DonGia ?? 0,
				ChiTiet = data.MoTa ?? string.Empty,
				Hinh = data.Hinh ?? string.Empty,
				MoTaNgan = data.MoTaDonVi ?? string.Empty,
				TenLoai = data.MaLoaiNavigation.TenLoai,
				SoLuongTon = 10,
				DiemDanhGia = 5,
			};
			return View(result); }

	}
}
