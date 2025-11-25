using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace StudentAdminSystem.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("student_id")]
        public int StudentId { get; set; }

        [BsonElement("order_date")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // FIXED: Specified the generic type as OrderItem
        [BsonElement("items")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        [BsonElement("total_amount")]
        public decimal TotalAmount { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = "Pending";

        [BsonElement("payment_method")]
        public string PaymentMethod { get; set; } = string.Empty;

        [BsonElement("delivery_address")]
        public string DeliveryAddress { get; set; } = string.Empty;

        [BsonElement("notes")]
        public string? Notes { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class OrderItem
    {
        [BsonElement("product_id")]
        public string ProductId { get; set; } = string.Empty;

        [BsonElement("product_name")]
        public string ProductName { get; set; } = string.Empty;

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("unit_price")]
        public decimal UnitPrice { get; set; }

        [BsonElement("subtotal")]
        public decimal Subtotal { get; set; }
    }
}