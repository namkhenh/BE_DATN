using Common.Contract.Common.Interfaces;
using Common.Infrastructure.Common.Implements;
using Portal.Application.Repositories.Interfaces.PermissionAction;
using Portal.Application.Repositories.Interfaces.RefeshToken;
using Portal.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.Implements.RefeshToken
{
  public class RefeshTokenRepository : RepositoryBaseAsync<Portal.Domain.Entities.RefeshToken, Guid, ApplicationDbContext>, IRefeshTokenRepository
  {
    public RefeshTokenRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }
  }
}
