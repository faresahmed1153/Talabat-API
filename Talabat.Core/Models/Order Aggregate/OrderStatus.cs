using System.Runtime.Serialization;

namespace Talabat.Core.Models.Order_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "PaymentRecived")]
        PaymentRecived,

        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed

    }
}
