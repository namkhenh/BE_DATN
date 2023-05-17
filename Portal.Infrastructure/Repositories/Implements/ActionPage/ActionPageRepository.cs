using Common.Contract.Common.Interfaces;
using Common.Infrastructure.Common.Implements;
using Portal.Application.Repositories.Interfaces.ActionPage;
using Portal.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.Implements.ActionPage
{
    public class ActionPageRepository : RepositoryBaseAsync<Portal.Domain.Entities.ActionPage, Guid, ApplicationDbContext>, IActionPageRepository
    {
        public ActionPageRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {

        }
    }
}
