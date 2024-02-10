namespace Products.Business.Contracts
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
