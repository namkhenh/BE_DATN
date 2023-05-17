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
  public class MenuConfiguration : IEntityTypeConfiguration<Menu>
  {
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
      builder.ToTable("Menus");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Code).IsRequired();
      builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
      builder.Property(x => x.Path).HasMaxLength(128).IsRequired();
      builder.Property(x => x.CreatedDate).IsRequired();
      builder.Property(x => x.LastModifiedDate);
      builder.Property(x => x.DeleteAt);
    }
  }
}
