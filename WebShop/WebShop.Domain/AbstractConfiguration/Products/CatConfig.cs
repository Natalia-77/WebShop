using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities.Products;

namespace WebShop.Domain.AbstractConfiguration.Products
{
    public class CatConfig : BaseConfiguration<Cat>
    {
        public override void Configure(EntityTypeBuilder<Cat> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("tblCats");

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Image)
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}
