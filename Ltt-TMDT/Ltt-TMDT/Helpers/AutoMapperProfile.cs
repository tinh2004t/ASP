using AutoMapper;
using Ltt_TMDT.Data;
using Ltt_TMDT.Views.ViewModels;

namespace Ltt_TMDT.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<RegisterVM, KhachHang>();
			//.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
			//.ReverseMap();
		}
	}
}