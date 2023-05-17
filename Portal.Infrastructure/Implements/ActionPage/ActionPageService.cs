using Common.Common.ActionResponse;
using Common.Dtos.ActionPage;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Interfaces.ActionPage;
using Portal.Application.Repositories.Interfaces.Action;
using Portal.Application.Repositories.Interfaces.ActionPage;
using Portal.Application.Repositories.Interfaces.Menu;
using Portal.Application.Repositories.Interfaces.Page;
using Portal.Domain.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Implements.ActionPage
{
  public class ActionPageService : IActionPageService
  {
    private readonly IMenuRepository _menuRepository;
    private readonly ILogger _logger;
    private readonly IPageRepository _pageRepository;
    private readonly IActionRepository _actionRepository;
    private readonly IActionPageRepository _actionPageRepository;
    public ActionPageService(IPageRepository pageRepository, IActionRepository actionRepository, IActionPageRepository actionPageRepository,
      ILogger logger, IMenuRepository menuRepository)
    {
      _menuRepository = menuRepository;
      _logger = logger;
      _actionPageRepository = actionPageRepository;
      _pageRepository = pageRepository;
      _actionRepository = actionRepository;
    }

    public async Task<ActionResponse<List<ActionPageDto>>> CreateListActionPage(Dictionary<string, List<string>> request, string menuCode)
    {
      ActionResponse<List<ActionPageDto>> result = new();
      try
      {

        List<Portal.Domain.Entities.ActionPage> actionPagesCreate = new();
        var menuInDbs = await _menuRepository.FindAll().ToListAsync();
        var actionInDbs = await _actionRepository.FindAll().ToListAsync();
        var pageInDbs = await _pageRepository.FindAll().ToListAsync();
        var actionPageInDbs = await _actionPageRepository.FindAll().ToListAsync();
        var menuRequest = menuCode;
        var menuDetailRequest = _menuRepository.FindByCondition(x => x.Code == menuCode).FirstOrDefault();
        if (menuDetailRequest != null)
        {
          var actionsInMenuRequest = await _actionRepository.FindByCondition(x => x.MenuId == menuDetailRequest.Id).ToListAsync();
          if (actionsInMenuRequest != null && actionsInMenuRequest.Count() > 0)
          {
            foreach (var actionRequest in request)
            {
              var actionCodeRequest = actionRequest.Key;
              if (actionsInMenuRequest.Any(x => x.Code == actionCodeRequest) == true)
              {
                var actionInDb = actionsInMenuRequest.Where(x => x.Code == actionCodeRequest).FirstOrDefault();
                if (actionInDb != null)
                {
                  var pagesOfActionInDb = _actionPageRepository.FindByCondition(x => x.ActionId == actionInDb.Id).ToList();
                  List<Portal.Domain.Entities.Page> pages = new();
                  foreach (var pageInDb in pagesOfActionInDb)
                  {
                    var addPage = pageInDbs.Where(x => x.Id == pageInDb.PathId).FirstOrDefault();
                    if (addPage != null)
                    {
                      pages.Add(addPage);
                    }

                  }
                  foreach (var pagesOfRequest in actionRequest.Value)
                  {

                    if (pages.Any(x => x.Code == pagesOfRequest) == false)
                    {
                      var pageId = pageInDbs.Where(x => x.Code == pagesOfRequest).Select(x => x.Id).FirstOrDefault();
                      actionPagesCreate.Add(new Portal.Domain.Entities.ActionPage()
                      {
                        ActionId = actionInDb.Id,
                        PathId = pageId,
                        Code = ""
                      });

                    }
                  }
                }
              }
            }
          }

        }
        if (actionPagesCreate.Count() > 0)
        {
          var lstId = await _actionPageRepository.CreateListAsync(actionPagesCreate);
          if (lstId.Count() > 0)
          {
            result.Success = true;

          }
        }
        return result;
      }
      catch (Exception ex)
      {
        result.Success = false;
        result.Message = ex.Message;
        return result;
      }
    }
  }
}
