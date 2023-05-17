using AutoMapper;
using Common.Common.ActionResponse;
using Common.Dtos.Page;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Interfaces.Page;
using Portal.Application.Repositories.Interfaces.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Implements.Page
{
  public class PageService : IPageService
  {
    private readonly IPageRepository _pageRepository;
    private readonly IMapper _mapper;
    public PageService(IPageRepository pageRepository, IMapper mapper)
    {
      _pageRepository = pageRepository;
      _mapper = mapper;
    }
    public async Task<ActionResponse<List<PageDto>>> CreateListPage(List<CreateUpdatePage> pages)
    {
      var pageInDbs = await _pageRepository.FindAll().ToListAsync();
      ActionResponse<List<PageDto>> result = new();
      List<Portal.Domain.Entities.Page> pageCreates = new();
      try
      {
        foreach (var page in pages)
        {
          if (page != null && pageInDbs.Any(x => x.Code == page.Code) == false)
          {
            var pageCreate = new Portal.Domain.Entities.Page()
            {
              Name = page.Name,
              Code = page.Code,
              Path = page.Path,
            };
            pageCreates.Add(pageCreate);

          }
        }
        if (pageCreates != null && pageCreates.Count() > 0)
        {
          await _pageRepository.CreateListAsync(pageCreates);
        }
        var pageDtos = _mapper.Map<List<Portal.Domain.Entities.Page>, List<PageDto>>(pageCreates);
        result.Data = pageDtos;
        result.Success = true;
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
