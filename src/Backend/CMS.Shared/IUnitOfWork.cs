using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Shared;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
