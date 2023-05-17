using Common.Contract.Common.Interfaces;
using Common.Infrastructure.Common.Implements;
using Portal.Application.Repositories.Interfaces.Page;
using Portal.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.Implements.Page
{
    public class PageRepository : RepositoryBaseAsync<Portal.Domain.Entities.Page, Guid, ApplicationDbContext>, IPageRepository
    {
        public PageRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {

        }
    }
}
