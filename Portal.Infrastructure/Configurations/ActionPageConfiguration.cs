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
  public class ActionPageConfiguration : IEntityTypeConfiguration<ActionPage>
  {
    public void Configure(EntityTypeBuilder<ActionPage> builder)
    {
      builder.ToTable("ActionPages");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Code).IsRequired();
      builder.Property(x => x.ActionId).IsRequired();
      builder.Property(x => x.PathId).IsRequired();
      builder.Property(x => x.CreatedDate).IsRequired();
      builder.Property(x => x.LastModifiedDate);
      builder.Property(x => x.DeleteAt);
    }
  }
}
