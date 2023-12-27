using System;
using AutoMapper;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Products.Commands.CreateProduct;
using ShoppingHub.Application.Products.Commands.UpdateProduct;
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
        }
	}
}

