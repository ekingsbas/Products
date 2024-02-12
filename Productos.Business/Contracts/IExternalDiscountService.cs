namespace Products.Business.Contracts
{
    public interface IExternalDiscountService
    {
        Task<int> GetDiscountAsync();
    }
}
