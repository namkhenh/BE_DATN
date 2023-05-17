using Common.Dtos.Action;
using Common.Dtos.Menu;
using Common.Dtos.Page;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Interfaces.Action;
using Portal.Application.Interfaces.ActionPage;
using Portal.Application.Interfaces.Menu;
using Portal.Application.Interfaces.Page;
using Portal.Domain.Common;
using Portal.Infrastructure;
using Portal.Infrastructure.Datas;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Portal.API.Rpc.setup
{
  // Các size khác được init còn size Portal ko đc init
  // mục đích thêm api , page vào trong các service
  [Route("api/[controller]")]
  [ApiController]
  public class SetupController : ControllerBase
  {
    private readonly Serilog.ILogger _logger;
    private readonly IMenuService _menuService;
    private readonly IPageService _pageService;
    private readonly IActionService _actionService;
    private readonly IActionPageService _actionPageService;
    public SetupController(IMenuService menuService, IPageService pageService
     , IActionService actionService, IActionPageService actionPageService, Serilog.ILogger logger)
    {
      _actionService = actionService;
      _pageService = pageService;
      _menuService = menuService;
      _actionPageService = actionPageService;
      _logger = logger;
    }
    [HttpGet("Init")]
    public async Task<IActionResult> Init()
    {
      List<Type> routeTypes = typeof(SetupController).Assembly.GetTypes()
         .Where(x => typeof(Root).IsAssignableFrom(x) && x.IsClass && x.Name != "Root")
         .ToList();
      await InitMenu(routeTypes);
      await InitAction(routeTypes);
      await InitPage(routeTypes);
      await InitActionPage(routeTypes);
      return Ok();
    }
    private async Task InitMenu(List<Type> routeTypes)
    {
      try
      {
        List<CreateUpdateMenu> createUpdateMenus = new();
        foreach (Type type in routeTypes)
        {
          var DisplayName = type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
               .Select(x => ((DisplayNameAttribute)x).DisplayName)
               .DefaultIfEmpty(type.Name)
               .FirstOrDefault();
          if (DisplayName != null && DisplayName != null)
          {
            var menuCode = FromNameToCode(DisplayName);
            createUpdateMenus.Add(new CreateUpdateMenu()
            {
              Name = DisplayName,
              Code = menuCode,
              Path = ""
            });
          }
        }
        await _menuService.CreateList(createUpdateMenus);
      }
      catch (Exception ex)
      {
        _logger.Error(ex.Message);
      }

    }
    private async Task InitPage(List<Type> routeTypes)
    {
      try
      {
        foreach (Type type in routeTypes)
        {
          var values = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
          .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
          .Select(x => (string)x.GetRawConstantValue())
          .ToList();
          if (values.Any())
          {
            List<CreateUpdatePage> createPages = new();
            foreach (var value in values)
            {

              if (value != null)
              {
                CreateUpdatePage createPage = new();
                createPage.Name = value;
                createPage.Path = value;
                createPage.Code = FromNameToCode(value);
                createPages.Add(createPage);
              }
            }
            await _pageService.CreateListPage(createPages);
          }
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex.Message);
      }

    }
    private async Task InitActionPage(List<Type> routeTypes)
    {
      try
      {

        foreach (Type type in routeTypes)
        {
          Dictionary<string, List<string>> request = new();
          var values = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
               .Where(fi => !fi.IsInitOnly && fi.FieldType == typeof(Dictionary<string, IEnumerable<string>>))
               .Select(x => (Dictionary<string, IEnumerable<string>>)x.GetValue(x))
               .FirstOrDefault();
          var DisplayName = type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
             .Select(x => ((DisplayNameAttribute)x).DisplayName)
             .DefaultIfEmpty(type.Name)
             .FirstOrDefault();
          var menuCode = "";
          if (!string.IsNullOrEmpty(DisplayName))
          {
            menuCode = FromNameToCode(DisplayName);
          }
          if (values != null && !string.IsNullOrEmpty(menuCode))
          {
            foreach (var value in values)
            {
              List<string> pageCodes = new();
              var actionCode = FromNameToCode(value.Key);
              foreach (var page in value.Value)
              {
                var pageCode = FromNameToCode(page);
                pageCodes.Add(pageCode);
              }
              request.Add(actionCode, pageCodes);
            }
          }

          if (values != null)
          {
            await _actionPageService.CreateListActionPage(request, menuCode);
          }
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex.Message);
      }

    }
    private async Task InitAction(List<Type> routeTypes)
    {
      try
      {
        List<CreateUpdateAction> createUpdateActions = new();
        foreach (Type type in routeTypes)
        {
          var menu = type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
               .Select(x => ((DisplayNameAttribute)x).DisplayName)
               .DefaultIfEmpty(type.Name)
               .FirstOrDefault();
          var menuCode = FromNameToCode(menu ?? "");
          var values = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
               .Where(fi => !fi.IsInitOnly && fi.FieldType == typeof(Dictionary<string, IEnumerable<string>>))
               .Select(x => (Dictionary<string, IEnumerable<string>>)x.GetValue(x))
               .FirstOrDefault();
          if (values != null)
          {
            foreach (var value in values)
            {
              var actionCode = FromNameToCode(value.Key);
              createUpdateActions.Add(new CreateUpdateAction()
              {
                Name = value.Key,
                Code = actionCode,
                Path = value.Key,
                MenuCode = menuCode ?? ""
              });
            }
            if (createUpdateActions.Count() > 0)
            {
              await _actionService.CreateListAction(createUpdateActions);
            }
          }
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex.Message);
      }

    }
    private static string RemoveVietnameseTone(string text)
    {
      string result = text.ToLower();
      result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
      result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
      result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
      result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
      result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
      result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
      result = Regex.Replace(result, "đ", "d");
      return result;
    }
    private string FromNameToCode(string value)
    {
      var result = "";
      try
      {
        if (!string.IsNullOrEmpty(value))
        {
          result = value.ToLower();
          result = result.Replace(" ", "-");
          result = RemoveVietnameseTone(result);
          return result;
        }
        return result;
      }
      catch (Exception ex)
      {
        _logger.Error(ex.Message);
        return result;
      }

    }
  }
}
