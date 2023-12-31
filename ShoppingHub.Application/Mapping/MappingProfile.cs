using AutoMapper;
using ShoppingHub.Application.BasketDetails.Commands.CreateBasketDetail;
using ShoppingHub.Application.BasketDetails.Commands.DeleteBasketDetail;
using ShoppingHub.Application.BasketDetails.Queries.GetCurrentBasketDetails;
using ShoppingHub.Application.Baskets.Commands.CreateBasket;
using ShoppingHub.Application.Baskets.Commands.UpdateBasket;
using ShoppingHub.Application.Baskets.Queries.GetBasketsByUserId;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Products.Commands.CreateProduct;
using ShoppingHub.Application.Products.Commands.DeleteProduct;
using ShoppingHub.Application.Products.Commands.UpdateProduct;
using ShoppingHub.Application.Products.Queries.GetAllProducts;
using ShoppingHub.Application.Users.Commands.CreateUser;
using ShoppingHub.Application.Users.Commands.LoginUser;
using ShoppingHub.Domain.Entities;

namespace ShoppingHub.Application.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, DeleteProductCommand>().ReverseMap();
            CreateMap<Product, GetAllProductsQuery>().ReverseMap();

            CreateMap<BasketDetail, BasketDetailDto>().ReverseMap();
            CreateMap<BasketDetail, CreateBasketDetailCommand>().ReverseMap();
            CreateMap<BasketDetail, DeleteBasketDetailCommand>().ReverseMap();
            CreateMap<BasketDetail, GetCurrentBasketDetailsQuery>().ReverseMap();

            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<Basket, CreateBasketCommand>().ReverseMap();
            CreateMap<Basket, UpdateBasketCommand>().ReverseMap();
            CreateMap<Basket, GetBasketsByUserIdQuery>().ReverseMap();

            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, LoginUserCommand>().ReverseMap();
        }
    }
}

