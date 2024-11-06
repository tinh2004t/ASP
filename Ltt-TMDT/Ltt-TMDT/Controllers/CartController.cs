using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ltt_TMDT.Data;
using Ltt_TMDT.Helpers;
using Ltt_TMDT.Views.ViewModels;
using Ltt_TMDT.Services;
using Ltt_TMDT.ViewModels;
using Newtonsoft.Json;

namespace Ltt_TMDT.Controllers
{
	public class CartController : Controller
	{
		private readonly PaypalClient _paypalClient;
		private readonly LttTmdtContext db;
		private readonly IVnPayService _vnPayservice;

		public CartController(LttTmdtContext context, PaypalClient paypalClient, IVnPayService vnPayService)
		{
			_paypalClient = paypalClient;
			db = context;
			_vnPayservice = vnPayService;
		}

		public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

		public IActionResult Index()
		{
			return View(Cart);
		}

		public IActionResult AddToCart(int id, int quantity = 1)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaHh == id);
			if (item == null)
			{
				var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
				if (hangHoa == null)
				{
					TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
					return Redirect("/404");
				}
				item = new CartItem
				{
					MaHh = hangHoa.MaHh,
					TenHh = hangHoa.TenHh,
					DonGia = hangHoa.DonGia ?? 0,
					Hinh = hangHoa.Hinh ?? string.Empty,
					SoLuong = quantity
				};
				gioHang.Add(item);
			}
			else
			{
				item.SoLuong += quantity;
			}

			HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

