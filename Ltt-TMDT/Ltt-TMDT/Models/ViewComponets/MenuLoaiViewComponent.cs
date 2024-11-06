using Ltt_TMDT.Data;
using Ltt_TMDT.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace Ltt_TMDT.Models.ViewComponets
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly LttTmdtContext db;
        public MenuLoaiViewComponent(LttTmdtContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai);

            return View(data); //Default.cshtml
            //return View("Default",data);
        }
    }
}
