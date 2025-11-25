using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace StudentAdminSystem.Models
{
    public class Receipt
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("order_id")]
        public string OrderId { get; set; } = string.Empty;

        [BsonElement("student_id")]
        public int StudentId { get; set; }

        [BsonElement("receipt_date")]
        public DateTime ReceiptDate { get; set; } = DateTime.UtcNow;

        [BsonElement("total_amount")]
        public decimal TotalAmount { get; set; }

        // FIXED: Specified the generic type as ReceiptItem
        [BsonElement("items")]
        public List<ReceiptItem> Items { get; set; } = new List<ReceiptItem>();

        [BsonElement("payment_method")]
        public string PaymentMethod { get; set; } = string.Empty;

        [BsonElement("status")]
        public string Status { get; set; } = "Generated";
        public object DateIssued { get; internal set; }
    }

    public class ReceiptItem
    {
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