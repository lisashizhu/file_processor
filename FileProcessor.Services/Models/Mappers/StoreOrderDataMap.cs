using System;
using CsvHelper.Configuration;

namespace FileProcessor.Services.Models.Mappers
{
	public class StoreOrderDataMap: ClassMap<StoreOrderData>
	{
		public StoreOrderDataMap()
		{
			Map(m => m.Id).Name("Row ID");
			Map(m => m.OrderId).Name("Order ID");
			Map(m => m.OrderDate).Name("Order Date");
			Map(m => m.ShipDate).Name("Ship Date");
			Map(m => m.ShipMode).Name("Ship Mode");
			Map(m => m.CustomerId).Name("Customer ID");
			Map(m => m.CustomerName).Name("Customer Name");
			Map(m => m.Segment).Name("Segment");
			Map(m => m.Country).Name("Country");
			Map(m => m.City).Name("City");
			Map(m => m.State).Name("State");
			Map(m => m.PostalCode).Name("Postal Code");
			Map(m => m.Region).Name("Region");
			Map(m => m.ProductId).Name("Product ID");
			Map(m => m.Category).Name("Category");
			Map(m => m.SubCategory).Name("Sub-Category");
			Map(m => m.ProductName).Name("Product Name");
			Map(m => m.Sales).Name("Sales");
			Map(m => m.Quantity).Name("Quantity");
			Map(m => m.Discount).Name("Discount");
			Map(m => m.Profit).Name("Profit");
		}
	}
}

