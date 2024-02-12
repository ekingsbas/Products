namespace Products.Business.Contracts
{
    public interface IExternalDiscountService
    {
        Task<short> GetDiscountAsync();
    }
}
