using AutoMapper;
using MST_REST_Web_API.Entities;
using MST_REST_Web_API.Models.ViewModels;

namespace MST_REST_Web_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<User, UserView>().ReverseMap();
            //CreateMap<User, UserDto>().ReverseMap();
            //CreateMap<User, MyAccountDetailsView>().ReverseMap();
            //CreateMap<Product, CreateProductDto>().ReverseMap();
            //CreateMap<Product, ProductDto>().ReverseMap();
            //CreateMap<OrderView, Order>().ReverseMap().ForMember(o => o.Status, s => s.MapFrom(x => x.OrderStatus.Name));
            //CreateMap<OpinionView, Opinion>().ReverseMap().ForMember(o => o.UserName, s => s.MapFrom(x => x.User.FirstName));
            //CreateMap<User, UserDetailsView>();
            //CreateMap<Product, ProductsView>().ReverseMap();
            //CreateMap<Post, PostDto>().ReverseMap();


        }
    }
}
