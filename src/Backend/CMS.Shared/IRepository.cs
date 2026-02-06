using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Shared;

public interface IRepository<T>
{
    Task AddAsync(T Obj);
    Task AddManyAsync(List<T> Objs);
    Task<T?> GetAsync(Guid Id);
    Task<IEnumerable<T>?> GetManyAsync(List<Guid> Ids);
}
