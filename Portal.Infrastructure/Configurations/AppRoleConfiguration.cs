using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Configurations
{
  public class AppRoleConfiguration : IEntityTypeConfiguration<Portal.Domain.Entities.AppRole>
  {
    public void Configure(EntityTypeBuilder<Portal.Domain.Entities.AppRole> builder)
    {
      builder.ToTable("AppRoles");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Code).IsRequired();
      builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
      builder.Property(x => x.CreatedDate).IsRequired();
      builder.Property(x => x.LastModifiedDate);
      builder.Property(x => x.DeleteAt);

    }
  }
}
