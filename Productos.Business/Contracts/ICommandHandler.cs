using Products.BusinessModels.Commands;
using Products.BusinessModels.Product;

namespace Products.Business.Contracts
{
    public interface ICommandHandler<TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
