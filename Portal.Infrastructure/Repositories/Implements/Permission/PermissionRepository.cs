using Common.Contract.Common.Interfaces;
using Common.Infrastructure.Common.Implements;
using Portal.Application.Repositories.Interfaces.Permission;
using Portal.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.Implements.Permission
{
    public class PermissionRepository : RepositoryBaseAsync<Portal.Domain.Entities.Permission, Guid, ApplicationDbContext>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {

        }
    }
}