			return RedirectToAction("Index");
		}

		public IActionResult RemoveCart(int id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaHh == id);
			if (item != null)
			{
				gioHang.Remove(item);
				HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}

		[Authorize]
		[HttpGet]
		public IActionResult Checkout()
		{
			if (Cart.Count == 0)
			{
				return Redirect("/");
			}

			ViewBag.PaypalClientId = _paypalClient.ClientId;
			return View(Cart);
		}

		[Authorize]
		[HttpPost]
		public IActionResult Checkout(CheckoutVM model, string payment = "COD")
		{
			if (ModelState.IsValid)
			{
				if (payment == "Thanh toán VNPay")
				{
					TempData["CheckoutVM"] = JsonConvert.SerializeObject(model);  // Serialize model to TempData

					var vnPayModel = new VnPaymentRequestModel
					{
						Amount = Cart.Sum(p => p.ThanhTien),
						CreatedDate = DateTime.Now,
						Description = $"{model.HoTen} {model.DienThoai}",
						FullName = model.HoTen,
						OrderId = new Random().Next(1000, 100000)
					};
					return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
				}
				var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID).Value;
				var khachHang = new KhachHang();
				if (model.GiongKhachHang)
				{
					khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == customerId);
				}

				var hoadon = new HoaDon
				{
					MaKh = customerId,
					HoTen = model.HoTen ?? khachHang.HoTen,
					DiaChi = model.DiaChi ?? khachHang.DiaChi,
					DienThoai = model.DienThoai ?? khachHang.DienThoai,
					NgayDat = DateTime.Now,
					CachThanhToan = "COD",
					CachVanChuyen = "GRAB",
					MaTrangThai = 0,
					GhiChu = model.GhiChu
				};

				db.Database.BeginTransaction();
				try
				{
					db.Database.CommitTransaction();
					db.Add(hoadon);
					db.SaveChanges();

					var cthds = new List<ChiTietHd>();
					foreach (var item in Cart)
					{
						cthds.Add(new ChiTietHd
						{
							MaHd = hoadon.MaHd,
							SoLuong = item.SoLuong,
							DonGia = item.DonGia,
							MaHh = item.MaHh,
							GiamGia = 0
						});
					}
					db.AddRange(cthds);
					db.SaveChanges();

					HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());

					return View("Success");
				}
				catch
				{
					db.Database.RollbackTransaction();
				}
			}

			return View(Cart);
		}

		[Authorize]
		public IActionResult PaymentSuccess()
		{
			return View("Success");
		}

		#region Paypal payment
		[Authorize]
		[HttpPost("/Cart/create-paypal-order")]
		public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
		{
			// Thông tin đơn hàng gửi qua Paypal
			var tongTien = Cart.Sum(p => p.ThanhTien).ToString();
			var donViTienTe = "USD";
			var maDonHangThamChieu = "DH" + DateTime.Now.Ticks.ToString();

			try
			{
				var response = await _paypalClient.CreateOrder(tongTien, donViTienTe, maDonHangThamChieu);

				return Ok(response);
			}
			catch (Exception ex)
			{
				var error = new { ex.GetBaseException().Message };
				return BadRequest(error);
			}
		}

		[Authorize]
		[HttpPost("/Cart/capture-paypal-order")]
		public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken, CheckoutVM model)
		{
			try
			{
				var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID).Value;
				var khachHang = new KhachHang();
				var response = await _paypalClient.CaptureOrder(orderID);
				var hoadon = new HoaDon
				{
					MaKh = customerId,
					HoTen = model.HoTen ?? khachHang.HoTen,
					DiaChi = model.DiaChi ?? khachHang.DiaChi,
					DienThoai = model.DienThoai ?? khachHang.DienThoai,
					NgayDat = DateTime.Now,
					CachThanhToan = "PayPal",
					CachVanChuyen = "GRAB",
					MaTrangThai = 0,
					GhiChu = model.GhiChu
				};
				// Lưu database đơn hàng của mình
				db.Database.BeginTransaction();
				try
				{
					db.Database.CommitTransaction();
					db.Add(hoadon);
					db.SaveChanges();

					var cthds = new List<ChiTietHd>();
					foreach (var item in Cart)
					{
						cthds.Add(new ChiTietHd
						{
							MaHd = hoadon.MaHd,
							SoLuong = item.SoLuong,
							DonGia = item.DonGia,
							MaHh = item.MaHh,
							GiamGia = 0
						});
					}
					db.AddRange(cthds);
					db.SaveChanges();

					HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());

					return View("Success");
				}
				catch
				{
					db.Database.RollbackTransaction();
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				var error = new { ex.GetBaseException().Message };
				return BadRequest(error);
			}
		}

		#endregion


		#region VnPayment
		[Authorize]
		public IActionResult PaymentFail()
		{
			return View();
		}

		[Authorize]
		public IActionResult PaymentCallBack()
		{
			var response = _vnPayservice.PaymentExecute(Request.Query);

			if (response == null || response.VnPayResponseCode != "00")
			{
				TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
				return RedirectToAction("PaymentFail");
			}

			// Retrieve and deserialize model from TempData
			var model = JsonConvert.DeserializeObject<CheckoutVM>(TempData["CheckoutVM"]?.ToString());

			if (model == null)
			{
				TempData["Message"] = "Không thể tìm thấy thông tin đơn hàng.";
				return RedirectToAction("PaymentFail");
			}

			var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;
			var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == customerId);

			// Use model data if GiongKhachHang is false, otherwise use khachHang
			var hoadon = new HoaDon
			{
				MaKh = customerId,
				HoTen = model.GiongKhachHang ? khachHang?.HoTen : model.HoTen,
				DiaChi = model.GiongKhachHang ? khachHang?.DiaChi : model.DiaChi,
				DienThoai = model.GiongKhachHang ? khachHang?.DienThoai : model.DienThoai,
				NgayDat = DateTime.Now,
				CachThanhToan = "VNPay",
				CachVanChuyen = "GRAB",
				MaTrangThai = 0,
				GhiChu = model.GiongKhachHang ? "Thanh toán qua VNPay" : model.GhiChu
			};

			db.Database.BeginTransaction();
			try
			{
				db.Add(hoadon);
				db.SaveChanges();

				var cthds = new List<ChiTietHd>();
				foreach (var item in Cart)
				{
					cthds.Add(new ChiTietHd
					{
						MaHd = hoadon.MaHd,
						SoLuong = item.SoLuong,
						DonGia = item.DonGia,
						MaHh = item.MaHh,
						GiamGia = 0
					});
				}
				db.AddRange(cthds);
				db.SaveChanges();

				HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());
				db.Database.CommitTransaction();

				TempData["Message"] = "Thanh toán VNPay thành công";
				return RedirectToAction("PaymentSuccess");
			}
			catch
			{
				db.Database.RollbackTransaction();
				TempData["Message"] = "Đã xảy ra lỗi khi lưu đơn hàng vào cơ sở dữ liệu";
				return RedirectToAction("PaymentFail");
			}
		}





		#endregion
	}
}