using Common.Contract.Common.Interfaces;
using Common.Infrastructure.Common.Implements;
using Portal.Application.Repositories.Interfaces.PermissionAction;
using Portal.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.Implements.PermissionAction
{
    public class PermissionActionRepository : RepositoryBaseAsync<Portal.Domain.Entities.PermissionAction, Guid, ApplicationDbContext>, IPermissionActionRepository
    {
        public PermissionActionRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {

        }
    }
}
