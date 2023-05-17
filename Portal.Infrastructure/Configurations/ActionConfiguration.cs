using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Portal.Infrastructure.Configurations
{
  public class ActionConfiguration : IEntityTypeConfiguration<Portal.Domain.Entities.Action>
  {
    public void Configure(EntityTypeBuilder<Portal.Domain.Entities.Action> builder)
    {
      builder.ToTable("Actions");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.MenuId).IsRequired();
      builder.Property(x => x.Code).IsRequired();
      builder.Property(x => x.Path).HasMaxLength(128).IsRequired();
      builder.Property(x => x.CreatedDate).IsRequired();
      builder.Property(x => x.LastModifiedDate);
      builder.Property(x => x.DeleteAt);
    }
  }
}
