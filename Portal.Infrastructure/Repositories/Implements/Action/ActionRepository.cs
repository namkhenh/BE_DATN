using Common.Contract.Common.Interfaces;
using Common.Infrastructure.Common.Implements;
using Portal.Application.Repositories.Interfaces.Action;
using Portal.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.Implements.Action
{
    public class ActionRepository : RepositoryBaseAsync<Portal.Domain.Entities.Action, Guid, ApplicationDbContext>, IActionRepository
    {
        public ActionRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {

        }
    }
}
