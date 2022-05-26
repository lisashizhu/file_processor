using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileProcessor.Data;
using FileProcessor.Services;
using FileProcessor.Services.Models;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using FileProcessor.Common.Exceptions;

namespace FileProcessor.Test
{
    public class CsvFileProcessorTestFixture
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly string csvString = $"Row ID,Order ID,Order Date,Ship Date,Ship Mode,Customer ID,Customer Name,Segment,Country,City,State,Postal Code,Region,Product ID,Category,Sub-Category,Product Name,Sales,Quantity,Discount,Profit\n1,CA-2016-152156,08.11.16,11.11.16,Second Class,CG-12520,Claire Gute,Consumer,United States,Henderson,Kentucky,42420,South,FUR-BO-10001798,Furniture,Bookcases,Bush Somerset Collection Bookcase,261.96,2,0,41.9136\n2,CA-2016-152156,08.11.16,11.11.16,Second Class,CG-12520,Claire Gute,Consumer,United States,Henderson,Kentucky,42420f,South,FUR-CH-10000454,Furniture,Chairs,\"Hon Deluxe Fabric Upholstered Stacking Chairs, Rounded Back\",731.94,3,0,219.582\n3,CA-2016-138688,12.06.16,16.06.16,Second Class,DV-13045,Darrin Van Huff,Corporate,United States,Los Angeles,California,90036,West,OFF-LA-10000240,Office Supplies,Labels,Self-Adhesive Address Labels for Typewriters by Universal,14.62,2,0,6.8714\n4,US-2015-108966,11.10.15,18.10.15,Standard Class,SO-20335,Sean O\'Donnell,Consumer,United States,Fort Lauderdale,Florida,33311,South,FUR-TA-10000577,Furniture,Tables,Bretford CR4500 Series Slim Rectangular Table,957.5775,5,0.45,-383.031\n5,US-2015-108966,11.10.15,18.10.15,Standard Class,SO-20335,Sean O\'Donnell,Consumer,United States,Fort Lauderdale,Florida,33311,South,\"\",Office Supplies,Storage,Eldon Fold \'N Roll Cart System,22.368,2,0.2,2.5164\n6,CA-2014-115812,09.06.14,14.06.14,Standard Class,BH-11710,Brosina Hoffman,Consumer,United States,Los Angeles,California,90032,West,FUR-FU-10001487,Furniture,Furnishings,\"Eldon Expressions Wood\nand Plastic Desk Accessories, Cherry Wood\",48.86,7,0,14.1694\n7,CA-2014-115812,09.06.14,14.06.14,Standard Class,BH-11710,Brosina Hoffman,Consumer,United States,Los Angeles,California,90032,West,OFF-AR-10002833,Office Supplies,Art,Newell 322,7.28,4,0,1.9656\n8,CA-2014-115812,09.06.14,14.06.14,Standard Class,BH-11710,Brosina Hoffman,Consumer,United States,Los Angeles,California,90032,West,TEC-PH-10002275,Technology,Phones,Mitel 5320 IP Phone VoIP phone,907.152,6,0.2,90.7152\n9,CA-2015-117415,27.12.15,31.12.15,Standard Class,SN-20710,Steve Nguyen,Home Office,United States,Houston,Texas,77041,Central,OFF-EN-10002986,Office Supplies,Envelopes,\"#10-4 1/8\"\" x, 9 1/2\"\" Premium Diagonal Seam Envelopes\",113.328,9,0.2,35.415\n10,,09.06.14,14.06.14,Standard Class,BH-11710,Brosina Hoffman,Consumer,United States,Los Angeles,California,90032,West,OFF-AP-10002892,Office Supplies,Appliances,Belkin F5C206VTEL 6 Outlet Surge,114.9,5,0,34.47\n11,CA-2014-115812,09.06.14,14.06.14,Standard Class,BH-11710,Brosina Hoffman,Consumer,United States,Los Angeles,California,90032,West,FUR-TA-10001539,Furniture,Tables,Chromcraft Rectangular Conference Tables,1706.184,9,0.2,85.3092\n";
        private string databaseName = Guid.NewGuid().ToString();

        public CsvFileProcessorTestFixture()
        {
            _mapperMock = new Mock<IMapper>();
            _serviceProviderMock = new Mock<IServiceProvider>();
            _serviceProviderMock.Setup(x => x.GetService(typeof(DatabaseContext)))
                .Returns(CreateDatabaseContext());
        }

        [Fact]
        public async Task Ensure_CsvFileProcessor_SaveData()
        {
            var orderToSave = new STORE_ORDER {
                CATEGORY = "Furniture",
                CUSTOMER_ID = "CG-12520",
                CUSTOMER_NAME = "Claire Gute",
                DISCOUNT = 0,
                ORDER_DATE = new DateTime(2016, 11, 08),
                ORDER_ID = "CA-2016-152156",
                PRODUCT_ID = "FUR-BO-10001798",
                PRODUCT_NAME = "Bush Somerset Collection Bookcase",
                PROFIT = 41.9136M,
                QUANTITY = 2,
                SHIP_DATE = new DateTime(2016, 11, 11),
                SHIP_MODE = "Second Class"};
            _mapperMock.Setup(obj => obj.Map<STORE_ORDER>(It.IsAny<StoreOrderData>())).Returns(orderToSave);
            var processor = new CsvFileProcessor(_mapperMock.Object, _serviceProviderMock.Object);
            await processor.SaveData(csvString);

            List<STORE_ORDER> records;
            await using(var context = CreateDatabaseContext())
            {
                records = context.StoreOrder.Where(so => so.CUSTOMER_ID == "CG-12520")?.ToList();
                var a = context.StoreOrder.ToList();
            }
            Assert.NotNull(records);
            Assert.True(records.Count > 0);
            Assert.Equal(records[0].PRODUCT_ID, "FUR-BO-10001798");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Ensure_CsvFileProcessor_SaveData_ThrowException_WhenInputIsNullOrEmpty(string csvString)
        {
            var processor = new CsvFileProcessor(_mapperMock.Object, _serviceProviderMock.Object);
            var res = await Assert.ThrowsAsync<BusinessException>(() => processor.SaveData(csvString));

            Assert.NotNull(res);
            Assert.Equal(res.Message, "Input is null or empty.");
        }

        private DatabaseContext CreateDatabaseContext() =>
            InMemoryDbBuilder.CreateInstance<DatabaseContext>(databaseName);
    }
}

