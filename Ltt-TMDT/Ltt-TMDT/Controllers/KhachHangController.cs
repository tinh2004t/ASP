﻿using AutoMapper;
using Ltt_TMDT.Data;
using Ltt_TMDT.Helpers;
using Ltt_TMDT.Views.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ltt_TMDT.Controllers
{
	public class KhachHangController : Controller
	{
		private readonly LttTmdtContext db;
		private readonly IMapper _mapper;

		public KhachHangController(LttTmdtContext context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}

		#region Register
		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}

		[HttpPost]
		public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
		{
			if (!ModelState.IsValid)
			{
				try
				{
					var khachHang = _mapper.Map<KhachHang>(model);
					khachHang.RandomKey = MyUtil.GenerateRamdomKey();
					khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
					khachHang.HieuLuc = true;//sẽ xử lý khi dùng Mail để active
					khachHang.VaiTro = 0;

					if (Hinh != null)
					{
						khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
					}

					db.Add(khachHang);
					db.SaveChanges();
					return RedirectToAction("Profile", "KhachHang");
				}
				catch (Exception ex)
				{
					var mess = $"{ex.Message} shh";
				}
			}
			return View();
		}
		#endregion


		#region Login
		[HttpGet]
		public IActionResult DangNhap(string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			if (ModelState.IsValid)
			{
				var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
				if (khachHang == null)
				{
					ModelState.AddModelError("loi", "Không có khách hàng này");
				}
				else
				{
					if (!khachHang.HieuLuc)
					{
						ModelState.AddModelError("loi", "Tài khoản đã bị khóa. Vui lòng liên hệ Admin.");
					}
					else
					{
						if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
						{
							ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
						}
						else
						{
							var claims = new List<Claim> {
								new Claim(ClaimTypes.Email, khachHang.Email),
								new Claim(ClaimTypes.Name, khachHang.HoTen),
								new Claim(MySetting.CLAIM_CUSTOMERID,khachHang.MaKh),

								//claim - role động
								new Claim(ClaimTypes.Role, "Customer")
							};

							var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
							var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

							await HttpContext.SignInAsync(claimsPrincipal);

							if (Url.IsLocalUrl(ReturnUrl))
							{
								return Redirect(ReturnUrl);
							}
							else
							{
								return Redirect("/");
							}
						}
					}
				}
			}
			return View();
		}
		#endregion

		[Authorize]
		public IActionResult Profile()
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> DangXuat()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
	}
}