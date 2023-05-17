using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Domain.Entities;

namespace Portal.Infrastructure.Configurations
{
  public class PageConfiguration : IEntityTypeConfiguration<Page>
  {
    public void Configure(EntityTypeBuilder<Page> builder)
    {
      builder.ToTable("Pages");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Code).IsRequired();
      builder.Property(x => x.Name).HasMaxLength(500).IsRequired();
      builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
      builder.Property(x => x.Path).HasMaxLength(128).IsRequired();
      builder.Property(x => x.CreatedDate).IsRequired();
      builder.Property(x => x.LastModifiedDate);
      builder.Property(x => x.DeleteAt);

    }
  }
}
