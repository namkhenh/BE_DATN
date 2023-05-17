using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Configurations
{
  public class RefeshTokenConfiguration : IEntityTypeConfiguration<Portal.Domain.Entities.RefeshToken>
  {
    public void Configure(EntityTypeBuilder<Portal.Domain.Entities.RefeshToken> builder)
    {
      builder.ToTable("RefeshTokens");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.UserName).IsRequired();
      builder.Property(x => x.RefeshTokenName).IsRequired();
      builder.Property(x => x.ExTime);
      builder.Property(x => x.CreatedDate).IsRequired();
      builder.Property(x => x.LastModifiedDate);
      builder.Property(x => x.DeleteAt);
    }
  }
}
