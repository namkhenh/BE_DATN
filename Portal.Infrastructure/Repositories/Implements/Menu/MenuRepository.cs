using Common.Contract.Common.Interfaces;
using Common.Infrastructure.Common.Implements;
using Portal.Application.Repositories.Interfaces.Menu;
using Portal.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.Implements.Menu
{
    public class MenuRepository : RepositoryBaseAsync<Portal.Domain.Entities.Menu, Guid, ApplicationDbContext>, IMenuRepository
    {
        public MenuRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {

        }
    }
}
