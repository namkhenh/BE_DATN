using AutoMapper;
using Common.Common.ActionResponse;
using Common.Dtos.Menu;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Interfaces.Menu;
using Portal.Application.Repositories.Interfaces.Menu;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Implements.Menu
{
  public class MenuService : IMenuService
  {
    private readonly IMenuRepository _menuRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    public MenuService(IMenuRepository menuRepository, IMapper mapper, ILogger logger)
    {
      _logger = logger;
      _mapper = mapper;
      _menuRepository = menuRepository;
    }
    public async Task<ActionResponse<List<MenuDto>>> CreateList(List<CreateUpdateMenu> menus)
    {
      ActionResponse<List<MenuDto>> result = new();
      try
      {
        var menuInDbs = await _menuRepository.FindAll().ToListAsync();
        List<Portal.Domain.Entities.Menu> listMenuCreate = new();
        foreach (var menu in menus)
        {
          var isMenuInDb = menuInDbs.Any(x => x.Code == menu.Code);
          if (menu != null && isMenuInDb == false)
          {
            var menuCreate = new Portal.Domain.Entities.Menu()
            {
              Name = menu.Name,
              Path = menu.Path,
              Code = menu.Code
            };
            listMenuCreate.Add(menuCreate);
          }
        }
        if (listMenuCreate.Count > 0)
        {
          await _menuRepository.CreateListAsync(listMenuCreate);
        }
        var menuDtoRes = _mapper.Map<List<Portal.Domain.Entities.Menu>, List<MenuDto>>(listMenuCreate);
        result.Success = true;
        result.Data = menuDtoRes;
        return result;
      }
      catch (Exception ex)
      {
        _logger.Error(ex.Message);
        result.Success = false;
        return result;
      }
    }
  }
}
