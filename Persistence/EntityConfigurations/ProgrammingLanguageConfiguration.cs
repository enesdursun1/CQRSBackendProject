using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class ProgrammingLanguageConfiguration : IEntityTypeConfiguration<ProgrammingLanguage>
{
    public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
    {
        builder.ToTable("ProgrammingLanguages").HasKey(p => p.Id);

        builder.Property(p=>p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p=>p.Name).HasColumnName("Name").IsRequired();
        builder.HasMany(p => p.Technologies);
    }
}
