using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Portal.Domain.Entities;
using Portal.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Datas
{
  public class ApplicationDbContext : IdentityDbContext<Portal.Domain.Entities.AppUser, Portal.Domain.Entities.AppRole, string>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {

      builder.ApplyConfiguration(new AppUserConfiguration());
      builder.ApplyConfiguration(new RefeshTokenConfiguration());
      builder.ApplyConfiguration(new AppRoleConfiguration());
      builder.ApplyConfiguration(new PermissionConfiguration());
      builder.ApplyConfiguration(new ActionConfiguration());
      builder.ApplyConfiguration(new MenuConfiguration());
      builder.ApplyConfiguration(new PermissionActionConfiguration());
      builder.ApplyConfiguration(new PageConfiguration());
      builder.ApplyConfiguration(new ActionPageConfiguration());
      base.OnModelCreating(builder);
    }

    public DbSet<Portal.Domain.Entities.AppUser> AppUsers { get; set; }
    public DbSet<Portal.Domain.Entities.AppRole> AppRoles { get; set; }
    public DbSet<Permission> Permission { get; set; }
    public DbSet<Portal.Domain.Entities.Action> Actions { get; set; }
    public DbSet<PermissionAction> PermissionActions { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<RefeshToken> RefeshTokens { get; set; }
    public DbSet<ActionPage> ActionPages { get; set; }
    public DbSet<Menu> Menus { get; set; }
  }
}
