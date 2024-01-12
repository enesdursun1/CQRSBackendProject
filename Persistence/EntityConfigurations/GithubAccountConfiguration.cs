using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class GithubAccountConfiguration: IEntityTypeConfiguration<GithubAccount>
{

   

    public void Configure(EntityTypeBuilder<GithubAccount> builder)
    {
        builder.ToTable("GithubAccounts").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(p => p.GithubAddress).HasColumnName("GithubAddress").IsRequired();
        builder.HasOne(p => p.User);



    }
}
