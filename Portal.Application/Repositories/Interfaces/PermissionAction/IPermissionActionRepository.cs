using Common.Contract.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Repositories.Interfaces.PermissionAction
{
    public interface IPermissionActionRepository: IRepositoryBaseAsync<Portal.Domain.Entities.PermissionAction, Guid>
    {
    }
}
