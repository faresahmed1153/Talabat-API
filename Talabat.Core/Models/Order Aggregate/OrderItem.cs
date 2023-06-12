namespace Talabat.Core.Models.Order_Aggregate
{
    public class OrderItem : BaseModel
    {

        public ProductItemOrdered Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }


        public OrderItem()
        {

        }

        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }




    }
}
