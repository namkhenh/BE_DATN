using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Configurations
{
  public class AppUserConfiguration : IEntityTypeConfiguration<Portal.Domain.Entities.AppUser>
  {
    public void Configure(EntityTypeBuilder<Portal.Domain.Entities.AppUser> builder)
    {
      builder.ToTable("AppUsers");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Code).IsRequired();
      builder.Property(x => x.FirstName).HasMaxLength(128).IsRequired();
      builder.Property(x => x.LastName).HasMaxLength(128).IsRequired();
      builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
      builder.Property(x => x.CreatedDate);
      builder.Property(x => x.LastModifiedDate);
      builder.Property(x => x.DeleteAt);
    }
  }
}
