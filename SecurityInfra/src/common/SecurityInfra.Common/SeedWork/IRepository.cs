using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Common.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
