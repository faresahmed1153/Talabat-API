

namespace Talabat.APIs.Dtos
{
    public class OrderDto
    {
        public string CartId { get; set; }

        public int DeliveryMethodId { get; set; }

        public AddressDto ShippingAddress { get; set; }
    }
}
