using refactor_me.Core.Repositories;
using System;

namespace refactor_me.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IProductOptionRepository ProductOptions { get; }

        int Complete();
    }
}
