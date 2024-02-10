namespace Products.Business.Contracts
{
    public interface ICommandHandler<TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
