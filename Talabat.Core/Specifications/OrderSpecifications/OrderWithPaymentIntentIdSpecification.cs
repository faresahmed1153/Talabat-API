using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecifications
{
    public class OrderWithPaymentIntentIdSpecification:BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdSpecification(string paymentintentId)
            :base(O=>O.PaymentIntedId==paymentintentId)
        {

        }
    }
}
