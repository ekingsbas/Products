namespace Products.BusinessModels.Commands
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Stock { get; set; } = 0;
        public double Price { get; set; } = 0;
        public short Status { get; set; } = 1;

        public CreateProductCommand
            (
                string name, 
                string description, 
                double stock, 
                double price, 
                short status
            )
        {
            Name = name;
            Description = description;
            Stock = stock;
            Price = price;
            Status = status;
        }
    }
}
