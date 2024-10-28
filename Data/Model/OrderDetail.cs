namespace Data.Model
{
    public class OrderDetail
    {
        public int Id { get; set; } 
        public int? ProductId { get; set; }
        public int? CartId { get; set; }
        public string NameProduct { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public string Image { get; set; }
        public Product? Product { get; set; }
        public Cart? Cart { get; set; }
    }
}
