using API_Nox.Model;
using API_Nox.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Nox.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<UserViewModel>
    {
        public void Configure(EntityTypeBuilder<UserViewModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(80);
        }
    }
}
