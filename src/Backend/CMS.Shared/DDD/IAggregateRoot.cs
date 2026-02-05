using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Shared.DDD;

public interface IAggregateRoot
{
    public Guid Id { get; set; }
}
