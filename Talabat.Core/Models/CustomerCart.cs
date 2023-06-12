namespace Talabat.Core.Models
{
    public class CustomerCart
    {
        public string Id { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public string PaymentIntentId { get; set; }

        public string ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }

        public decimal ShippingCost { get; set; }

        public CustomerCart(string id)
        {
            Id = id;
        }
    }
}
