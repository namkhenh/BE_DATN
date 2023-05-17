using Common.Contract.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Repositories.Interfaces.Page
{
    public interface IPageRepository : IRepositoryBaseAsync<Portal.Domain.Entities.Page, Guid>
    {
    }
}
