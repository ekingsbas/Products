namespace Products.BusinessModels.Commands
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Stock { get; set; } = 0;
        public double Price { get; set; } = 0;
        public bool Status { get; set; } = true;
        public string? StatusName { get; set; }
        public double Discount { get; set; }
        public double FinalPrice { get; set; }

        public CreateProductCommand
            (
                string name, 
                string description, 
                double stock, 
                double price, 
                bool status
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
