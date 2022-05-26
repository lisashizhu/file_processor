using System;

namespace FileProcessor.Data
{
    public class STORE_ORDER
    {
        public int ID { get; set; }
        public string ORDER_ID { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public DateTime SHIP_DATE { get; set; }
        public string SHIP_MODE { get; set; }
        public int QUANTITY { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal PROFIT { get; set; }
        public string PRODUCT_ID { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CATEGORY { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
    }
}

