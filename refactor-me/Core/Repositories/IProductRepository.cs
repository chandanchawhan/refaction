using refactor_me.Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace refactor_me.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<ProductOption> GetProductOptionsOfProduct(Product product);
        ProductOption GetProductOptionsOfProductByProductOptionId(Product product, Guid productOptionId);
    }
}
