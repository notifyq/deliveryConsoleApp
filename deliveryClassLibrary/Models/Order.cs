using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace deliveryClassLibrary.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Weight { get; set; }
        public string OrderPoint { get; set; }
        public DateTime DeliveryTime { get; set; }
    }
}
