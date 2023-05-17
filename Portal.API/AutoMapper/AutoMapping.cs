using AutoMapper;
using Common.Dtos.Action;
using Common.Dtos.ActionPage;
using Common.Dtos.Menu;
using Common.Dtos.Page;
using Common.Dtos.Site;
using Common.Dtos.User;
using Portal.Domain.Entities;

namespace Portal.API.AutoMapper
{
  public class AutoMapping : Profile
  {
    public AutoMapping()
    {
      CreateMap<Menu, MenuDto>().ReverseMap();
      CreateMap<AppUser, UserDto>().ReverseMap();
      CreateMap<Portal.Domain.Entities.Action, ActionDto>();
      CreateMap<Portal.Domain.Entities.Page, PageDto>();
      CreateMap<Portal.Domain.Entities.ActionPage, ActionPageDto>();
    }
  }
}
