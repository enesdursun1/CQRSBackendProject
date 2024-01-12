using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
{
      public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
{
    builder.ToTable("UserOperationClaims").HasKey(p => p.Id);

    builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
    builder.Property(p => p.UserId).HasColumnName("UserId").IsRequired();
    builder.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();
        builder.HasOne(u => u.User);
        builder.HasOne(u => u.OperationClaim);


    }



}
