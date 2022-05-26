using System;
using AutoMapper;
using FileProcessor.Common.Extensions;
using FileProcessor.Data;
using FileProcessor.Services.Models;

namespace FileProcessor.Business.Models.Mappers
{
	public class StoreOrderProfile : Profile
	{
		public StoreOrderProfile()
		{
			CreateMap<StoreOrderData, STORE_ORDER>()
				.ForMember(dest => dest.ORDER_ID, opt => opt.MapFrom(scr => scr.OrderId))
				.ForMember(dest => dest.ORDER_DATE, opt => opt.MapFrom(scr => scr.OrderDate.ToDateTime("dd'.'MM'.'yy")))
				.ForMember(dest => dest.SHIP_DATE, opt => opt.MapFrom(scr => scr.ShipDate.ToDateTime("dd'.'MM'.'yy")))
				.ForMember(dest => dest.SHIP_MODE, opt => opt.MapFrom(scr => scr.ShipMode))
				.ForMember(dest => dest.QUANTITY, opt => opt.MapFrom(scr => scr.Quantity))
				.ForMember(dest => dest.DISCOUNT, opt => opt.MapFrom(scr => scr.Discount))
				.ForMember(dest => dest.PROFIT, opt => opt.MapFrom(scr => scr.Profit))
				.ForMember(dest => dest.PRODUCT_ID, opt => opt.MapFrom(scr => scr.ProductId))
				.ForMember(dest => dest.CUSTOMER_NAME, opt => opt.MapFrom(scr => scr.CustomerName))
				.ForMember(dest => dest.CATEGORY, opt => opt.MapFrom(scr => scr.Category))
				.ForMember(dest => dest.CUSTOMER_ID, opt => opt.MapFrom(scr => scr.CustomerId))
				.ForMember(dest => dest.PRODUCT_NAME, opt => opt.MapFrom(scr => scr.ProductName));
		}
	}
}

