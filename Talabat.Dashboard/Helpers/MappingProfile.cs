using AutoMapper;
using Talabat.Core.Models;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Helpers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductViewModel>().ReverseMap();
		}
	}
}
