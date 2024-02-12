namespace Products.Entities
{
    public class ProductEntity
    {
        public Guid ProductId {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Stock {  get; set; }  = 0;
        public double Price { get; set; } = 0;
        public short Status { get; set; } = true;
    }
}
