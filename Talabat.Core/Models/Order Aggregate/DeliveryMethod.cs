namespace Talabat.Core.Models.Order_Aggregate
{
    public class DeliveryMethod : BaseModel
    {
        public string ShortName { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string DeliveryTime { get; set; }

        //ICollection<Order>

        //one to one bttw7l l one => many fe el sql mn 8er mktb configuration

        public DeliveryMethod()
        {

        }

        public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }



    }
}
