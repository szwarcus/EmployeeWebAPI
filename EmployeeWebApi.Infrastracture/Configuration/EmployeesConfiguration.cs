using EmployeeWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeWebApi.Infrastracture.Configuration
{
    internal class EmployeesConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Id).HasColumnName("Id").HasConversion(id => id.Value, id => new EmployeeWebAPI.Domain.ValueObjects.Ids.EmployeeId(id)).IsRequired();
            builder.OwnsOne(x => x.Name).Property(x=>x.First).HasColumnName("FirstName").IsRequired();
            builder.OwnsOne(x => x.Name).Property(x=>x.Last).HasColumnName("LastName").IsRequired();
            builder.OwnsOne(x => x.Pesel).Property(x=>x.Value).HasColumnName("Pesel").IsRequired();
            builder.OwnsOne(x => x.RegistrationNumber).Property(x=>x.Value).HasColumnName("RegistrationNumber").IsRequired();
        }
    }
}