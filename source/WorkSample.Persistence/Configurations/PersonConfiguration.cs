using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using WorkSample.Domain;

namespace WorkSample.Persistence.Configurations;

/// <summary>
///     Configuration for entity type <see cref="Person"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    /// <summary>
    ///     Configures the entity type <see cref="Person"/>.
    /// </summary>
    /// <param name="builder">The current instance of <see cref="EntityTypeBuilder{Person}"/></param>
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasData(
            new Person
            {
                Id = 1,
                Name = "Max",
                Surname = "Mustermann",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new Person
            {
                Id = 2,
                Name = "Erika",
                Surname = "Musterfrau",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            }
        );

        builder.Property(q => q.Id)
            .UseIdentityColumn();

        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(q => q.Surname)
            .IsRequired()
            .HasMaxLength(100);
    }
}