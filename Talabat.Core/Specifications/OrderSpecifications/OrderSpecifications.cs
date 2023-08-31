using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecifications
{
    public class OrderSpecifications : BaseSpecification<Order>
    {
        public OrderSpecifications()
            : base()
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDecending(O => O.OrderDate);
        }

        public OrderSpecifications(string email)
            : base(O => O.BuyerEmail == email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDecending(O => O.OrderDate);
        }

        public OrderSpecifications(string email, int id)
           : base(O => O.BuyerEmail == email && O.Id == id)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);


        }
    }
}
