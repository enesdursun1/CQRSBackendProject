using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Technology> builder)
    {
        builder.ToTable("Technologies").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
        builder.HasOne(p => p.ProgrammingLanguage);



    }
}
