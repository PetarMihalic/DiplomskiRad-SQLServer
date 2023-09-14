using Microsoft.EntityFrameworkCore.Migrations;

namespace SneakerShopSQLServer.Models
{
    public class Inventory
    {
        public int ID { get; set; }
        public int SneakerID { get; set; }
        public float Size { get; set; }
        public int Quantity { get; set; }
        public Sneaker Sneaker { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
